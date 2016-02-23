using UnityEngine;
using System.Collections;
using Assets.Scripts.Environment;

public class EnemyController : MonoBehaviour
{
    public Transform player_transform;
    public float maxSpeed = 10f;
    public float stop = 0.5f;
    public float move_speed = 10;
    public WalkableDetector groundDetector;

    private bool facingRight = true;
    private Rigidbody2D rigidBody2D;
    private bool grounded = false;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        player_transform = EntityManager.GetRandomPlayer().transform;
        anim = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        groundDetector = GetComponent<WalkableDetector>();
    }

    void FixedUpdate()
    {
        //Checking if the character is grounded
        grounded = groundDetector.CheckGround();
        anim.SetBool("Ground", grounded);
        Vector3 direction = new Vector3(0,0,0);

        if ((player_transform.position - transform.position).magnitude > stop)
        {
            //move towards the player
            direction = (player_transform.position - transform.position)/(player_transform.position - transform.position).sqrMagnitude;
            transform.position += direction * move_speed * Time.deltaTime;
        }

        anim.SetFloat("Speed", Mathf.Abs(direction.x));
        anim.SetFloat("vSpeed", Mathf.Abs(direction.y));

        //Character moving right, but facing left
        if (direction.x < 0 && !facingRight)
        {
            Flip();
        }
        //character moving to the left but facing the right
        else if (direction.x > 0 && facingRight)
        {
            Flip();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
