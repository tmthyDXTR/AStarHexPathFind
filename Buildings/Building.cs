using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Tile.Property property;
    [SerializeField]
    private int workAmount;
    public int WorkAmount
    {
        get { return workAmount; }
        set { workAmount = value; }
    }
    [SerializeField]
    private int costWork;
    public int CostWork
    {
        get { return costWork; }
        set { costWork = value; }
    }
    [SerializeField]
    private int costWood;
    public int CostWood
    {
        get { return costWood; }
        set { costWood = value; }
    }
    [SerializeField]
    private int costStone;
    public int CostStone
    {
        get { return costStone; }
        set { costStone = value; }
    }


    private void OnEnable()
    {
    }

    public void InitializeBuildingInfo(Tile.Property property)
    {
        int costWood = 0, costStone = 0, costWork = 0, workAmount = 0;

        if (property == Tile.Property.House)
        {
            // Use a dictionary or smth other external and central for this?
            costWood = 4;
            costStone = 0;
            costWork = 2;
            workAmount = 4;
        }

        this.WorkAmount = workAmount;
        this.costWork = costWork;
        this.costWood = costWood;
        this.costStone = costStone;
        Debug.Log("Initialized Building Info: " + property);
    }
}
