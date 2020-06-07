using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Helper class for initializing items and its effects for player equipment,
/// add mod/skill scripts
/// </summary>
public class EffectManager : MonoBehaviour
{

    public static void InitializeEffect(Effect effect, GameObject obj)
    {
        Stats stats = obj.GetComponent<Stats>();
        Abilities abilities = obj.GetComponent<Abilities>();

        #region Ranges
        // Ranges
        if (effect.passiveMod == Effect.PassiveMod.IncreaseLightRange)
        {
            if (obj.GetComponent<LightSource>() == null)
            {
                obj.AddComponent<LightSource>();
            }
            stats.LightRangeMod(effect.amount);
        }
        if (effect.passiveMod == Effect.PassiveMod.GiveLightRange)
        {
            if (obj.GetComponent<LightSource>() == null)
            {
                obj.AddComponent<LightSource>();
            }
            //if (stats.GetLightRange < effect.amount)
            //{
            //    stats.LightRangeMod(effect.amount - stats.GetLightRange);
            //}
            stats.LightRangeMod(effect.amount);
        }
        if (effect.passiveMod == Effect.PassiveMod.IncreaseMoveRange)
        {
            stats.MoveRangeMod(effect.amount);
        }
        #endregion


        #region Damage
        // Damage
        if (effect.passiveMod == Effect.PassiveMod.IncreaseFireDmg)
        {
            stats.FireDmgMod(effect.amount);
        }
        #endregion


        #region Abilities
        // Abilities
        if (effect.passiveMod == Effect.PassiveMod.GiveAbility)
        {
            if (effect.ability == Abilities.Ability.GatherResources)
            {
                abilities.GatherResources = true;
            }
        }
        #endregion


    }
    public static void DeinitializeEffect(Effect effect, GameObject obj)
    {
        Stats stats = obj.GetComponent<Stats>();
        Abilities abilities = obj.GetComponent<Abilities>();

        // Ranges
        if (effect.passiveMod == Effect.PassiveMod.IncreaseLightRange)
        {            
            stats.LightRangeMod(-1 * effect.amount);
        }
        if (effect.passiveMod == Effect.PassiveMod.GiveLightRange)
        {                       
            stats.LightRangeMod(-1 * effect.amount);            
        }
        if (effect.passiveMod == Effect.PassiveMod.IncreaseMoveRange)
        {
            stats.MoveRangeMod(-1 * effect.amount);
        }

        // Damage
        if (effect.passiveMod == Effect.PassiveMod.IncreaseFireDmg)
        {
            stats.FireDmgMod(-1 * effect.amount);
        }

        // Abilities
        if (effect.passiveMod == Effect.PassiveMod.GiveAbility)
        {
            if (effect.ability == Abilities.Ability.GatherResources)
            {
                abilities.GatherResources = false;
            }
        }
    }
}
