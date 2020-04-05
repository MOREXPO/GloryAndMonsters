using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("escape")) Application.Quit();
       
    }

    public void setBound(GameObject bg) {
        CinemachineConfiner CinemachineC=GetComponent<CinemachineConfiner>();
        if (bg != null) CinemachineC.m_BoundingShape2D = bg.GetComponent<Collider2D>();
        else CinemachineC.m_BoundingShape2D = null;
    }

  
}
