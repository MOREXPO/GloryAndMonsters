using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemVida : MonoBehaviour
{
    public GameObject healthbar;
    public float cantCura;
    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.transform.tag == "Player")
        {
            Destroy(gameObject);
            if (col.transform.tag == "Player")
            {
                healthbar.SendMessage("Curar", cantCura);
            }
        }
    }
}
