using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Tile.Property property;
    [SerializeField]
    private int health;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    [SerializeField]
    private int workCost;
    public int WorkCost
    {
        get { return workCost; }
        set { workCost = value; }
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

}
