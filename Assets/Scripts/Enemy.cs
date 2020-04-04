using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    // Variables para gestionar el radio de visión, el de ataque y la velocidad
    public float visionRadius;
    public float attackRadius;
    public float speed;

    // Variables relacionadas con el ataque
    [Tooltip("Prefab de la roca que se disparará")]
    public GameObject rockPrefab;
    [Tooltip("Velocidad de ataque (segundos entre ataques)")]
    public float attackSpeed = 2f;
    bool attacking;

    // Variables relacionadas con la vida
    [Tooltip("Puntos de vida")]
    public int maxHp = 3;
    [Tooltip("Vida actual")]
    public int hp;
    //Variables relacionadas con la experiencia
    public int exp;
    Ui ui;
    // Variable para guardar al jugador
    GameObject player;
    //Variable de referencia al componente Player
    public Player playerScript;
    // Variable para guardar la posición inicial
    Vector3 initialPosition, target;

    // Animador y cuerpo cinemático con la rotación en Z congelada
    Animator anim;
    Rigidbody2D rb2d;

    void Start () {

       
        player = GameObject.FindGameObjectWithTag("Player");

       
        initialPosition = transform.position;

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        ui = GameObject.Find("UI").GetComponent<Ui>();
        
        hp = maxHp;
    }

    void Update () {

        
        target = initialPosition;

        
        if (player!=null) {
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                player.transform.position - transform.position,
                visionRadius,
                1 << LayerMask.NameToLayer("Default")
            
            );

            
            Vector3 forward = transform.TransformDirection(player.transform.position - transform.position);
            Debug.DrawRay(transform.position, forward, Color.red);

            
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Player")
                {
                    target = player.transform.position;
                }
            }
        }
        

        
        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = (target - transform.position).normalized;

       
        if (target != initialPosition && distance < attackRadius){
           
            anim.SetFloat("movX", dir.x);
            anim.SetFloat("movY", dir.y);
            anim.Play("Enemy_Walk", -1, 0); 

            
            if (!attacking) StartCoroutine(Attack(attackSpeed));
        }
        
        else {
            rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);

            
            anim.speed = 1;
            anim.SetFloat("movX", dir.x);
            anim.SetFloat("movY", dir.y);
            anim.SetBool("walking", true);
        }

       
        if (target == initialPosition && distance < 0.05f){
            transform.position = initialPosition; 
           
            anim.SetBool("walking", false);
        }

        
        Debug.DrawLine(transform.position, target, Color.green);
    }

    // Podemos dibujar el radio de visión y ataque sobre la escena dibujando una esfera
    void OnDrawGizmosSelected() {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);

    }

    IEnumerator Attack(float seconds){
        attacking = true;  
        
        if (target != initialPosition && rockPrefab != null) {
            Instantiate(rockPrefab, transform.position, transform.rotation);
            
            yield return new WaitForSeconds(seconds);
        }
        attacking = false; 
    }

    //Gestión del ataque fisico
    public void AtaqueFisico(){
        hp -= playerScript.Fuerza;
        if (hp <= 0) {
            Destroy(gameObject);
            ui.Aumentar(exp);
        }
    }
    //Gestión del ataque Magico
    public void AtaqueMagico()
    {
        hp -= playerScript.Magia;
        if (hp <= 0)
        {
            Destroy(gameObject);
            ui.Aumentar(exp);
        }
    }

    // Dibujamos las vidas del enemigo en una barra 
    void OnGUI() {
        
        Vector2 pos = Camera.main.WorldToScreenPoint (transform.position);

       
        GUI.Box(
            new Rect(
                pos.x - 20,                   
                Screen.height - pos.y + 60,  
                40,                               
                24                             
            ), hp + "/" + maxHp               
        );
    }

}