using UnityEngine;
using System;
using System.Collections.Generic;
using ARK.Player.Ability;
using ARK.Player.Ability.Manager;
using ARK.Utility.Ability;
using Assets.Scripts.Utility;

//Player Profile
//<summary>
// Contains all important information regarding the player
//</summary>
public class PlayerProfile : MonoBehaviour
{
    private Animator anim;
    public string playerName;                   //Players name points
    public float playerHealth;                  //Players health points
    public float maxEnergy;                     //Players Energy points
    public float maxCorruption;                 //Players corruption points
    public float energyRegen;              //Players energy regeneration 
    public float corruptionDegen;           //Players corruption degeneration rate 

    public PlayerHUDManager playerHUD;          //Players HUD manager
    public CorruptionManager corruptManager;    //inter-level corruption manager

    public int lightPoints;                    //Number of light points player currently has
    public int darkPoints;                     //Number of dark points player currently has

    public List<Ability> EquippedAbilities;     // Player Abilities that are equipped 

    string[] ids = {
            "A1-ML-DK-DM-005-001-0",
            "A2-ML-LT-DM-005-001-0",
            "S1-ML-DK-DT-005-00A-0",
            "S2-ML-DK-DM-032-00C-0",
            "S3-ML-DK-DM-104-014-0",
            "UL-ML-DK-DM-3E8-014-0"
        };

    private AbilityManager _AbilityManager;

    //on player load setup abilities
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        energyRegen = 5.0f;
        corruptionDegen = 1.0f;
        EquippedAbilities = new List<Ability>(Constants.MAX_EQUIPPABLE_ABILITIES);
        _AbilityManager = new AbilityManager();
        LoadPlayerAbilities(ids);

    }


    /// <summary>
    /// Loads player abilities into appropraite slots
    /// </summary>
    /// <param name="ids">array of ability ids to be constructed</param>
    private void LoadPlayerAbilities(string[] ids)
    {
        //Equip Player Abilities
        for(int i =0; i< Constants.MAX_EQUIPPABLE_ABILITIES; i++)
        {
            Ability temp = _AbilityManager.ConstructAbility(ids[i]);
            //Load ability animation
            LoadAbilityAnimations(temp);
            EquippedAbilities.Add(temp);
        }
    }

    /// <summary>
    /// Loads the animation clips into associated animation state by overriding the existing controller with a new one
    /// </summary>
    /// <param name="ability">abilitiy</param>
    private void  LoadAbilityAnimations(Ability ability)
    {
        RuntimeAnimatorController currentController = anim.runtimeAnimatorController;
        AnimatorOverrideController overrideController = new AnimatorOverrideController();

        overrideController.runtimeAnimatorController = currentController;
        ARKLogger.LogMessage(eLogCategory.Animation, eLogLevel.Info, "Loading player ability: " + ability.DevInformation.animationpath);

        AnimationClip newAnim = Resources.Load<AnimationClip>(ability.DevInformation.animationpath);
        overrideController[ability.DevInformation.animationKey] = newAnim;

        anim.runtimeAnimatorController = overrideController;
    }

    //Determines what ability is being casted
    public Ability DetermineAbility(eEquippedSlot slot)
    {
        return EquippedAbilities[Convert.ToInt32(slot)];
    }
}
