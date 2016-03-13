using UnityEngine;
using System.Collections;
using PlayerAbilityTest;
using Assets.Scripts.Utility;

public class CorruptionManager {
    public int corruptionMeter; // in stage corruption
    public int corruptionLevel; // overall character corruption which updated at the end of the stage

    private int stageCorLevel; // keep track of corruption usage during the stage
    private static int corruptionThresh = 75;
    private bool corrupted;

    static CorruptionManager _Singleton = null;

    public static CorruptionManager Singleton
    {
        get { return _Singleton; }
    }

    // initiate corruption manager

    void Start()
    {
        // Ensure only 1 singleton
        if (null != _Singleton)
        {
            UnityEngine.Debug.LogError("CorruptionManager: Multiple CorruptionManagers violate Singleton pattern.");
        }
        _Singleton = this;

        // Trace Startup
        ARKLogger.LogMessage(eLogCategory.Control,
                             eLogLevel.Info,
                             "CorruptionManager: Initiate.");
        corruptionMeter = 0;
        corruptionLevel = 0;
        stageCorLevel = 0;
        corrupted = false;
    }

    public void ModifyMeter(AbilityTest castedAbil)
    {
        if (castedAbil.cast == eAbilityCast.Light) {
            corruptionMeter += castedAbil.corruption;
            stageCorLevel += castedAbil.corruption;
        } else {
            corruptionMeter -= castedAbil.corruption;
            stageCorLevel += castedAbil.corruption;
        }
        if (corruptionMeter > 100) corruptionMeter = 100;
        if (corruptionMeter < 0) corruptionMeter = 0;
        if (corruptionMeter + corruptionLevel >= corruptionThresh) corrupted = true;
    }

    public void EndStage()
    {
        corruptionMeter = 0;
        corruptionLevel += stageCorLevel;
    }

}
