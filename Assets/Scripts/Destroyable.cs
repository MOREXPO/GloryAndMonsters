﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public string destroyState;
    public float timeForDisable;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Attack") {
            anim.Play(destroyState);
            yield return new WaitForSeconds(timeForDisable);
            foreach (Collider2D c in GetComponents<Collider2D>()) {
                c.enabled = false;
            }
        }
    }

    private void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(destroyState)&&stateInfo.normalizedTime>=1) {
            Destroy(gameObject);
        }
    }
}
