using System;
using UnityEngine;
using Assets.Scripts.Environment.Event;
using Assets.Scripts.Environment.Parallax;


namespace Assets.Scripts.Environment
{
    public enum eEnvironmentLayerType : int
    {
        BackgroundLayer = 0,
        MainLayer,
        ForegroundLayer
    };


    public class EnvironmentLayer : MonoBehaviour
    {
        public eEnvironmentLayerType    type;
        public ParallaxController       parallaxController;
    }
}
