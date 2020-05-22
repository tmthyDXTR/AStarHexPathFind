using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;


[System.Serializable]
[CreateAssetMenu(fileName = "new Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public string title;
    public string infoText;

    public GameObject prefab;
    public Sprite sprite;


    
    public ItemManager.Effect effect_1;
    // Dynamically hidden and showed variables for the (in insepector) chosen effect
    public int effect1Range = 1;


    public ItemManager.Effect effect_2;
    // Dynamically hidden and showed variables for the (in insepector) chosen effect
    public int effect2Range = 1;


    public ItemManager.Effect effect_3;
    // Dynamically hidden and showed variables for the (in insepector) chosen effect
    public int effect3Range = 1;


}
