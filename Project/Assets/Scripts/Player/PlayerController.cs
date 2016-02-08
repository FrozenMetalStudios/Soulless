<<<<<<< HEAD
﻿using UnityEngine;
using System.Collections;


//Player Controller
//<summary>
//Enemy attacking script
//</summary>
public class PlayerController : MonoBehaviour {

    public bool facingRight = true;
    public float maxSpeed = 10f;
    private Rigidbody2D rigidBody2D;
    public float jumpForce = 700f;
    private bool doubleJump = false;


    private bool grounded = false;
    public Transform groundCheck;
    private float groundRadius = 0.2f;
    public LayerMask whatIsGround;        //defines what the character considers ground (things he can land on)


    private Animator anim;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Checking if the character is grounded
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        if (grounded)
        {
            doubleJump = false;
        }
        //vertical speed of the character
        anim.SetFloat("vSpeed", rigidBody2D.velocity.y);

        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));
        rigidBody2D.velocity = new Vector2(move * maxSpeed, rigidBody2D.velocity.y);

        //Character moving right, but facing left
        if(move > 0 && !facingRight)
        {
            Flip();
        }
        //character moving to the left but facing the right
        else if(move < 0 && facingRight)
        {
            Flip();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if((grounded || !doubleJump) && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            rigidBody2D.AddForce(new Vector2(0, jumpForce));

            if (!doubleJump && !grounded)
            {
                doubleJump = true;
            }
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


}
=======
﻿using UnityEngine;
using System.Collections;
using Assets.Scripts.Environment;

public class PlayerController : MonoBehaviour
{

    public bool facingRight = true;
    public float maxSpeed = 10f;
    public float jumpForce = 700f;
    public WalkableDetector groundDetector;
    public float currHeight;
    public float targetHeight;

    private Rigidbody2D rigidBody2D;
    private bool doubleJump = false;
    private bool grounded = false;
    private bool rightGrounded = false;
    private bool leftGrounded = false;
    private bool upGrounded = false;
    private bool downGrounded = false;
    private Animator anim;
    private bool jumping = false;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        groundDetector = GetComponent<WalkableDetector>();
    }

    void FixedUpdate()
    {
        //Checking if the character is grounded
        grounded = groundDetector.CheckGround();

        //Check around the character if it is walkable
        rightGrounded = groundDetector.CheckRightGround();
        leftGrounded = groundDetector.CheckLeftGround();
        upGrounded = groundDetector.CheckUpGround();
        downGrounded = groundDetector.CheckDownGround();

        if (jumping)
        {
            grounded = false;
        }
        anim.SetBool("Ground", grounded);

        if (grounded)
        {
            doubleJump = false;
        }
        //vertical speed of the character
        anim.SetFloat("vSpeed", rigidBody2D.velocity.y);

        float horMove = Input.GetAxis("Horizontal");
        float verMove = Input.GetAxis("Vertical");
        anim.SetFloat("Speed", Mathf.Abs(verMove + horMove));

        // if character is at left or right boundary
        if ((!leftGrounded && horMove < 0) || (!rightGrounded && horMove > 0))
        {
            horMove = 0;
        }

        // if character is at top or bottom boundary
        if ((!downGrounded && verMove < 0) || (!upGrounded && verMove > 0))
        {
            verMove = 0;
        }
        rigidBody2D.velocity = new Vector2(horMove * maxSpeed, verMove * maxSpeed);

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
        currHeight = groundDetector.check_height();
        if ((grounded || !doubleJump) && Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            targetHeight = currHeight;
            currHeight += 0.5f;
            jumping = true;
            anim.SetBool("Ground", false);
            rigidBody2D.AddForce(new Vector2(0, jumpForce));

            if (!doubleJump && !grounded)
            {
                doubleJump = true;
            }
        }
        if (jumping)
        {
            rigidBody2D.gravityScale = 3;
            if (currHeight >= targetHeight)
            {
                anim.SetBool("Ground", false);
            }
            else
            {
                anim.SetBool("Ground", true);
                jumping = false;
            }
        }
        else
        {
            rigidBody2D.gravityScale = 0;
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


}
>>>>>>> refs/remotes/origin/master
