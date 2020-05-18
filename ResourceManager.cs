using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This class handles the resources, adding the specific
/// ressource scripts to the tiles at Start and setting the
/// ressource attributes
/// </summary>
public class ResourceManager : MonoBehaviour
{
    public enum ResourceType
    {
        Fire,
        Wood,
        Stone,
    }
    [SerializeField]
    public static int resourceAmountAtStartWood = 2;
    public int workCostWood = 1;
    [SerializeField]
    public static int resourceAmountAtStartStone = 4;
    public int workCostStone = 2;

    [SerializeField]
    public static int resourceAmountAtStartFire = 1;


    [SerializeField]
    private GameObject stumpPrefab;


    private Transform _hexTileHolder;



    public static void AddRemoveResource(ResourceType type, int amount, MethodHandler.Command addRemove)
    {
        var player = GameObject.Find("ResourceManager").GetComponent<PlayerResources>();
        if (type == ResourceManager.ResourceType.Wood)
        {
            //Add Wood
            if (addRemove == MethodHandler.Command.Add)
            {
                player.wood += amount;
                foreach (var storage in player.woodStorageList)
                {
                    if (storage.currAmount < storage.maxAmount)
                    {
                        storage.AddRemoveStoredResource(1, MethodHandler.Command.Add);
                        break;
                    }
                }
            }
            //Remove Wood
            else if (addRemove == MethodHandler.Command.Remove)
            {
                player.wood -= amount;
                foreach (var storage in player.woodStorageList)
                {
                    if ((storage.currAmount - amount) >= 0)
                    {
                        storage.AddRemoveStoredResource(amount, MethodHandler.Command.Remove);
                        break;
                    }
                }
            }
        }
        if (type == ResourceManager.ResourceType.Stone)
        {
            //Add Stone
            if (addRemove == MethodHandler.Command.Add)
            {
                player.stone += amount;
                foreach (var storage in player.stoneStorageList)
                {
                    if (storage.currAmount < storage.maxAmount)
                    {
                        storage.AddRemoveStoredResource(1, MethodHandler.Command.Add);
                        break;
                    }
                }
            }
            //Remove Stone
            else if (addRemove == MethodHandler.Command.Remove)
            {
                player.stone -= amount;
            }
        }
        if (type == ResourceManager.ResourceType.Fire)
        {
            //Add Fire
            if (addRemove == MethodHandler.Command.Add)
            {
                player.fire += amount;
            }
            // Remove Fire
            else if (addRemove == MethodHandler.Command.Remove)
            {
                player.fire -= amount;
            }
        }
    }

    public static void AddRemoveStorage(ResourceStorage storage, MethodHandler.Command addRemove)
    {
        var player = GameObject.Find("ResourceManager").GetComponent<PlayerResources>();
        var storageAmount = storage.maxAmount;
        if (storage.type == ResourceManager.ResourceType.Wood)
        {
            //Add Wood storage
            if (addRemove == MethodHandler.Command.Add)
            {
                if (!player.woodStorageList.Contains(storage))
                {
                    player.woodStorageList.Add(storage);
                    PlayerResources.woodMax += storageAmount;
                }                    
            }
            //Remove Wood storage
            else if (addRemove == MethodHandler.Command.Remove)
            {
                if (player.woodStorageList.Contains(storage))
                {
                    player.woodStorageList.Remove(storage);
                    PlayerResources.woodMax -= storageAmount;
                }
            }
        }
        if (storage.type == ResourceManager.ResourceType.Stone)
        {
            //Add Stone storage
            if (addRemove == MethodHandler.Command.Add)
            {
                if (!player.stoneStorageList.Contains(storage))
                {
                    player.stoneStorageList.Add(storage);
                    PlayerResources.stoneMax += storageAmount;
                }
            }
            //Remove Stone storage
            else if (addRemove == MethodHandler.Command.Remove)
            {
                if (player.stoneStorageList.Contains(storage))
                { 
                    player.stoneStorageList.Remove(storage);
                    PlayerResources.stoneMax -= storageAmount;
                }
            }
        }
    }

