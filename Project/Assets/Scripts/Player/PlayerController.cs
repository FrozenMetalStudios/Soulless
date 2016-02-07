using UnityEngine;
using System.Collections;
using Assets.Scripts.Environment;

public class PlayerController : MonoBehaviour {

    public bool facingRight = true;
    public float maxSpeed = 10f;
    public float jumpForce = 700f;
    public WalkableDetector groundDetector;

    private Rigidbody2D rigidBody2D;
    private bool doubleJump = false;
    private bool grounded = false;
    private Animator anim;


    // Use this for initialization
    void Start() 
    {
        EntityManager.RegisterPlayer(this);
        anim = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        groundDetector = GetComponent<WalkableDetector>();
    }

    void FixedUpdate()
    {
        //Checking if the character is grounded
        grounded = groundDetector.CheckGround();
        anim.SetBool("Ground", grounded);

        if (grounded)
        {
            doubleJump = false;
        }
        //vertical speed of the character
        anim.SetFloat("vSpeed", rigidBody2D.velocity.y);

        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));
        float vertical = Input.GetAxis("Vertical");
        rigidBody2D.velocity = new Vector2(move * maxSpeed, vertical * maxSpeed);

        //Character moving right, but facing left
        if (move > 0 && !facingRight)
        {
            Flip();
        }
        //character moving to the left but facing the right
        else if (move < 0 && facingRight)
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
