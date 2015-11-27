using UnityEngine;
using System.Collections;
using PlayerAbilities;

public class PlayerProfile : MonoBehaviour {

    public string playerName;
    public int playerHealth;
    private BaseDemon demon;
    private BaseSpirit spirit;
    private int lightPoints;
    private int darkPoints;

    private Abilities attack1;
    private Abilities attack2;
    private Abilities spell1;
    private Abilities spell2;
    private Abilities spell3;
    private Abilities ultimate;

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

    void Start()
    {
        attack1 = new Abilities(5, 1f, "BasicAttack1");
        attack2 = new Abilities(10, 3f, "BasicAttack2");
        spell1 = new Abilities(20, 5f, "Ability1");
        spell2 = new Abilities(25, 5f, "Ability2");
        spell3 = new Abilities(40, 10f, "Ability3");
    }

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
