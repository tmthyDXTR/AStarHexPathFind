using System;
using System.Collections;
using System.Collections.Generic;
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
        Tree,
        Rock,
    }
    [SerializeField]
    private int resourceAmountAtStartTree;
    public static int workCostTree;
    [SerializeField]
    private int resourceAmountAtStartRock;
    public static int workCostRock;


    private Transform hexTileHolder;
    void Start()
    {
        hexTileHolder = GameObject.Find("HexGen").transform;

        foreach (Transform tile in hexTileHolder)
        {
            if (tile.tag == "ResourceTree")
            {
                CreateNewResource(tile, ResourceType.Tree, resourceAmountAtStartTree, workCostTree);
            }
            if (tile.tag == "ResourceRock")
            {
                CreateNewResource(tile, ResourceType.Rock, resourceAmountAtStartRock, workCostRock);
            }
        }
    }

    public void CreateNewResource(Transform tile, ResourceType type, int amount, int workCost)
    {
        var resource = tile.gameObject.AddComponent<Resource>();
        resource.type = type;
        resource.amount = amount;
        resource.WorkCost = workCost;
    }
}
