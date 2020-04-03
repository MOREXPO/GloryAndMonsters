using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manabar : MonoBehaviour
{
    public Image imgMana;
    float mana, maxMana = 100f;
    float timer, waitTime = 10;
    public Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        maxMana = 100f + playerScript.Magia;
        mana = maxMana;
        Time.timeScale = 1;
    }

    void Update()
    {
        maxMana = 100f + playerScript.Magia;
        timer += Time.deltaTime;
            // Comprueba si hemos alcanzado más de 2 segundos.
            // Restar dos es más preciso con el tiempo que restablecer a cero.
            if (timer > waitTime)
            {
                mana += 5;
                // Eliminar los 2 segundos grabados.
                timer = timer - waitTime;    
            }
        
        if (mana>=100) {
            mana = maxMana;
        }
        imgMana.transform.localScale = new Vector2(mana / maxMana, 1);
    }

    public void Gastar(float amount) {
        mana = Mathf.Clamp(mana-amount,0f,maxMana);
    }

    public bool Usable(float cant) {
        if (cant <= mana)
        {
            return true;
        }
        else {
            return false;
        }
    }
}
