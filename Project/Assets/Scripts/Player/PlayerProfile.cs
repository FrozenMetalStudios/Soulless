using UnityEngine;
using System.Collections;
using PlayerAbilities;

//Player Profile
//<summary>
// Contains all important information regarding the player
//</summary>
public class PlayerProfile : MonoBehaviour {

    public string playerName;                   //Players name
    public int playerHealth;                    //Players health
    public PlayerHUDManager playerHUD;          //Players HUD manager
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
        /*
        attack1 = AbilityFactory.CreateAbility(eAbilityType.Melee);
        attack2 = AbilityFactory.CreateAbility(eAbilityType.Melee);
        spell1 = AbilityFactory.CreateAbility(eAbilityType.Melee);
        spell2 = AbilityFactory.CreateAbility(eAbilityType.Melee);
        spell3 = AbilityFactory.CreateAbility(eAbilityType.Melee);
        ultimate = AbilityFactory.CreateAbility(eAbilityType.Melee);
        */
        #region shit
        
        //Abilities(damage, cooldown, InputTag, sprite image)
        attack1 = new Ability(eEquippedSlot.Attack1, eAbilityCast.Light, 
                                5, 1f, Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        attack2 = new Ability(eEquippedSlot.Attack2, eAbilityCast.Dark, 
                                10, 3f,Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        spell1 = new Ability(eEquippedSlot.Spell1, eAbilityCast.Light, 
                                20, 5f, Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        spell2 = new Ability(eEquippedSlot.Spell2, eAbilityCast.Dark, 
                                25, 5f, Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        spell3 = new Ability(eEquippedSlot.Spell3, eAbilityCast.Dark, 
                                40, 10f, Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        ultimate = new Ability(eEquippedSlot.Ultimate, eAbilityCast.Light, 
                                80, 60f, Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));
                                
        #endregion
    }

    //Determines what ability is being casted
    public Ability determineAbility(eEquippedSlot ability)
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
