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
    public enum PassiveMod
    {
        // Ranges
        GiveLightRange,
        IncreaseLightRange,
        IncreaseMoveRange,

        // Dmg
        IncreaseFireDmg,

        // Ability
        GiveAbility,
    }


    public BaseType baseType;
    public PassiveMod passiveMod;


    public int amount;
    public Abilities.Ability ability;
}
