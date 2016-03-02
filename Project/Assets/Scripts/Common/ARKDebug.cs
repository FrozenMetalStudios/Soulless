using UnityEngine;
using System.Collections;
using Assets.Scripts.Utility;

namespace Assets.Scripts.Common
{
    public class ARKDebug : MonoBehaviour
    {
        public GameObject managers;
        public GenericPreSceneCheck preSceneCheckScript;

        // Use this for initialization
        void Awake()
        {
            ARKStatus status = ARKStatus.ARK_ERROR_GENERAL;

            if (null == SceneLoader.Singleton)
            {
                Instantiate(managers);
                ARKLogger.LogMessage(eLogCategory.General,
                                     eLogLevel.Info,
                                     "Instantiated Debug Managers.");
            }

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
    }
    }
}
