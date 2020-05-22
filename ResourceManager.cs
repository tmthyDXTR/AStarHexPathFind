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

    public static void EndBurn(int amount)
    {
        
        Bonfire bonfire = GameObject.Find("Bonfire").GetComponent<Bonfire>();
        bonfire._resource.Amount -= amount;
        ResourceManager.AddRemoveResource(ResourceManager.ResourceType.Fire, 1, MethodHandler.Command.Remove);
        bonfire.UpdateBonfireLevel();
        EventHandler.current.ConsumeFire();
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
            if (worker.remainingMoves >= resource.CostWork)
            {
                worker.remainingMoves -= resource.CostWork;
                resource.Amount--;
                

                if (resource.type == ResourceManager.ResourceType.Wood)
                {
                    var resourceObjToReplace = resource.resourceObjects[0];
                    // OldTree
                    if (resourceObjToReplace.transform.parent.tag == TagHandler.magicTreeString)
                    {
                        OldTree oldTree = resource.resourceObjects[0].GetComponent<OldTree>();
                        var sun = GameObject.Find("Sun").GetComponent<LightSource>();
                        if (resource.Amount == 9)
                        {
                            Talker.TypeThis("Kablang..."); 
                            sun.LightRange--;
                            oldTree.minimum = -0.8f;
                            
                        }
                        if (resource.Amount == 8)
                        {
                            Talker.TypeThis("Mortal, you are way out of line!");
                            sun.LightRange--;
                            oldTree.minimum = -0.6f;

                        }
                        if (resource.Amount == 7)
                        {
                            Talker.TypeThis("Hold on for a second, I must warn you!");
                            sun.LightRange--;
                            oldTree.minimum = -0.4f;

                        }
                        if (resource.Amount == 6)
                        {
                            Talker.TypeThis("Felling this tree will lead down a path of darkness...");
                            sun.LightRange--;
                            oldTree.minimum = -0.2f;

                        }
                        if (resource.Amount == 5)
                        {
                            Talker.TypeThis("The last person that tried to cut me down somehow seemed more sensible than you.");
                        }if (resource.Amount < 5 && resource.Amount > 0)
                        {
                            AddRemoveResource(resource.type, 1, MethodHandler.Command.Add);
                            EventHandler.current.ResourceGathered(worker, resource.type);
                            Debug.Log("Gathered: " + resource.type);
                            Talker.TypeThis("Gathered: " + resource.type);
                            sun.LightRange--;
                            oldTree.minimum =+ 0.2f;

                        }
                        if (resource.Amount == 0)
                        {
                            AddRemoveResource(resource.type, 1, MethodHandler.Command.Add);
                            EventHandler.current.ResourceGathered(worker, resource.type);
                            Debug.Log("Gathered: " + resource.type);
                            Talker.TypeThis("Gathered: " + resource.type);
                            resource.resourceObjects.Remove(resourceObjToReplace);
                            Destroy(resourceObjToReplace);
                            if (resource.resourceObjects.Count == 0)
                            {
                                Destroy(resource);
                            }

                            sun.LightRange = 0;
                            GameObject.Destroy(sun.gameObject);
                            GameObject stump = Instantiate((GameObject)Resources.Load("Nature/OldTreeDark"), resourceObjToReplace.transform.position, resourceObjToReplace.transform.rotation, resource.transform);

                        }
                    }
                    else
                    {
                        AddRemoveResource(resource.type, 1, MethodHandler.Command.Add);
                        EventHandler.current.ResourceGathered(worker, resource.type);
                        Debug.Log("Gathered: " + resource.type);
                        Talker.TypeThis("Gathered: " + resource.type);
                        GameObject stump = Instantiate((GameObject)Resources.Load("Nature/Stump"), resourceObjToReplace.transform.position, resourceObjToReplace.transform.rotation, resource.transform);
                        GameObject treeFall = Instantiate((GameObject)Resources.Load("Nature/TreeFall"), resourceObjToReplace.transform.position, resourceObjToReplace.transform.rotation, resource.transform);
                        treeFall.transform.localScale += new Vector3(1, 1, 1);
                        resource.resourceObjects.Remove(resourceObjToReplace);
                        Destroy(resourceObjToReplace);
                        if (resource.resourceObjects.Count == 0)
                        {
                            Destroy(resource);
                        }
                    }                    
                }
                else if (resource.type == ResourceManager.ResourceType.Stone)
                {
                    AddRemoveResource(resource.type, 1, MethodHandler.Command.Add);
                    EventHandler.current.ResourceGathered(worker, resource.type);
                    Debug.Log("Gathered: " + resource.type);
                    Talker.TypeThis("Gathered: " + resource.type);
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
            if (tile.tag == TagHandler.magicTreeString)
            {
                CreateNewResource(tile, ResourceType.Wood, 10, workCostWood);
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
        resource.CostWork = workCost;
        foreach(Transform child in resource.transform)
        {
            if (child.tag == TagHandler.treeString || child.tag == TagHandler.stoneString)
                resource.resourceObjects.Add(child.gameObject);
        }
    }
}