    public static void ConsumeFirePower(int amount, Resource resource, Unit unit)
    {
        if ((resource.Amount - amount) >= 0)
        {
            AddRemoveResource(resource.type, amount, MethodHandler.Command.Remove);
            resource.Amount -= amount;
            EventHandler.current.ConsumeFire();
            Debug.Log("Fire consumed: " + amount);
        }
        else
        {
            Debug.Log("Can't consume anymore fire power.");
        }
    }
    
    public static void FeedFire(int amount, Resource resource)
    {
        if ((resource.Amount + amount) <= 10 && PlayerResources.Wood >= amount)
        {
            AddRemoveResource(ResourceManager.ResourceType.Wood, amount, MethodHandler.Command.Remove);
            AddRemoveResource(resource.type, amount, MethodHandler.Command.Add);
            resource.Amount += amount;
            EventHandler.current.FireFed();
            Debug.Log("Fire fed with: " + amount + " log(s).");
        }        
        else
        {
            Debug.Log("Can't feed fire anymore.");
        }
    }

    public static void Gather(Unit worker, Resource resource)
    {
        if (PlayerResources.hasStorageRoom(resource.type, 1))
        {
            if (worker.remainingMoves >= resource.WorkCost)
            {
                worker.remainingMoves -= resource.WorkCost;
                resource.Amount--;
                EventHandler.current.ResourceGathered(worker, resource.type);
                AddRemoveResource(resource.type, 1, MethodHandler.Command.Add);
                Debug.Log("Gathered: " + resource.type);

                if (resource.type == ResourceManager.ResourceType.Wood)
                {
                    var resourceObjToReplace = resource.resourceObjects[0];
                    GameObject stump = Instantiate((GameObject)Resources.Load("Nature/Stump"), resourceObjToReplace.transform.position, resourceObjToReplace.transform.rotation, resource.transform);
                    resource.resourceObjects.Remove(resourceObjToReplace);
                    Destroy(resourceObjToReplace);
                    if (resource.resourceObjects.Count == 0)
                    {
                        Destroy(resource);
                    }
                }
                else if (resource.type == ResourceManager.ResourceType.Stone)
                {
                    var resourceObjToReplace = resource.resourceObjects[0];
                    //GameObject stump = Instantiate((GameObject)Resources.Load("Nature/Stump"), resourceObjToReplace.transform.position, resourceObjToReplace.transform.rotation, resource.transform);
                    //resource.resourceObjects.Remove(resourceObjToReplace);
                    if (resource.Amount < 1) 
                    {
                        Destroy(resourceObjToReplace);
                        Destroy(resource);
                    }
                }
            }
            else
            {
                Debug.Log("No remaining Moves: " + resource.type + " not gathered");
            }
        }
        else
        {
            Debug.Log("No Storage rooom for: " + resource.type + " not gathered");
        }
    }



    void Start()
    {
        stumpPrefab = (GameObject)Resources.Load("Nature/Stump");

        _hexTileHolder = GameObject.Find("HexGen").transform;

        foreach (Transform tile in _hexTileHolder)
        {
            if (tile.tag == TagHandler.resourceWoodString)
            {
                CreateNewResource(tile, ResourceType.Wood, resourceAmountAtStartWood, workCostWood);
            }
            if (tile.tag == TagHandler.resourceStoneString)
            {
                CreateNewResource(tile, ResourceType.Stone, resourceAmountAtStartStone, workCostStone);
            }
            //if (tile.tag == TagHandler.buildingBonfireString && tile.Find("Bonfire"))
            //{
            //    CreateNewResource(tile, ResourceType.Fire, resourceAmountAtStartFire);
            //}
        }
    }




    public void CreateNewResource(Transform tile, ResourceType type, int amount, int workCost = 0)
    {
        var resource = tile.gameObject.AddComponent<Resource>();
        resource.type = type;
        resource.Amount = amount;
        resource.WorkCost = workCost;
        foreach(Transform child in resource.transform)
        {
            if (child.tag == TagHandler.treeString || child.tag == TagHandler.stoneString)
                resource.resourceObjects.Add(child.gameObject);
        }
    }
}
