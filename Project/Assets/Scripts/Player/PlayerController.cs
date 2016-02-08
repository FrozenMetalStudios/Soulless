using UnityEngine;
using System.Collections;
using Assets.Scripts.Environment;

public class PlayerController : MonoBehaviour
{

    public bool facingRight = true;
    public float maxSpeed = 10f;
    public float jumpForce = 7000f;
    public float jumpHeight;
    public WalkableDetector groundDetector;
    public float currHeight;
    public float currDepth;

    private Rigidbody2D rigidBody2D;
    private bool maxHeight = false;
    private bool grounded = false;
    private bool rightGrounded = false;
    private bool leftGrounded = false;
    private bool upGrounded = false;
    private bool downGrounded = false;
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

        if (jumping)
        {
            jumpHeight += verMove;
            currDepth += 0.1f*verMove;
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
        // update current depth only when the character is grounded
        if (grounded)
            currDepth = groundDetector.check_yPos();

        currHeight = groundDetector.check_yPos() - currDepth;

        // initiate jumping
        if (grounded && Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            jumpHeight = currHeight + 5;
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
            if (groundDetector.check_yPos() >= currDepth)
            {
                anim.SetBool("Ground", false);
                jumping = true;
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
