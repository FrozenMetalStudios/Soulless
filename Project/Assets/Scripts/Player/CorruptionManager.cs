using UnityEngine;
using System.Collections;
using PlayerAbilityTest;

public class CorruptionManager : MonoBehaviour {
    public int corruptionMeter; // in stage corruption
    public int corruptionLevel; // overall character corruption which updated at the end of the stage

    private int stageCorLevel; // keep track of corruption usage during the stage
    private static int corruptionThresh = 75;
    private bool corrupted;

    // initiate corruption manager
    void Start()
    {
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
