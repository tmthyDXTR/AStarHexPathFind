using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

/// <summary>
/// This class handles the "light" the object emits
/// and clears darkness, makes tiles features clear
/// and makes tiles passable for walking and attacking
/// </summary>
public class LightSource : MonoBehaviour
{
    private Bonfire bonfire;
    private Unit lightObject;
    private Grid _grid;
    private LightManager _lightManager;

    [SerializeField]
    private int lightRange = 3;
    private List<Tile> lightArea = new List<Tile>(); // This light sources individual lit area
    [SerializeField]
    public bool lightRaySent = false;
    void Start()
    {
        if (GetComponent<Unit>())
            lightObject = GetComponent<Unit>();
        if (GetComponent<Bonfire>())
            bonfire = GetComponent<Bonfire>();

        _grid = GameObject.Find("HexGen").GetComponent<Grid>();
        _lightManager = GameObject.Find("LightManager").GetComponent<LightManager>();
    }

    private void Update()
    {
        if (!lightRaySent)
        {
            CubeIndex index = new CubeIndex(0, 0, 0);
            if (bonfire)
            {
                index = bonfire.currTile.index;
            }
            if (lightObject)
            {
                index = lightObject.currTile.index;
            }
            //StartThreadLightAreaCalculation(lightObject.currTile.index, lightRange);
            CalculateNewLightArea(index, lightRange);
        }
    }

    public HashSet<CubeIndex> GetLightArea()
    {
        var ret = new HashSet<CubeIndex>();
        foreach (var item in lightArea)
        {
            ret.Add(item.index);
        }
        return ret;
    }

    private void StartThreadLightAreaCalculation(CubeIndex start, int lightRange)
    {
        Thread myThread = new Thread(() => CalculateNewLightArea(start, lightRange));
        myThread.Start();
        Debug.Log("Started new Thread to calculate light area");
        
    }
    private void CalculateNewLightArea(CubeIndex index, int lightRange)
    {
        // First iterate through current light area
        foreach (var tile in lightArea)
        {
            // And remove one from the tiles light counter
            _lightManager.litTiles[tile]--;
        }
        // Calculate the new indiviual light area at the new position
        lightArea = _grid.TilesInRange(index, lightRange);
        foreach (var tile in lightArea)
        {
            // If light manager include this tile yet
            if (!_lightManager.litTiles.ContainsKey(tile))
            {
                // Add it with one light source
                _lightManager.litTiles[tile] = 1;
            }
            // If it already exists in the lit list
            else
            {
                // Add one to the light source counter
                _lightManager.litTiles[tile]++;
            }
        }
        lightRaySent = true;
        EventHandler.current.UpdateLightSources();

        Debug.Log("Calculated new Light Area with range: " + lightRange);
    }
}
