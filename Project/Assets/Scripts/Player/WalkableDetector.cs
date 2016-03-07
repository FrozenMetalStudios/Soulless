//#define TESTING_WITH_MOUSE
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Environment
{
    public class WalkableDetector : MonoBehaviour
    {
        public float alphaCutoff;               // Alpha range for walkable surfaces
        public Transform groundCheck;
        public LayerMask whatIsGround;        // Defines what the character considers ground (things he can land on)


#if TESTING_WITH_MOUSE
        public void FixedUpdate()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity, whatIsGround);
            CheckImages(hits);
        }
#endif // TEST_WITH_MOUSE

        private bool CheckImages(RaycastHit2D[] hits)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (null != hit.collider)
                {
                    // Gather information about the image
                    SpriteRenderer spriteRenderer = hit.transform.GetComponent<SpriteRenderer>();
                    Texture2D tex = spriteRenderer.sprite.texture;
                    Vector3 v = hit.transform.worldToLocalMatrix.MultiplyPoint3x4(hit.point);
                    Bounds bounds = hit.transform.GetComponent<SpriteRenderer>().sprite.bounds;

                    // Convert to a UV System
                    float xPic = bounds.size.x - (v.x + bounds.extents.x);
                    float yPic = v.y + bounds.extents.y;

                    // Grab the alpha
                    Color color = tex.GetPixel((int)((xPic / bounds.size.x) * tex.width), (int)((yPic / bounds.size.y) * tex.height));
                    float alpha = color.a;

                    if (alphaCutoff < alpha)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckGround()
        {
            Vector3 position = groundCheck.position;
            RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector3.forward, Mathf.Infinity, whatIsGround);
            return CheckImages(hits);
        }

        // for checkgin ground around the character adjust scale factor based on the character size
        public bool CheckRightGround(Vector3 shadow)
        {
            Vector3 position = shadow + Vector3.right;
            RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector3.forward, Mathf.Infinity, whatIsGround);
            return CheckImages(hits);
        }

        public bool CheckLeftGround(Vector3 shadow)
        {
            Vector3 position = shadow + Vector3.left;
            RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector3.forward, Mathf.Infinity, whatIsGround);
            return CheckImages(hits);
        }

        public bool CheckUpGround(Vector3 shadow)
        {
            Vector3 position = shadow + (Vector3.up)/5;
            RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector3.forward, Mathf.Infinity, whatIsGround);
            return CheckImages(hits);
        }

        public bool CheckDownGround(Vector3 shadow)
        {
            Vector3 position = shadow + (Vector3.down)/5;
            RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector3.forward, Mathf.Infinity, whatIsGround);
            return CheckImages(hits);
        }
        public float CheckYPos()
        {
            Vector3 position = groundCheck.position;
            return position.y;
        }
    }
}
