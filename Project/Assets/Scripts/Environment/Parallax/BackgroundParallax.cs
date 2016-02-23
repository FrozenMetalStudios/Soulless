using System;
using UnityEngine;


namespace Assets.Scripts.Environment.Parallax
{
    public class BackgroundParallax : ParallaxController
    {
        [Range(0, ParallaxController.MAX_DEPTH)]
        public int parallaxDepth = 0;                // Specifies the layer depth

        override public void PerformParallax(float cameraTravel)
        {
            // ... set a target x position which is their current position plus the parallax multiplied by the reduction.
            float positionOffset = cameraTravel * (this.parallaxDepth * ((float)1 / ParallaxController.MAX_DEPTH));

            // Determine the new target X position
            float backgroundTargetPosX = this.gameObject.transform.position.x + positionOffset;

            // Create a target position which is the background's current position but with it's target x position.
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX,
                                                      this.gameObject.transform.position.y,
                                                      this.gameObject.transform.position.z);

            // Lerp the background's position between itself and it's target position.
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position,
                                                              backgroundTargetPos,
                                                              this.smoothing * Time.deltaTime);
        }
    }
}
