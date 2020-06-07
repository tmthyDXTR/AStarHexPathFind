using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Skill class, a skill can consists of multiple effects
/// </summary>
[System.Serializable]
[CreateAssetMenu(fileName = "new Skill", menuName = "ScriptableObjects/Skill")]
public class Skill : ScriptableObject
{
    public SkillManager.SkillId Id;
    public Form form;
    public Type type;
    public Duration duration;
    public AdditionalCost additionalCost;
    public string nameText;
    public string infoText;

    public Sprite sprite;

    public List<Effect> effects;
    public GameObject effectPrefab;


    public enum Form
    {     
        None,
        SingleTarget,
        AOE,
        Directional,
    }
    public enum AdditionalCost
    {
        None,
        Wood,
        Fire,
    }
    public enum Duration
    {
        None,
        Instant,
        OverTime,
    }
    public enum Type
    {
        None,
        Physical,
        Fire,
    }

    public int cost;
    public int addCost;
    public int range;
    public int time;
    public int radius = 1;
    public int amount;
}
