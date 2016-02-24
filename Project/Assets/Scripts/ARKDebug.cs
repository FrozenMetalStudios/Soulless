using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class ARKDebug : MonoBehaviour
    {
        public GameObject managers;

        // Use this for initialization
        void Awake()
        {
            if (null == SceneLoader.Singleton)
            {
                Instantiate(managers);
            }
    }
    }
}
