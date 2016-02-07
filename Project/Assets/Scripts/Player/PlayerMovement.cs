using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public bool facingRight = true;
    public float maxSpeed = 10f;
    private Rigidbody2D rigidBody2D;
    public float jumpForce = 200f;

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
        PlayerInputHandler();
    }

    private void PlayerInputHandler()
    {
        if (grounded && Input.GetButton("Jump"))
        {
            anim.SetBool("Ground", false);
            rigidBody2D.AddForce(new Vector2(0, jumpForce));
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


}
