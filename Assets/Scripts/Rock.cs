using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

    [Tooltip("Velocidad de movimiento en unidades del mundo")]
    public float speed;

    GameObject player;   // Recuperamos al objeto jugador
    Rigidbody2D rb2d;    // Recuperamos el componente de cuerpo rígido
    Vector3 target, dir; // Vectores para almacenar el objetivo y su dirección
    private GameObject healthbar;//Recuperamos la barra de vida

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
        healthbar = GameObject.Find("Healthbar");
        
        if (player != null){
            target = player.transform.position;
            dir = (target - transform.position).normalized;
        }
	}
    // Si hay un objetivo movemos la roca hacia su posición
    void FixedUpdate () {
        
        if (target != Vector3.zero) {
            rb2d.MovePosition(transform.position + (dir * speed) * Time.deltaTime);
        }
	}
    // Si chocamos contra el jugador o un ataque la borramos
    void OnTriggerEnter2D(Collider2D col){
        
        if (col.transform.tag == "Player" || col.transform.tag == "Attack"){
            Destroy(gameObject);
            if (col.transform.tag == "Player") {
                healthbar.SendMessage("TakeDamage",15);
            }
        }
    }
    // Si se sale de la pantalla borramos la roca
    void OnBecameInvisible() {
       
        Destroy(gameObject);
    }
}
