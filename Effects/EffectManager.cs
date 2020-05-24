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

        if (effect.mod == Effect.Mod.AddLightRange)
        {
            if (obj.GetComponent<LightSource>() == null)
            {
                obj.AddComponent<LightSource>();
            }
            stats.LightRangeMod(effect.amount);
        }
        if (effect.mod == Effect.Mod.GiveLightRange)
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
        if (effect.mod == Effect.Mod.AddMoveRange)
        {
            stats.MoveRangeMod(effect.amount);
        }
        if (effect.mod == Effect.Mod.AddFireDamage)
        {
            stats.FireDmgMod(effect.amount);
        }
    }
    public static void DeinitializeEffect(Effect effect, GameObject obj)
    {
        Stats stats = obj.GetComponent<Stats>();

        if (effect.mod == Effect.Mod.AddLightRange)
        {            
            stats.LightRangeMod(-1 * effect.amount);
        }
        if (effect.mod == Effect.Mod.GiveLightRange)
        {                       
            stats.LightRangeMod(-1 * effect.amount);            
        }
        if (effect.mod == Effect.Mod.AddMoveRange)
        {
            stats.MoveRangeMod(-1 * effect.amount);
        }
        if (effect.mod == Effect.Mod.AddFireDamage)
        {
            stats.FireDmgMod(-1 * effect.amount);
        }
    }
}
