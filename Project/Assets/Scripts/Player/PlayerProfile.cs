using UnityEngine;
using ARK.Player.Ability;
using ARK.Player.Ability.Manager;

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
    private BaseDemon demon;                    //Demon profile 
    private BaseSpirit spirit;                  //Spirit profile

    private int lightPoints;                    //Number of light points player currently has
    private int darkPoints;                     //Number of dark points player currently has

    public Ability attack1;                  //Equipped attack 1 ability
    public Ability attack2;                  //Equipped attack 2 ability
    public Ability spell1;                   //Equipped spell 1 ability
    public Ability spell2;                   //Equipped spell 2 ability
    public Ability spell3;                   //Equipped spell 3 ability
    public Ability ultimate;                 //Equipped ultimate ability

    string[] ids = {
            "A1-ML-DK-DM-005-001-0",
            "A2-ML-LT-DM-005-001-0",
            "S1-ML-LT-DM-032-00A-0",
            "S2-ML-DK-DM-032-00C-0",
            "S3-ML-DK-DM-104-014-0",
            "UL-ML-DK-DM-3E8-014-0"
        };

    private AbilityManager _AbilityManager;


    #region player getters and setters
    //Player Demon and Spirit profile getters and setters
    public BaseDemon Demon
    {
        get { return demon; }
        set { demon = value; }
    }

    //Player Light and Dark Point Getters and setters
    public int LightPoints
    {
        get { return lightPoints; }
        set { lightPoints = value; }
    }
    public int DarkPoints
    {
        get { return darkPoints; }
        set { darkPoints = value; }
    }
    #endregion

    //on player load setup abilities
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        energyRegen = 5.0f;
        corruptionDegen = 1.0f;
        _AbilityManager = new AbilityManager();


        //Profile must pares JSON file that lays out which abilities the player has intially equipped
        //Create a JSON parser script

        //Parse the ids through the AbilityManager to obtain a ability which the profile will hold

        //Abilities(damage, cooldown, InputTag, sprite image)
        LoadPlayerAbilities(ids);

    }


    /// <summary>
    /// Loads player abilities into appropraite slots
    /// </summary>
    /// <param name="ids">array of ability ids to be constructed</param>
    private void LoadPlayerAbilities(string[] ids)
    {
        //Construct player ability objects
        attack1 = _AbilityManager.ConstructAbility(ids[0]);
        attack2 = _AbilityManager.ConstructAbility(ids[1]);
        spell1 = _AbilityManager.ConstructAbility(ids[2]);
        spell2 = _AbilityManager.ConstructAbility(ids[3]);
        spell3 = _AbilityManager.ConstructAbility(ids[4]);
        ultimate = _AbilityManager.ConstructAbility(ids[5]);

        //Load animations into approprate slots
        /*
        LoadAbilityAnimations(attack1);
        LoadAbilityAnimations(attack2);
        LoadAbilityAnimations(spell1);
        LoadAbilityAnimations(spell2);
        LoadAbilityAnimations(spell3);
        LoadAbilityAnimations(ultimate);
    */
    }

    /// <summary>
    /// Loads the animation clips into animator
    /// </summary>
    /// <param name="ability">abilitiy</param>
    private void  LoadAbilityAnimations(Ability ability)
    {
        RuntimeAnimatorController currentController = anim.runtimeAnimatorController;
        AnimatorOverrideController overrideController = new AnimatorOverrideController();

        overrideController.runtimeAnimatorController = currentController;
        UnityEngine.Debug.Log(ability.DevInformation.animationpath);
        AnimationClip newAnim = Resources.Load<AnimationClip>(ability.DevInformation.animationpath);
        UnityEngine.Debug.Log(overrideController[ability.DevInformation.animationKey].name);
        UnityEngine.Debug.Log(newAnim.name.ToString());
        overrideController[ability.DevInformation.animationKey] = newAnim;

        anim.runtimeAnimatorController = overrideController;
    }
    //Determines what ability is being casted
    public Ability determineAbility(eEquippedSlot ability)
    {
        switch (ability)
        {
            case eEquippedSlot.AttackSlot1:
                return attack1;
            case eEquippedSlot.AttackSlot2:
                return attack2;
            case eEquippedSlot.SpellSlot1:
                return spell1;
            case eEquippedSlot.SpellSlot2:
                return spell2;
            case eEquippedSlot.SpellSlot3:
                return spell3;
            case eEquippedSlot.UltimateSlot:
                return ultimate;
            default:
                return null;
        }
    }
}
