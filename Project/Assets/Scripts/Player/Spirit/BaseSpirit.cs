using UnityEngine;
using System.Collections;
using PlayerAbilityTest;

public class BaseSpirit : MonoBehaviour {

    private string spiritName;
    private string classType;
    private int exp;
    private int level;
    private AbilityTest[] spiritAbilities;

    public string SpiritName
    {
        get { return spiritName; }
        set { spiritName = value; }
    }
    public string ClassType
    {
        get { return classType; }
        set { classType = value; }
    }
    public int spiritExp
    {
        get { return exp; }
        set { exp = value; }
    }
    public int Spiritlevel
    {
        get { return level; }
        set { level = value; }
    }
    public AbilityTest[] SpiritAbilities
    {
        get { return spiritAbilities; }
        set { spiritAbilities = value; }
    }
}
