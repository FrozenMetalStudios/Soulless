using UnityEngine;
using Assets.Scripts.Environment;
using Assets.Scripts.Enemy.AI;


namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(WalkableDetector))]
    public class EnemyController : MonoBehaviour
    {
        private struct EnemyMovement
        {
            public float moveSpeed;
            public bool facingRight;
            public bool grounded;
        }

        // Internal Functional
        private EnemyMovement movement;

        public BehaviourController controller; 

        // External Functional
        public WalkableDetector groundDetector;

        // Internal References
        private Rigidbody2D rigidBody2D;
        private Animator anim;

        // Use this for initialization
        void Start()
        {
            groundDetector = GetComponent<WalkableDetector>();
        }
        

        // Update is called once per frame
        void Update()
        {
            controller.DetermineNewPosition(this);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void SetPosition(Vector3 newPosition)
        {
            transform.position = newPosition;
        }

        void Flip()
        {
            movement.facingRight = !movement.facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
