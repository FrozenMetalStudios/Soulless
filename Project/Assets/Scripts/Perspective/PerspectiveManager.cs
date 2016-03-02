using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Utility;
using Assets.Scripts.Perspective.Cameras;


namespace Assets.Scripts.Perspective
{
    public class PerspectiveManager : MonoBehaviour
    {
        private List<AbstractCamera> registeredCameras = new List<AbstractCamera>();

        // --------------------------------------------------------------------
        static PerspectiveManager _Singleton = null;

        // --------------------------------------------------------------------
        public static PerspectiveManager Singleton
        {
            get { return _Singleton; }
        }

        void Awake()
        {
            // Ensure only 1 singleton
            if (null != _Singleton)
            {
                ARKLogger.LogMessage(eLogCategory.Control,
                                     eLogLevel.System,
                                     "PerspectiveManager: Multiple PerspectiveManagers violate Singleton pattern.");
            }
            _Singleton = this;
        }

        #region Unimplemented Unity Callbacks
        // Use this for initialization
        void Start() { }

        // Update is called once per frame
        void Update() { }
        #endregion

        public void RegisterOnCameraBound(AbstractCamera camera)
        {
            registeredCameras.Add(camera);
        }

        public void DeregisterOnCameraBound(AbstractCamera camera)
        {
            registeredCameras.Remove(camera);
        }

        private void DetermineRestriction(float viewport, eCameraBoundEntryAxis entryVector, ref eAxisRestriction restriction)
        {
            if (((viewport > 0.5F) &&
                ((entryVector == eCameraBoundEntryAxis.CameraBoundEntryNegative) || (entryVector == eCameraBoundEntryAxis.CameraBoundEntryBoth))) || 
                ((viewport < 0.5F) &&
                ((entryVector == eCameraBoundEntryAxis.CameraBoundEntryPositive) || (entryVector == eCameraBoundEntryAxis.CameraBoundEntryBoth))))
            {
                if (entryVector == eCameraBoundEntryAxis.CameraBoundEntryBoth)
                {
                    restriction = eAxisRestriction.AxisRestrictionBoth;
                }
                else if (entryVector == eCameraBoundEntryAxis.CameraBoundEntryNegative)
                {
                    restriction = eAxisRestriction.AxisRestrictionPositive;
                }
                else if (entryVector == eCameraBoundEntryAxis.CameraBoundEntryPositive)
                {
                    restriction = eAxisRestriction.AxisRestrictionNegative;
                }
            }
        }

        public void OnCameraBoundEntry(CameraBound cameraBound)
        {
            ARKLogger.LogMessage(eLogCategory.Control,
                                 eLogLevel.Info,
                                 "OnCameraBoundEntry");

            foreach(AbstractCamera cam in registeredCameras)
            {
                if (cameraBound.GetComponent<Renderer>().IsVisibleFrom(cam.GetComponent<Camera>()))
                {
                    CameraMovementRestrictions restriction = new CameraMovementRestrictions();
                    Vector3 viewPos = cam.GetComponent<Camera>().WorldToViewportPoint(cameraBound.transform.position);
                    DetermineRestriction(viewPos.x, cameraBound.onXEntry, ref restriction.xAxis);
                    DetermineRestriction(viewPos.y, cameraBound.onYEntry, ref restriction.yAxis);
                    cam.RegisterBound(cam.GetInstanceID(), restriction);
                }
            }
        }

        public void OnCameraBoundExit(CameraBound cameraBound)
        {
            ARKLogger.LogMessage(eLogCategory.Control,
                                 eLogLevel.Info,
                                 "OnCameraBoundExit");

            foreach (AbstractCamera cam in registeredCameras)
            {
                if (!cameraBound.GetComponent<Renderer>().IsVisibleFrom(cam.GetComponent<Camera>()))
                {
                    ARKLogger.LogMessage(eLogCategory.Control,
                                         eLogLevel.Info,
                                         "Not Visible");
                    cam.DeregisterBound(cam.GetInstanceID());
                }
            }
        }
    }
}
