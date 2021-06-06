using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    
    float horizontallMovement;
    public float runSpeed;
    bool jump = false;
    bool crouch = false;
    public bool canMove = true;

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            horizontallMovement = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horizontallMovement));
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }

        //Crouch if we need it in the future
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }


    }

    void FixedUpdate()
    {
        controller.Move(horizontallMovement * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    public void ResetMoving()
    {
        canMove = true;
    }
}
