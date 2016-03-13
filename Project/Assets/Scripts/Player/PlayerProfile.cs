using UnityEngine;
using System.Collections;
using PlayerAbilityTest;

//Player Profile
//<summary>
// Contains all important information regarding the player
//</summary>
public class PlayerProfile : ScriptableObject
{
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

    public AbilityTest attack1;                  //Equipped attack 1 ability
    public AbilityTest attack2;                  //Equipped attack 2 ability
    public AbilityTest spell1;                   //Equipped spell 1 ability
    public AbilityTest spell2;                   //Equipped spell 2 ability
    public AbilityTest spell3;                   //Equipped spell 3 ability
    public AbilityTest ultimate;                 //Equipped ultimate ability

    #region player getters and setters
    //Player Demon and Spirit profile getters and setters
    public BaseDemon Demon
    {
        get { return demon; }
        set { demon = value; }
    }
    public BaseSpirit Spirit
    {
        get { return spirit; }
        set { spirit = value; }
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
        energyRegen = 5.0f;
        corruptionDegen = 1.0f;

        #region Ability setup
        //Abilities(damage, cooldown, InputTag, sprite image)
        attack1 = new AbilityTest(eEquippedSlot.Attack1, eAbilityCast.Light, 
                                5, 0.25f, 0, 5, Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        attack2 = new AbilityTest(eEquippedSlot.Attack2, eAbilityCast.Dark, 
                                10, 3f, 5, 20, Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        spell1 = new AbilityTest(eEquippedSlot.Spell1, eAbilityCast.Light, 
                                20, 5f, 10, 20, Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        spell2 = new AbilityTest(eEquippedSlot.Spell2, eAbilityCast.Dark, 
                                25, 5f, 20, 25, Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        spell3 = new AbilityTest(eEquippedSlot.Spell3, eAbilityCast.Dark, 
                                40, 10f, 30, 50, Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        ultimate = new AbilityTest(eEquippedSlot.Ultimate, eAbilityCast.Light, 
                                80, 60f, 60, 80, Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        #endregion
    }

    //Determines what ability is being casted
    public AbilityTest determineAbility(eEquippedSlot ability)
    {
        switch (ability)
        {
            case eEquippedSlot.Attack1:
                return attack1;
            case eEquippedSlot.Attack2:
                return attack2;
            case eEquippedSlot.Spell1:
                return spell1;
            case eEquippedSlot.Spell2:
                return spell2;
            case eEquippedSlot.Spell3:
                return spell3;
            case eEquippedSlot.Ultimate:
                return ultimate;
            default:
                return null;
        }
    }
}
