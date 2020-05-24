using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/// <summary>
/// Item class, a item can consists of multiple effects
/// </summary>
[System.Serializable]
[CreateAssetMenu(fileName = "new Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public ItemManager.ItemID ID;
    public string infoText;

    public GameObject prefab;
    public Sprite sprite;

    public List<Effect> effects;


}
