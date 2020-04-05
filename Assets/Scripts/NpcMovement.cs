using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    public bool inMovement;
    public float moveSpeed;
    private Rigidbody2D myRigidBody;
    public bool isWalking;
    public float walkTime;
    private float walkCounter;
    public float waitTime;
    private float waitCounter;
    private int WalkDirection;
    private Dialogue_Manager dialogue_manager;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        waitCounter = waitTime;
        walkCounter = walkTime;
        if (GetComponent<Dialogue_Manager>() != null) dialogue_manager = GetComponent<Dialogue_Manager>();
        if(GetComponent<Animator>()!=null)anim = GetComponent<Animator>();
        
        
        if (inMovement)ChooseDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogue_manager != null)if (dialogue_manager.IsWithin()) isWalking = false;
        if (!inMovement) isWalking = false;
        if (isWalking)
        {
            walkCounter -= Time.deltaTime;
            
            switch (WalkDirection) {
                case 0:
                    myRigidBody.velocity = new Vector2(0,moveSpeed);
                    break;
                case 1:
                    myRigidBody.velocity = new Vector2(moveSpeed,0);
                    break;
                case 2:
                    myRigidBody.velocity = new Vector2(0,-moveSpeed);
                    break;
                case 3:
                    myRigidBody.velocity = new Vector2(-moveSpeed, 0);
                    break;
            }
            if (GetComponent<Animator>()!=null) {
                anim.speed = 1;
                anim.SetFloat("movX", myRigidBody.velocity.x);
                anim.SetFloat("movY", myRigidBody.velocity.y);
                anim.SetBool("walking", true);
            }
            

            if (walkCounter < 0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }
        }
        else {
            waitCounter -= Time.deltaTime;
            myRigidBody.velocity = Vector2.zero;
            if (GetComponent<Animator>() != null) anim.SetBool("walking", false);
            if (waitCounter<0) {
                ChooseDirection();
            }
        }
    }

    public void ChooseDirection() {
        WalkDirection = Random.Range(0,4);
        isWalking = true;
        walkCounter = walkTime;
    }
}
