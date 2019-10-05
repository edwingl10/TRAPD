using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    float horizontalMove = 0f;
    public float runspeed = 40f;

	bool jump = false;
    bool moveLeft = false;
    bool moveRight;

    void Update()
    {
		horizontalMove = Input.GetAxisRaw("Horizontal") * runspeed;
        
		if (moveLeft)
		{
			horizontalMove = -runspeed;
		}
		else if (moveRight)
		{
			horizontalMove = runspeed;
		}
	
        animator.SetFloat("speed",Mathf.Abs(horizontalMove));

        
        if(Input.GetButtonDown("Jump")){
            jump = true;
            animator.SetBool("isJumping", true);
        }
    
    }

    public void OnLanding(){
        animator.SetBool("isJumping", false);
    }
    
    void FixedUpdate(){
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    //functions for buttons

    public void MoveLeft()
	{
		moveLeft = true;
	}
    public void NoMoveLeft()
	{
		moveLeft = false;
		horizontalMove = 0;
	}
    public void MoveRight()
	{
		moveRight = true;
	}
    public void NoMoveRight()
	{
		moveRight = false;
		horizontalMove = 0;
	}
    public void Jump()
	{
		jump = true;
		animator.SetBool("isJumping", true);
	}
}
