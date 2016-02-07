using System;
using UnityEngine;


namespace Assets.Scripts.Environment
{
    public class ScrollingReplicator : MonoBehaviour
    {
        private float x_offset = 0;
        private Vector3 last_position = new Vector3( 0, 0, 0 );
        public Transform reference_transform;

        void Start()
        {
            last_position = this.gameObject.transform.position;
        }

        void Update()
        {
            x_offset = (last_position - reference_transform.position).x;
            last_position = reference_transform.position;

            Bounds bounds = GetComponent<SpriteRenderer>().sprite.bounds;
            if (x_offset > 0)
            {
                
            }
            else
            {

            }
        }
    }
}
