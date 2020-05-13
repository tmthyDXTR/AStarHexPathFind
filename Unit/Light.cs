using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

/// <summary>
/// This class handles the "light" the object emits
/// and clears darkness, makes tiles features clear
/// and makes tiles passable for walking and attacking
/// </summary>
public class Light : MonoBehaviour
{
    private Unit lightObject;
    private Grid _grid;

    [SerializeField]
    private int lightRange = 3;
    private List<Tile> lightArea = new List<Tile>();
    [SerializeField]
    public bool lightRaySent = false;
    void Start()
    {
        lightObject = GetComponent<Unit>();
        _grid = GameObject.Find("HexGen").GetComponent<Grid>();
    }

    private void Update()
    {
        if (!lightRaySent)
        {
            //StartThreadLightAreaCalculation(lightObject.currTile.index, lightRange);
            CalculateNewLightArea(lightObject.currTile.index, lightRange);
        }
    }

    private void StartThreadLightAreaCalculation(CubeIndex start, int lightRange)
    {
        Thread myThread = new Thread(() => CalculateNewLightArea(start, lightRange));
        myThread.Start();
        Debug.Log("Started new Thread to calculate light area");
        
    }
    private void CalculateNewLightArea(CubeIndex index, int lightRange)
    {
        foreach (var tile in lightArea)
        {
            if (tile.lightFromList.Contains(this))
                tile.lightFromList.Remove(this);
            if (tile.lightFromList.Count == 0)
            {
                VisualHandler vH = tile.GetComponent<VisualHandler>();
                vH.ChangeVisibility(Tile.Visibility.Hidden);
            }
        }
        lightArea = _grid.TilesInRange(index, lightRange);
        foreach (var tile in lightArea)
        {
            if (!tile.lightFromList.Contains(this))
                tile.lightFromList.Add(this);
            VisualHandler vH = tile.GetComponent<VisualHandler>();
            vH.ChangeVisibility(Tile.Visibility.Default);
            //if (tile.property == Tile.Property.Grass)
            //{
            //    tile.Passable = true;
            //}
        }
        lightRaySent = true;

        Debug.Log("Calculated new Light Area with range: " + lightRange);
    }
}
