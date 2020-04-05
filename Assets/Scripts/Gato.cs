using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gato : MonoBehaviour
{
    // Variables para gestionar el radio de visión, el de ataque y la velocidad
    public float visionRadius;
    public float spaceRadius;
    public float speed;

    // Variable para guardar al jugador
    GameObject raton;

    // Variable para guardar la posición inicial
    Vector3 initialPosition, target;

    // Animador y cuerpo cinemático con la rotación en Z congelada
    Animator anim;
    Rigidbody2D rb2d;

    void Start()
    {


        raton = GameObject.FindGameObjectWithTag("Raton");


        initialPosition = transform.position;

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {


        target = initialPosition;


        if (raton != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                raton.transform.position - transform.position,
                visionRadius,
                1 << LayerMask.NameToLayer("Default")

            );


            Vector3 forward = transform.TransformDirection(raton.transform.position - transform.position);
            Debug.DrawRay(transform.position, forward, Color.red);


            if (hit.collider != null)
            {
                if (hit.collider.tag == "Raton")
                {
                    target = raton.transform.position;
                }
            }
        }



        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = (target - transform.position).normalized;


        if (target != initialPosition && distance < spaceRadius)
        {

            anim.SetFloat("movX", dir.x);
            anim.SetFloat("movY", dir.y);
            anim.Play("Gato_Walk", -1, 0);

        }

        else
        {
            rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);


            anim.speed = 1;
            anim.SetFloat("movX", dir.x);
            anim.SetFloat("movY", dir.y);
            anim.SetBool("walking", true);
        }


        if (target == initialPosition && distance < 0.05f)
        {
            transform.position = initialPosition;

            anim.SetBool("walking", false);
        }


        Debug.DrawLine(transform.position, target, Color.green);
    }

    // Podemos dibujar el radio de visión y ataque sobre la escena dibujando una esfera
    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.DrawWireSphere(transform.position, spaceRadius);

    }
   
}
