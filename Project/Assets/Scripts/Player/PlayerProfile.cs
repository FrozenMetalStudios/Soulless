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

    private Abilities attack1;                  //Equipped attack 1 ability
    private Abilities attack2;                  //Equipped attack 2 ability
    private Abilities spell1;                   //Equipped spell 1 ability
    private Abilities spell2;                   //Equipped spell 2 ability
    private Abilities spell3;                   //Equipped spell 3 ability
    private Abilities ultimate;                 //Equipped ultimate ability

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

    //Player ability getter and setters
    public Abilities Attack1
    {
        get { return attack1; }
        set { attack1 = value; }
    }
    public Abilities Attack2
    {
        get { return attack2; }
        set { attack2 = value; }
    }
    public Abilities Spell1
    {
        get { return spell1; }
        set { spell1 = value; }
    }
    public Abilities Spell2
    {
        get { return spell2; }
        set { spell2 = value; }
    }
    public Abilities Spell3
    {
        get { return spell3; }
        set { spell3 = value; }
    }
    public Abilities Ultimate
    {
        get { return ultimate; }
        set { ultimate = value; }
    }
    #endregion

    //on player load setup abilities
    void Start()
    {
        //Abilities(damage, cooldown, InputTag, sprite image)
        //Resources.Load<Sprite>("Sprites/Demon/BasicAttack1");
        attack1 = new Abilities(ePlayerAbilities.BasicAttack1, AbilityType.Light, 
                                5, 1f, "BasicAttack1", 
                                Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        attack2 = new Abilities(ePlayerAbilities.BasicAttack2, AbilityType.Dark, 
                                10, 3f, "BasicAttack2", 
                                Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        spell1 = new Abilities(ePlayerAbilities.Spell1, AbilityType.Light, 
                                20, 5f, "Ability1", 
                                Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        spell2 = new Abilities(ePlayerAbilities.Spell2, AbilityType.Dark, 
                                25, 5f, "Ability2", 
                                Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        spell3 = new Abilities(ePlayerAbilities.Spell3, AbilityType.Dark, 
                                40, 10f, "Ability3", 
                                Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));

        ultimate = new Abilities(ePlayerAbilities.Ultimate, AbilityType.Light, 
                                80, 60f, "Ultimate", 
                                Resources.Load<Sprite>("Sprites/Demon/Abilities/BasicAttacks/BasicAttack1"));
    }

    //Determines what ability is being casted
    public Abilities determineAbility(ePlayerAbilities ability)
    {
        switch (ability)
        {
            case ePlayerAbilities.BasicAttack1:
                return attack1;
            case ePlayerAbilities.BasicAttack2:
                return attack2;
            case ePlayerAbilities.Spell1:
                return spell1;
            case ePlayerAbilities.Spell2:
                return spell2;
            case ePlayerAbilities.Spell3:
                return spell3;
            case ePlayerAbilities.Ultimate:
                return ultimate;
            default:
                return null;
        }
    }
}
