using UnityEngine;
using System.Collections;
using System;
using System.IO;
using ARK.Player.Ability;
using ARK.Player.Ability.Effects;
using Assets.Scripts.Utility;
using SimpleJSON;




abstract public class  Json<T>
{
    public JSONNode file = null;
    abstract public T Load(string id);
    abstract public void Save(T item, string path);
    abstract public void Add(T item, string path);
    abstract public void Remove(T item, string path);

    public eEffectType determineEffect(string type)
    {
        if (String.Equals(type, "Damage", StringComparison.OrdinalIgnoreCase)) return eEffectType.Damage;
        if (String.Equals(type, "Stun", StringComparison.OrdinalIgnoreCase)) return eEffectType.Stun;
        if (String.Equals(type, "Slow", StringComparison.OrdinalIgnoreCase)) return eEffectType.Slow;
        if (String.Equals(type, "DamageOverTime", StringComparison.OrdinalIgnoreCase)) return eEffectType.DamageOverTime;
        else return eEffectType.undefined;
    }
    public eEquippedSlot determineEquippedSlot(string type)
    {
        if (String.Equals(type, "AttackSlot1", StringComparison.OrdinalIgnoreCase)) return eEquippedSlot.AttackSlot1;
        if (String.Equals(type, "AttackSlot2", StringComparison.OrdinalIgnoreCase)) return eEquippedSlot.AttackSlot2;
        if (String.Equals(type, "SpellSlot1", StringComparison.OrdinalIgnoreCase)) return eEquippedSlot.SpellSlot1;
        if (String.Equals(type, "SpellSlot2", StringComparison.OrdinalIgnoreCase)) return eEquippedSlot.SpellSlot2;
        if (String.Equals(type, "SpellSlot3", StringComparison.OrdinalIgnoreCase)) return eEquippedSlot.SpellSlot3;
        if (String.Equals(type, "Ultimate", StringComparison.OrdinalIgnoreCase)) return eEquippedSlot.UltimateSlot;
        else return eEquippedSlot.undefined;
    }
    public eAbilityCast determineAbilityCast(string type)
    {
        if (String.Equals(type, "light", StringComparison.OrdinalIgnoreCase)) return eAbilityCast.Light;
        if (String.Equals(type, "dark", StringComparison.OrdinalIgnoreCase)) return eAbilityCast.Dark;
        else return eAbilityCast.undefined;
    }
    public eAbilityType determineAbilityType(string type)
    {
        if (String.Equals(type, "Melee", StringComparison.OrdinalIgnoreCase)) return eAbilityType.Melee;
        if (String.Equals(type, "Ranged", StringComparison.OrdinalIgnoreCase)) return eAbilityType.Ranged;
        if (String.Equals(type, "SelfBuff", StringComparison.OrdinalIgnoreCase)) return eAbilityType.SelfBuff;
        if (String.Equals(type, "Mobility", StringComparison.OrdinalIgnoreCase)) return eAbilityType.Mobility;
        if (String.Equals(type, "Transform", StringComparison.OrdinalIgnoreCase)) return eAbilityType.Transform;
        else return eAbilityType.undefined;
    }
}

public class PlayerAbilityInformation: Json<Ability>
{
    public PlayerAbilityInformation()
    {
    }

    public  override void Save(Ability stats, string path)
    {

    }

    public override Ability Load(string id)
    {
        Ability ability = new Ability();
        ability.slot = determineEquippedSlot(file[id]["slot"]);
        ability.type = determineAbilityType(file[id]["type"]);
        ability.cast = determineAbilityCast(file[id]["cast"]);

        ARKLogger.LogMessage(eLogCategory.General, eLogLevel.Info, ability.slot.ToString());
        ARKLogger.LogMessage(eLogCategory.General, eLogLevel.Info, ability.type.ToString());
        ARKLogger.LogMessage(eLogCategory.General, eLogLevel.Info, ability.cast.ToString());

        ability.Statistics = LoadStats(id);
        ability.DevInformation = LoadDevInfo(id);



        return ability;
    }

    public override void Add(Ability item, string path)
    {
        throw new NotImplementedException();
    }
    public override void Remove(Ability item, string path)
    {
        throw new NotImplementedException();
    }

    private AbilityStats LoadStats(string id)
    {
        AbilityStats stats = new AbilityStats();
        if (file != null)
        {
            ARKLogger.LogMessage(eLogCategory.General, eLogLevel.Info, "Loading Statistics");
            stats = JsonUtility.FromJson<AbilityStats>(file[id]["statistics"].ToString());

            ARKLogger.LogMessage(eLogCategory.General, eLogLevel.Info, stats.name);
            ARKLogger.LogMessage(eLogCategory.General, eLogLevel.Info, stats.damage.ToString());
            ARKLogger.LogMessage(eLogCategory.General, eLogLevel.Info, stats.modifier.ToString());
            ARKLogger.LogMessage(eLogCategory.General, eLogLevel.Info, stats.cooldown.ToString());
            ARKLogger.LogMessage(eLogCategory.General, eLogLevel.Info, stats.energy.ToString());
            ARKLogger.LogMessage(eLogCategory.General, eLogLevel.Info, stats.corruption.ToString());

        }
        else
        {
            ARKLogger.LogMessage(eLogCategory.General, eLogLevel.Assert, "JSON File is null!");
        }
        return stats;
    }

    private AbilityInformation LoadDevInfo(string id)
    {
        AbilityInformation devinfo = new AbilityInformation();
        if (file != null)
        {
            ARKLogger.LogMessage(eLogCategory.General, eLogLevel.Info, "Loading Dev Info");
            devinfo = JsonUtility.FromJson<AbilityInformation>(file[id]["information"].ToString());
            devinfo.hitbox = JsonUtility.FromJson<AbilityHitBox>(file[id]["information"]["hitbox"].ToString());

            ARKLogger.LogMessage(eLogCategory.General, eLogLevel.Info, devinfo.animationKey);
            ARKLogger.LogMessage(eLogCategory.General, eLogLevel.Info, devinfo.description);
            ARKLogger.LogMessage(eLogCategory.General, eLogLevel.Info, devinfo.hitbox.length.ToString());
            ARKLogger.LogMessage(eLogCategory.General, eLogLevel.Info, devinfo.hitbox.height.ToString());
        }
        else
        {
            ARKLogger.LogMessage(eLogCategory.General, eLogLevel.Assert, "JSON File is null!");
        }
        return devinfo;
    }


}
public class PlayerInformation : Json<string>
{
    public PlayerInformation()
    {

    }
    public override void Save(string stats, string path)
    {

    }
    public override string Load(string id)
    {
        throw new NotImplementedException();
    }
    public override void Add(string item, string path)
    {
        throw new NotImplementedException();
    }
    public override void Remove(string item, string path)
    {
        throw new NotImplementedException();
    }
}

//JSON Manager
//<summary>
// Wrapper class for SimpleJSON Interface
// This wrapper will parse our game specific formats
//</summary>
public class JsonManager 
{
    public JSONNode file;
    public PlayerAbilityInformation AbilityParser;
    public PlayerInformation StatsParsers;

    public JsonManager()
    {
        AbilityParser = new PlayerAbilityInformation();
        StatsParsers = new PlayerInformation();

    }

    public void LoadFile(string path)
    {
        string text = File.ReadAllText(path);
        file = JSON.Parse(text);
        AbilityParser.file = file;
        StatsParsers.file = file;
    }
}
