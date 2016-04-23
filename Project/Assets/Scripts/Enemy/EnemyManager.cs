using UnityEngine;
using Assets.Scripts.Utility;


public class EnemyManager : ScriptableObject
{
    // --------------------------------------------------------------------
    static EnemyManager _Singleton = null;

    // --------------------------------------------------------------------
    public static EnemyManager Singleton
    {
        get { return _Singleton; }
    }

    void Awake()
    {
        // Ensure only 1 singleton
        if (null != _Singleton)
        {
            ARKLogger.LogMessage(eLogCategory.General,
                                 eLogLevel.System,
                                 "EnemyManager: Multiple EnemyManager violate Singleton pattern.");
        }
        _Singleton = this;
    }
}
