using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
   
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void EmpezarJuego(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Cerrarjuego(){
        Application.Quit();
    }
}
