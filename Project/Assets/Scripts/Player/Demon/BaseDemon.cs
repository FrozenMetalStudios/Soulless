using UnityEngine;
using System.Collections;
using ARK.Player.Ability;

public class BaseDemon : MonoBehaviour
{
    private string demonName;
    private string classType;
    private int exp;
    private int level;
    private Ability[] demonAbilities;

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
    public Ability[] DemonAbilities
    {
        get { return demonAbilities; }
        set { demonAbilities = value; }
    }
}

