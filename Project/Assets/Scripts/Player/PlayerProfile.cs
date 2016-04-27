using UnityEngine;
using System.Collections;
using PlayerAbilityTest;
using ARK.Player.Ability;
using ARK.Player.Ability.Manager;
using ARK.Player.Ability.Builders;

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

    //Implementation of Abilities Using Builder Functionality
    public Ability slot1;
    public Ability slot2;
    public Ability slot3;
    public Ability slot4;
    public Ability slot5;
    public Ability slot6;

    private AbilityManager _AbilityManager;


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
        _AbilityManager = new AbilityManager();

        //Profile must pares JSON file that lays out which abilities the player has intially equipped
        //Create a JSON parser script

        //Parse the ids through the AbilityManager to obtain a ability which the profile will hold

        #region Ability setup
        //Abilities(damage, cooldown, InputTag, sprite image)
        attack1 = new AbilityTest(PlayerAbilityTest.eEquippedSlot.Attack1, PlayerAbilityTest.eAbilityCast.Light, 
                                5, 0.25f, 0, 5, Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        attack2 = new AbilityTest(PlayerAbilityTest.eEquippedSlot.Attack2, PlayerAbilityTest.eAbilityCast.Dark, 
                                10, 3f, 5, 20, Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        spell1 = new AbilityTest(PlayerAbilityTest.eEquippedSlot.Spell1, PlayerAbilityTest.eAbilityCast.Light, 
                                20, 5f, 10, 20, Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        spell2 = new AbilityTest(PlayerAbilityTest.eEquippedSlot.Spell2, PlayerAbilityTest.eAbilityCast.Dark, 
                                25, 5f, 20, 25, Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        spell3 = new AbilityTest(PlayerAbilityTest.eEquippedSlot.Spell3, PlayerAbilityTest.eAbilityCast.Dark, 
                                40, 10f, 30, 50, Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        ultimate = new AbilityTest(PlayerAbilityTest.eEquippedSlot.Ultimate, PlayerAbilityTest.eAbilityCast.Light, 
                                80, 60f, 60, 80, Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        #endregion

        _AbilityManager.ConstructAbility("0x0001");

    }

    //Determines what ability is being casted
    public AbilityTest determineAbility(PlayerAbilityTest.eEquippedSlot ability)
    {
        switch (ability)
        {
            case PlayerAbilityTest.eEquippedSlot.Attack1:
                return attack1;
            case PlayerAbilityTest.eEquippedSlot.Attack2:
                return attack2;
            case PlayerAbilityTest.eEquippedSlot.Spell1:
                return spell1;
            case PlayerAbilityTest.eEquippedSlot.Spell2:
                return spell2;
            case PlayerAbilityTest.eEquippedSlot.Spell3:
                return spell3;
            case PlayerAbilityTest.eEquippedSlot.Ultimate:
                return ultimate;
            default:
                return null;
        }
    }
}
