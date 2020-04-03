using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ui : MonoBehaviour
{
    public Image imgExp;
    float exp,expResto=0,maxExp = 100f;
    bool permitirExp;
    int cantNivel = 1;
    TextMeshProUGUI nivel;
    public Player player;
    GameObject statVida, statResistencia, statFuerza, statMagia;
    // Start is called before the first frame update
    void Start()
    {
        permitirExp = true;
        exp = 0;
        nivel = GameObject.Find("Nivel").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        nivel.SetText("{0}", cantNivel);
        statVida = GameObject.Find("StatVida");
        statResistencia = GameObject.Find("StatResistencia");
        statFuerza = GameObject.Find("StatFuerza");
        statMagia = GameObject.Find("StatMagia");
        statVida.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("{0}", player.NivelVida);
        statResistencia.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("{0}", player.NivelResistencia);
        statFuerza.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("{0}",player.NivelFuerza);
        statMagia.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("{0}", player.NivelMagia);
        statVida.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("{0}", player.Vida);
        statResistencia.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("{0}", player.Resistencia);
        statFuerza.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("{0}", player.Fuerza);
        statMagia.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("{0}", player.Magia);

        statVida.transform.GetChild(3).GetComponent<TextMeshProUGUI>().enabled = false;
        statResistencia.transform.GetChild(3).GetComponent<TextMeshProUGUI>().enabled = false;
        statFuerza.transform.GetChild(3).GetComponent<TextMeshProUGUI>().enabled = false;
        statMagia.transform.GetChild(3).GetComponent<TextMeshProUGUI>().enabled = false;
    }

    void Update()
    {
        if (exp >= 100)
        {
            if (SubirStat())
            {
                expResto = exp - 100;
                exp = expResto;
                cantNivel++;
                nivel.SetText("{0}", cantNivel);
                statVida.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("{0}", player.Vida);
                statResistencia.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("{0}", player.Resistencia);
                statFuerza.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("{0}", player.Fuerza);
                statMagia.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("{0}", player.Magia);
            }
            else {
                statVida.transform.GetChild(3).GetComponent<TextMeshProUGUI>().enabled = true;
                statResistencia.transform.GetChild(3).GetComponent<TextMeshProUGUI>().enabled = true;
                statFuerza.transform.GetChild(3).GetComponent<TextMeshProUGUI>().enabled = true;
                statMagia.transform.GetChild(3).GetComponent<TextMeshProUGUI>().enabled = true;
                imgExp.color = Color.red;
                imgExp.transform.localScale = new Vector2(100f / maxExp, 1);
                permitirExp = false;
            }

        }
        else {
            permitirExp = true;
            statVida.transform.GetChild(3).GetComponent<TextMeshProUGUI>().enabled = false;
            statResistencia.transform.GetChild(3).GetComponent<TextMeshProUGUI>().enabled = false;
            statFuerza.transform.GetChild(3).GetComponent<TextMeshProUGUI>().enabled = false;
            statMagia.transform.GetChild(3).GetComponent<TextMeshProUGUI>().enabled = false;
            imgExp.color = Color.yellow;
            imgExp.transform.localScale = new Vector2(exp / maxExp, 1);
        }
        
    }

    public void Aumentar(float cantExp) {
        if (permitirExp) {
            exp = exp + cantExp;
        }
    }

    public bool SubirStat() {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                player.Vida += 2;
                player.NivelVida++;
                statVida.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("{0}", player.NivelVida);
                return true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                player.Resistencia += 2;
                player.NivelResistencia++;
                statResistencia.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("{0}", player.NivelResistencia);
            return true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                player.Fuerza += 2;
                player.NivelFuerza++;
                statFuerza.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("{0}", player.NivelFuerza);
            return true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                player.Magia += 2;
                player.NivelMagia++;
                statMagia.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("{0}", player.NivelMagia);
            return true;
            }
        return false;
        }
}
