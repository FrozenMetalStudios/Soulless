using System;
using UnityEngine;


namespace Assets.Scripts.Environment.Parallax
{
    public abstract class ParallaxController : MonoBehaviour
    {
        // -- Class Constants ----------------------------------------------
        public const int MAX_FOREGROUND = 100;  //
        public const int MAX_DEPTH = 100;       //

        private Transform   _cam;                    // Shorter reference to the main camera's transform.
        private Vector3     _previousCamPos;         // The postion of the camera in the previous frame.

        public float smoothing = 8;                  // How smooth the parallax effect should be.

        public abstract void PerformParallax(float cameraTravel);


        void Awake()
        {
            // Setting up the reference shortcut.
            this._cam = Camera.main.transform;

            // The 'previous frame' had the current frame's camera position.
            this._previousCamPos = this._cam.position;
        }


        void Update()
        {
            // The parallax is the opposite of the camera movement since the previous frame multiplied by the scale.
            float parallax = this._cam.position.x - this._previousCamPos.x;
            
            // Update the parallax
            this.PerformParallax(parallax);

            // Set the previousCamPos to the camera's position at the end of this frame.
            this._previousCamPos = this._cam.position;
        }
    }
}
