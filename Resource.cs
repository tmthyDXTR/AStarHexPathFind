using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The script on the tile that handles the amount and type
/// of resource it holds. workers and skills can gather them
/// for a work/action point cost and deplete them.
/// </summary>
public class Resource : MonoBehaviour
{
    public ResourceManager.ResourceType type;

    [SerializeField]
    private int amount;  
    public int Amount
    {
        get { return amount; }
        set { amount = value; }
    }
    [SerializeField]
    private int costWork;
    public int CostWork
    {
        get { return costWork; }
        set { costWork = value; }
    }

    public List<GameObject> resourceObjects = new List<GameObject>();

    private Tile _tile;
    private Map _mapGen;
    Resource (ResourceManager.ResourceType type, int amount, int costWork)
    {
        // This constructor is used for the creation of resource tiles
        // and also defines the work cost to harvest
        this.type = type;
        this.amount = amount;
        this.costWork = costWork;
        
    }

    Resource (ResourceManager.ResourceType type, int amount)
    {
        // This constructor is used for carried item creation of the resource
        this.type = type;
        this.amount = amount;
    }

    private void OnEnable()
    {
        resourceObjects = new List<GameObject>();
        _tile = GetComponent<Tile>();
        _mapGen = GameObject.Find("MapGen").GetComponent<Map>();
    }

    private void OnDestroy()
    {
        if (this.tag != TagHandler.buildingBonfireString)
        {
            _mapGen.SetTilePropTo(this.transform, Tile.Property.Grass);
            EventHandler.current.ResourceDestroyed();
        }        
    }
}
