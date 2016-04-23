using UnityEngine;
using System.Collections;
using Assets.Scripts.Utility;
using Assets.Scripts.Common.PreScene;

namespace Assets.Scripts.Common
{
    public class ARKDebug : MonoBehaviour
    {
        public GameObject managers;
        public GenericPreSceneCheck preSceneCheckScript;

        // --------------------------------------------------------------------
        static ARKDebug _Singleton = null;

        // --------------------------------------------------------------------
        public static ARKDebug Singleton
        {
            get { return _Singleton; }
        }

        // Use this for initialization
        void Awake()
        {
            ARKStatus status = ARKStatus.ARK_ERROR_GENERAL;

            if ((null == SceneLoader.Singleton) &&
                (null == _Singleton))
            {
                Instantiate(managers);
                ARKLogger.LogMessage(eLogCategory.General,
                                     eLogLevel.Info,
                                     "Instantiated Debug Managers.");
            }
            _Singleton = this;

            if (null != preSceneCheckScript)
            {
                status = preSceneCheckScript.runPreSceneChecks();
                if (ARKStatus.ARK_SUCCESS == status)
                {
                    ARKLogger.LogMessage(eLogCategory.General,
                                         eLogLevel.Info,
                                         "Pre-Scene Checks result: SUCCESS");
                }
                else
                {
                    ARKLogger.Assert(eLogCategory.General,
                                     "Pre-Scene Checks result: FAILED");
                }
            }
            else
            {
                ARKLogger.LogMessage(eLogCategory.General,
                                     eLogLevel.Info,
                                     "No Pre-Scene Checks run");
            }

            // Make sure this object persists between scene loads.
            DontDestroyOnLoad(gameObject);
        }
    }
}
