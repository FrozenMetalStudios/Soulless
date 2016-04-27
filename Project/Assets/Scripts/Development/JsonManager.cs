using UnityEngine;
using System.Collections;
using System;
using System.IO;
using ARK.Player.Ability;
using ARK.Player.Ability.Effects;
using Assets.Scripts.Utility;
using SimpleJSON;




/// <summary>
/// Base Class for json parsers
/// </summary>
/// <typeparam name="T"></typeparam>
abstract public class  Json<T>
{
    public JSONNode file = null;

    /// <summary>
    /// Load the specified ability
    /// </summary>
    /// <param name="id"> JSON id for given ability </param>
    /// <returns> The specified object </returns>
    abstract public T Load(string id);

    /// <summary>
    /// Save the given type object to the specified path
    /// </summary>
    /// <param name="item">Object to be saved to JSON file </param>
    /// <param name="path"> location to be saved </param>
    abstract public void Save(T item, string path);

    // <summary>
    // These functions convert extracted strings of special types to their respective Enumerations
    // </summary>
    #region String to Enumeration Conversion Functions
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
    #endregion
}

/// <summary>
/// This class extracts information related to Player Abilities from JSON files
/// </summary>
public class PlayerAbilityInformation: Json<Ability>
{

    /// <summary>
    /// Default Contructor
    /// </summary>
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
        ability.Effect = LoadEffectInfo(id);


        return ability;
    }



    /// <summary>
    /// Loads ability stat information into AbilityStats object
    /// </summary>
    /// <param name="id">the specified ability id</param>
    /// <returns> the abilitystats object with specified information </returns>
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


    /// <summary>
    /// Loads ability information into AbilityInformation object
    /// </summary>
    /// <param name="id">the specified ability id</param>
    /// <returns>the abilityinformation object with specified information </returns>
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


    /// <summary>
    /// Loads effect information into AbilityEffect object
    /// </summary>
    /// <param name="id"> ability id</param>
    /// <returns> AbilityEffect object with specified information</returns>
    private AbilityEffect LoadEffectInfo(string id)
    {
        AbilityEffect effect = new AbilityEffect();
        effect.effectkey = determineEffect(file[id]["effect"]["effectkey"]);

        return effect;
    }
}

/// <summary>
/// This class extracts information related to Player Statisitics from JSON files
/// </summary>
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

    /// <summary>
    /// Loads specified JSOn file into JSONNode for easy parsing
    /// </summary>
    /// <param name="path"> path of json file</param>
    public void LoadFile(string path)
    {
        string text = File.ReadAllText(path);
        file = JSON.Parse(text);
        AbilityParser.file = file;
        StatsParsers.file = file;
    }
}
