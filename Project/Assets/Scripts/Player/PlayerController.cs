using UnityEngine;
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
