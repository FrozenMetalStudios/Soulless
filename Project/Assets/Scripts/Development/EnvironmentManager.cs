using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Environment.Parallax;
using Assets.Scripts.Environment.Event;


namespace Assets.Scripts.Environment
{
    public class EnvironmentManager : MonoBehaviour, iSubmanager
    {
        // -- Internal Variables -------------------------------------------
        private SubmanagerMajorState            _majorState;             //
        private SubmanagerStateChangeCallback   _stateCallbacks;         //

        // -- Configurable Variables ---------------------------------------
        public string                   identity;                   //
        public ParallaxManager          parallaxManager;            //
        public EnvironmentEventManager  environmentEventManager;    //
        public List<EnvironmentLayer>   environmentLayers;          //


        // -- Function Implementations -------------------------------------
        string iSubmanager.Identify()
        {
            return this.identity;
        }


        SubmanagerStatus iSubmanager.Init(ManagerContext context)
        {
            // Set the Initial Major State
            this._majorState = SubmanagerMajorState.INIT;

            // Initialize any sub managers
            // Init the Parallax Manager
            // Init the Environment Event Manager
            // Process any Children Environment Layers

            // Configure the Parallax Manager with data from the Environment Layers
            // Configure the Environment Event Manager with data from the Environment Layers

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

        SubmanagerStatus iSubmanager.Enable(ManagerContext context)
        {
            // Enable the Submodules
            
            // Transition to ACTIVE
            this._majorState = SubmanagerMajorState.ACTIVE;

            // Report the state change
            this._stateCallbacks(this._majorState);

            // Return success
            return SubmanagerStatus.ERROR_NONE;
        }

        SubmanagerStatus iSubmanager.Disable(ManagerContext context)
        {
            // Disable the Submodules

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
    }
}
