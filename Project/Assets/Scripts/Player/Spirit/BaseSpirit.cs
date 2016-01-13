using UnityEngine;
using System.Collections;
using PlayerAbilities;

public class BaseSpirit : MonoBehaviour {

    private string spiritName;
    private string classType;
    private int exp;
    private int level;
    private Ability[] spiritAbilities;

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
    public Ability[] SpiritAbilities
    {
        get { return spiritAbilities; }
        set { spiritAbilities = value; }
    }
}
