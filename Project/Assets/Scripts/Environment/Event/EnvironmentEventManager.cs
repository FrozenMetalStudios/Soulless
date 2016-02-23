using System;
using UnityEngine;
using Assets.Scripts.Environment;


namespace Assets.Scripts.Environment.Event
{
    public class EnvironmentEventManager : MonoBehaviour, iSubmanager
    {
        // -- Internal Variables -------------------------------------------
        private SubmanagerMajorState            _majorState;             //
        private SubmanagerStateChangeCallback   _stateCallbacks;         //

        // -- Configurable Variables ---------------------------------------
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
    }
}
