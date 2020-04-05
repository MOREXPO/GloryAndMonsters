using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour
{
    public float speed = 4f;
    Animator anim;
    Rigidbody2D rb2d;
    Vector2 mov;
    CircleCollider2D attackCollider;
    public GameObject initialMap;
    public GameObject slashPrefab;
    public Manabar manabar;
    bool movePrevent;
    Aura aura;


    public int NivelVida { set; get; }
    public int NivelResistencia { set; get; }
    public int NivelFuerza { set; get; }
    public int NivelMagia { set; get; }
    public float Vida{ set; get; }

    public int Resistencia{ set; get; }

    public int Fuerza{ set; get; }

    public int Magia{ set; get; }

    private void Awake()
    {
        Assert.IsNotNull(initialMap);
        Assert.IsNotNull(slashPrefab);
        Vida = 100f;
        Resistencia = 0;
        Fuerza = 1;
        Magia = 3;
        NivelVida = 1;
        NivelResistencia = 1;
        NivelFuerza = 1;
        NivelMagia = 1;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        attackCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
        attackCollider.enabled = false;
        //Camera.main.GetComponent<MainCamera>().setBound(initialMap);
        aura = transform.GetChild(1).GetComponent<Aura>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        Animaciones();
        AtaqueEspada();
        SlashAttack();
        PreventMovement();
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position+mov*speed*Time.deltaTime);
    }

    void Movimiento() {
        mov = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    void Animaciones() {
        if (mov != Vector2.zero)
        {
            anim.SetFloat("movX", mov.x);
            anim.SetFloat("movY", mov.y);
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }
    void AtaqueEspada() {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool attacking = stateInfo.IsName("Player_Attack");
        if (Input.GetKeyDown("space") && !attacking)
        {
            anim.SetTrigger("attacking");
        }

        if (mov != Vector2.zero) attackCollider.offset = new Vector2(mov.x / 2, mov.y / 2);

        if (attacking)
        {
            float playbackTime = stateInfo.normalizedTime;
            if (playbackTime > 0.33 && playbackTime < 0.66) attackCollider.enabled = true;
            else attackCollider.enabled = false;
        }
    }

    void PreventMovement() {
        if (movePrevent) {
            mov = Vector2.zero;
        }
    }

    void SlashAttack() {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool loading = stateInfo.IsName("Player_Slash");
        if (Input.GetKeyDown(KeyCode.LeftAlt) && manabar.Usable(20)) {
            anim.SetTrigger("loading");
            aura.AuraStart();
        }
        else if(Input.GetKeyUp(KeyCode.LeftAlt) && manabar.Usable(20))
        {
            anim.SetTrigger("attacking");
            if (aura.IsLoaded())
            {
                float angle = Mathf.Atan2(anim.GetFloat("movY"), anim.GetFloat("movX")) * Mathf.Rad2Deg;
                GameObject slashObj = Instantiate(slashPrefab, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
                Slash slash = slashObj.GetComponent<Slash>();
                slash.mov.x = anim.GetFloat("movX");
                slash.mov.y = anim.GetFloat("movY");
                manabar.Gastar(20);
            }
            aura.AuraStop();
            StartCoroutine(EnableMovementAfter(0.4f));
        }

        if (loading) {
            movePrevent = true;
        }
       
    }

    IEnumerator EnableMovementAfter(float seconds) {
        yield return new WaitForSeconds(seconds);
        movePrevent = false;
    }
}
