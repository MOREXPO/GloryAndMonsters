using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image health;
    float hp, maxHp;
    GameObject area;
    public GameObject player;
    public Player playerScript;

    void Start()
    {
        maxHp = playerScript.Vida;
        hp = maxHp;
        area = GameObject.FindGameObjectWithTag("Area");
    }

    void Update()
    {
        maxHp = playerScript.Vida;
        if (hp<=0) {
            Destroy(player);
            StartCoroutine(area.GetComponent<Area>().ShowArea("GAME OVER"));
        }
        health.transform.localScale = new Vector2(hp / maxHp, 1);
    }

    public void TakeDamage(float amount) {
        hp = Mathf.Clamp(hp+playerScript.Resistencia-amount,0f,maxHp);
    }
}
