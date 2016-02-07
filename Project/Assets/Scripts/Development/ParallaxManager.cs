using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Environment.Parallax
{
    public class ParallaxManager : MonoBehaviour, iSubmanager
    {
        // -- Class Constants ----------------------------------------------
        public const int MAX_DEPTH = 100;       //
        public const int MAX_FOREGROUND = 100;  //

        // -- Internal Variables -------------------------------------------
        private SubmanagerMajorState            _majorState;             //
        private Transform                       _cam;                    // Shorter reference to the main camera's transform.
        private Vector3                         _previousCamPos;         // The postion of the camera in the previous frame.
        private List<ParallaxController>        _parallaxControllers;    //
        private SubmanagerStateChangeCallback   _stateCallbacks;         //

        // -- Configurable Variables ---------------------------------------
        public float smoothing;              // How smooth the parallax effect should be.
        public string identity;               //


        // -- Function Implementations -------------------------------------
        string iSubmanager.Identify()
        {
            return this.identity;
        }

        SubmanagerStatus iSubmanager.Init(ManagerContext context)
        {
            // Set the Initial Major State
            this._majorState = SubmanagerMajorState.INIT;

            // Setting up the reference shortcut.
            this._cam = Camera.main.transform;

            // Create a list for stored ParallaxControllers
            this._parallaxControllers = new List<ParallaxController>();

            // The 'previous frame' had the current frame's camera position.
            this._previousCamPos = this._cam.position;

            // Return success
            return SubmanagerStatus.ERROR_NONE;
        }

        SubmanagerStatus iSubmanager.RegisterStateChangeHandler(SubmanagerStateChangeCallback handler)
        {
            // Register the Handler
            _stateCallbacks += handler;

            // Return success
            return SubmanagerStatus.ERROR_NONE;
        }

        public void RegisterParallaxController(ParallaxController parallaxController)
        {
            this._parallaxControllers.Add(parallaxController);
        }

        public void UnregisterParallaxController(ParallaxController parallaxController)
        {
            this._parallaxControllers.Remove(parallaxController);
        }

        SubmanagerStatus iSubmanager.Enable(ManagerContext context)
        {
            // Transition to ACTIVE
            this._majorState = SubmanagerMajorState.ACTIVE;

            // Report the state change
            this._stateCallbacks(this._majorState);

            // Return success
            return SubmanagerStatus.ERROR_NONE;
        }

        SubmanagerStatus iSubmanager.Disable(ManagerContext context)
        {
            // Transition to INACTIVE
            this._majorState = SubmanagerMajorState.INACTIVE;

            // Report the state change
            this._stateCallbacks(this._majorState);

            // Return success
            return SubmanagerStatus.ERROR_NONE;
        }

        SubmanagerStatus iSubmanager.Shutdown(ManagerContext context)
        {
            // Transition to SHUTDOWN
            this._majorState = SubmanagerMajorState.SHUTDOWN;

            // Report the state change
            this._stateCallbacks(this._majorState);

            // Return success
            return SubmanagerStatus.ERROR_NONE;
        }

        void Update()
        {
            switch(this._majorState)
            {
                case SubmanagerMajorState.ACTIVE:
                    this.UpdateParallax();
                    break;

                default:
                    break;
            }
        }

        void UpdateParallax()
        {
            // The parallax is the opposite of the camera movement since the previous frame multiplied by the scale.
            float parallax = this._cam.position.x - this._previousCamPos.x;

            // For each successive background...
            for (int i = 0; i < this._parallaxControllers.Count; i++)
            {
                this._parallaxControllers[i].PerformParallax(parallax);
            }

            // Set the previousCamPos to the camera's position at the end of this frame.
            this._previousCamPos = this._cam.position;
        }
    }
}
