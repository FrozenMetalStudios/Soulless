using UnityEngine;
using System.Collections;
using PlayerAbilityTest;

public class BaseDemon : MonoBehaviour
{
    private string demonName;
    private string classType;
    private int exp;
    private int level;
    private AbilityTest[] demonAbilities;

    public string DemonName
    {
        get { return demonName; }
        set { demonName = value; }
    }
    public string ClassType
    {
        get { return classType; }
        set { classType = value; }
    }
    public int DemonExp
    {
        get { return exp; }
        set { exp = value; }
    }
    public int Demonlevel
    {
        get { return level; }
        set { level = value; }
    }
    public AbilityTest[] DemonAbilities
    {
        get { return demonAbilities; }
        set { demonAbilities = value; }
    }
}

