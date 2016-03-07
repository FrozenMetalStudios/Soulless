using UnityEngine;
using System.Collections;
using Assets.Scripts.Environment;

public class PlayerController : MonoBehaviour
{

    public bool facingRight = true;
    public float maxSpeed = 5f;
    public float jumpForce = 700f;
    public float jumpHeight;
    public WalkableDetector groundDetector;
    public float currHeight;
    public float currDepth;
    public Vector3 shadow;

    private Rigidbody2D rigidBody2D;
    private bool maxHeight = false;
    private bool grounded = false;
    private bool rightShGrounded = false;
    private bool leftShGrounded = false;
    private bool upShGrounded = false;
    private bool downShGrounded = false;
    private Animator anim;
    private bool jumping = false;
    private float relHeight;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        groundDetector = GetComponent<WalkableDetector>();
    }

    void ProcessGroundCheck()
    {
        //Checking if the character is grounded
        grounded = groundDetector.CheckGround();

        if (jumping)
        {
            grounded = false;
        }
        // update current depth only when the character is grounded
        if (grounded)
        {
            currDepth = groundDetector.CheckYPos();
            shadow = groundDetector.groundCheck.position;
        }
        shadow.x = groundDetector.groundCheck.position.x;

        //Check around the character's shadow if it is walkable
        rightShGrounded = groundDetector.CheckRightGround(shadow);
        leftShGrounded = groundDetector.CheckLeftGround(shadow);
        upShGrounded = groundDetector.CheckUpGround(shadow);
        downShGrounded = groundDetector.CheckDownGround(shadow);

        anim.SetBool("Ground", grounded);

        //vertical speed of the character
        anim.SetFloat("vSpeed", rigidBody2D.velocity.y);

        float horMove = Input.GetAxis("Horizontal");
        float verMove = Input.GetAxis("Vertical");

        anim.SetFloat("Speed", Mathf.Abs(verMove + horMove));

        // if character is at left or right boundary
        if ((!leftShGrounded && horMove < 0) || (!rightShGrounded && horMove > 0))
        {
            horMove = 0;
        }

        // if character is at top or bottom boundary
        if ((!downShGrounded && verMove < 0) || (!upShGrounded && verMove > 0))
        {
            verMove = 0;
        }

        if (jumping)
        {
            if (downShGrounded || upShGrounded)
            {
                jumpHeight += 0.05f * verMove;
                currDepth += 0.05f * verMove;
            }
            verMove = 0;
            shadow.y = currDepth;
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
        ProcessGroundCheck();
        currHeight = groundDetector.CheckYPos() - currDepth;

        // initiate jumping
        if (grounded && Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            jumpHeight = currHeight + 2;
            jumping = true;
            maxHeight = false;
            anim.SetBool("Ground", false);
        }

        if (jumping)
        {
            // if the character hasn't reached the maximum height, apply jump force
            if ((currHeight <= jumpHeight) && !maxHeight)
            {
                relHeight = Mathf.Abs((jumpHeight - currHeight) / jumpHeight);
                rigidBody2D.AddForce(new Vector2(0, jumpForce));
                if (relHeight < 0.05)
                {
                    maxHeight = true;
                }
            }
            else
            {
                // else apply gravity
                rigidBody2D.gravityScale = 20;
            }

            // if current y position is greater than current depth of the character 
            // character is still in the air.
            if (groundDetector.CheckYPos() >= currDepth)
            {
                anim.SetBool("Ground", false);
                jumping = true;
                print("Ground false");
            }
            else
            {
                anim.SetBool("Ground", true);
                jumping = false;
                print("Ground true");
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