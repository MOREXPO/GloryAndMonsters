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

            if (timer > waitTime)
            {
                mana += 5;

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
