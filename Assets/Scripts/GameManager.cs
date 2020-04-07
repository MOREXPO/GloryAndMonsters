using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public string Nombre { set; get; }
    [SerializeField]private PlayableDirector secuenciaComienzo;
    public GameObject Texto;
    public GameObject CM_Comienzo;


    public void IntroducirNombre() {
        Nombre = Texto.GetComponent<TextMeshProUGUI>().text;
        if(Nombre.Trim().Length!=1)secuenciaComienzo.Play();

    }

    public void CambiarCamaraPersonaje() {
        CM_Comienzo.GetComponent<CinemachineVirtualCamera>().Priority = -1;
    }
}
