using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "new Effect", menuName = "ScriptableObjects/Effect")]
public class Effect : ScriptableObject
{
    public enum BaseType
    {
        Passive,
        Active,
    }
    public enum Mod
    {
        GiveLightRange,
        AddLightRange,
        AddMoveRange,
        AddFireDamage,
    }


    public BaseType baseType;
    public Mod mod;


    public int amount;
}
