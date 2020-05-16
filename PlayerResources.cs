using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// This class stores the gathered player resources, helper methods.
/// They get added and removed by the ResourceManager static helper methods
/// This script listens to the Resource Gather events and updates the stored
/// resources and also keeps a list of all the storage buildings
/// </summary>
public class PlayerResources : MonoBehaviour
{
    public static PlayerResources current;

    public int fire = 0;
    [SerializeField]
    public int wood = 0;
    [SerializeField]
    public static int woodMax = 0;
    [SerializeField]
    public int stone = 0;
    [SerializeField]
    public static int stoneMax = 0;


    public Bonfire bonfire = null;
    public List<ResourceStorage> woodStorageList = new List<ResourceStorage>();
    public List<ResourceStorage> stoneStorageList = new List<ResourceStorage>();


    private void Start()
    {
        current = this;
        fire = ResourceManager.resourceAmountAtStartFire;
        bonfire = GameObject.Find("Bonfire").GetComponent<Bonfire>();
    }
    public static int Fire
    {
        get
        {
            return PlayerResources.current.fire;
        }
    }

    public static int Wood
    {
        get {
            return PlayerResources.current.wood;
        } 
    }
    public static int Stone
    {
        get {
            return PlayerResources.current.stone;
        }
    }

    public static bool hasStorageRoom(ResourceManager.ResourceType type, int amount)
    {
        if (type == ResourceManager.ResourceType.Wood)
        {
            return Wood + amount <= woodMax;
        }
        if (type == ResourceManager.ResourceType.Stone)
        {
            return Stone + amount <= stoneMax;
        }
        return false;
    }
}