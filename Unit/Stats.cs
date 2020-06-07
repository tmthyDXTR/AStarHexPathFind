using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Base Values")]
    [SerializeField]
    private int baseMaxHealth = 1;
    [SerializeField]
    private int baseMoveRange = 1;
    [SerializeField]
    private int baseLightRange = 1;
    [SerializeField]
    private int baseFastDmg = 1;
    [SerializeField]
    private int baseSlowDmg = 2;
    [SerializeField]
    private int baseFireDmg = 0;

    [Header("Modifiers")]
    [SerializeField]
    private int maxHealthMod = 0;
    [SerializeField]
    private int moveRangeMod = 0;
    [SerializeField]
    private int lightRangeMod = 0;
    [SerializeField]
    private int fastDmgMod = 0;
    [SerializeField]
    private int slowDmgMod = 0;
    [SerializeField]
    private int fireDmgMod = 0;

    #region Actual value Get
    public int GetMaxHealth
    {
        get { return baseMaxHealth + maxHealthMod; }
    }

    public int GetMoveRange
    {
        get { return baseMoveRange + moveRangeMod; }
    }
    public int GetLightRange
    {
        get { return baseLightRange + lightRangeMod; }
    }
    public int GetFastDmg
    {
        get { return baseFastDmg + fastDmgMod; }
    }
    public int GetSlowDmg
    {
        get { return baseSlowDmg + slowDmgMod; }
    }
     public int GetFireDmg
    {
        get { return baseFireDmg + fireDmgMod; }
    }

    #endregion

    #region Modifiers GetSet
    public void MaxHealthMod(int valueChange)
    {
        maxHealthMod += valueChange;
        EventHandler.current.StatsChanged();
    }
    public void MoveRangeMod(int valueChange)
    {
        moveRangeMod += valueChange;
        EventHandler.current.StatsChanged();        
    }
    public void LightRangeMod(int valueChange)
    {
        lightRangeMod += valueChange;
        EventHandler.current.StatsChanged();        
    }
    public void FastDmgMod(int valueChange)
    {
        fastDmgMod += valueChange;
        EventHandler.current.StatsChanged();        
    }
    public void SlowDmgMod(int valueChange)
    {
        slowDmgMod += valueChange;
        EventHandler.current.StatsChanged();        
    }
    public void FireDmgMod(int valueChange)
    {
        fireDmgMod += valueChange;
        EventHandler.current.StatsChanged();        
    }
    


    #endregion
}
