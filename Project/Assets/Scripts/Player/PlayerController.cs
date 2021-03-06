﻿using UnityEngine;
using System.Collections;
using Assets.Scripts.Environment;

public class PlayerController : MonoBehaviour
{

    public bool facingRight = true;
    public float maxSpeed = 4f;
    public float jumpHeight;
    public WalkableDetector groundDetector;
    public float currHeight;
    public float currDepth;
    public Vector3 shadow;

    private Rigidbody2D rigidBody2D;
    private bool maxHeight = false;
    private bool grounded = true;
    private bool rightShGrounded = false;
    private bool leftShGrounded = false;
    private bool upShGrounded = false;
    private bool downShGrounded = false;
    private Animator anim;
    private float relHeight;
    private float horMove;
    private float verMove;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        groundDetector = GetComponent<WalkableDetector>();
    }

    void ProcessGroundCheck()
    {

        shadow.x = groundDetector.groundCheck.position.x;

        //Check around the character's shadow if it is walkable
        rightShGrounded = groundDetector.CheckRightGround(shadow);
        leftShGrounded = groundDetector.CheckLeftGround(shadow);
        upShGrounded = groundDetector.CheckUpGround(shadow);
        downShGrounded = groundDetector.CheckDownGround(shadow);

        anim.SetBool("Ground", grounded);

        //vertical speed of the character
        anim.SetFloat("vSpeed", rigidBody2D.velocity.y);

        horMove = Input.GetAxis("Horizontal");
        verMove = Input.GetAxis("Vertical");

        anim.SetFloat("Speed", Mathf.Abs(verMove + horMove));

        // if character is at left or right boundary
        if ((!leftShGrounded && horMove < 0) || (!rightShGrounded && horMove > 0))
        {
            horMove = 0;
            print("left or right bound");
        }

        // if character is at top or bottom boundary
        if ((!downShGrounded && verMove < 0) || (!upShGrounded && verMove > 0))
        {
            verMove = 0;
            print("up or down bound");
        }

        if (!grounded)
        {
            if (downShGrounded || upShGrounded)
            {
                jumpHeight += 0.05f * verMove;
                currDepth += 0.05f * verMove;
            }
            verMove = 0;
            shadow.y = currDepth;
        }

        //Character moving right, but facing left
        if (horMove > 0 && !facingRight)
        {
            Flip();
        }
        //character moving to the left but facing the right
        else if (horMove < 0 && facingRight)
        {
            Flip();
        }

    }

    // Update is called once per frame
    void Update()
    {
        ProcessGroundCheck();
        // update current depth only when the character is grounded
        if (grounded)
        {
            currDepth = groundDetector.CheckYPos();
            shadow = groundDetector.groundCheck.position;
        }
        //print(currDepth);
        currHeight = groundDetector.CheckYPos() - currDepth;

        // initiate jumping
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            jumpHeight = currHeight + 2;
            grounded = false;
            maxHeight = false;
            verMove = maxSpeed;
        }

        if (!grounded)
        {
            // if the character hasn't reached the maximum height, apply jump force
            if (!maxHeight)
            {

                relHeight = Mathf.Abs((jumpHeight - currHeight) / jumpHeight);
                verMove = (float)0.5*relHeight * maxSpeed;
                if ((relHeight < 0.01) || (currHeight >= jumpHeight) || (relHeight > 1))
                {
                    maxHeight = true;
                }
            }
            else
            {
                relHeight = Mathf.Abs((shadow.y - groundDetector.CheckYPos()) / shadow.y);
                verMove = -(float)0.2 *maxSpeed*(1-relHeight);
            }

            // if current y position is greater than current depth of the character 
            // character is still in the air.
            if (maxHeight)
            {
                if (groundDetector.CheckYPos() >= currDepth + 0.04*Mathf.Abs(currDepth))
                {
                    grounded = false;
                }
                else
                {
                    grounded = true;
                    maxHeight = false;

                }
            }

        }
        else
        {
            rigidBody2D.gravityScale = 0;
        }
        rigidBody2D.velocity = new Vector2(horMove * maxSpeed, verMove * maxSpeed);
        anim.SetBool("Ground", grounded);
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


}