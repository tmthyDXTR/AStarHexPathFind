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
    public int amount;

    private int workCost;

    Resource (ResourceManager.ResourceType type, int amount, int workCost)
    {
        // This constructor is used for the creation of resource tiles
        // and also defines the work cost to harvest
        this.type = type;
        this.amount = amount;
        this.workCost = workCost;
    }

    Resource (ResourceManager.ResourceType type, int amount)
    {
        // This constructor is used for carried item creation of the resource
        this.type = type;
        this.amount = amount;
    }

    public int WorkCost
    {
        get { return workCost; }
        set { workCost = value; }
    }
}
