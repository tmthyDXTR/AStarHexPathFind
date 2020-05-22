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
    public int LightRange
    {
        get { return lightRange; }
        set
        {
            if (value >= 0 && value <= 20)
            {
                lightRange = value;
                LightRaySent = false;
            }
            else
            {
                Debug.Log("lightRange cant be < 0 & > 20");
            }
        }
    }
    private List<Tile> lightArea = new List<Tile>(); // This light sources individual lit area
    
    [SerializeField]
    private bool lightRaySent = false;
    public bool LightRaySent
    { 
        get {return lightRaySent; }
        set
        {
            lightRaySent = value;
            if (!lightRaySent)
            {
                SendLightRays(lightRange);
            }
        }
    }
    void OnEnable()
    {
        if (GetComponent<Unit>())
            lightObject = GetComponent<Unit>();
        if (GetComponent<Bonfire>())
            bonfire = GetComponent<Bonfire>();


        _grid = GameObject.Find("HexGen").GetComponent<Grid>();
        _lightManager = GameObject.Find("LightManager").GetComponent<LightManager>();
        StartCoroutine(SysHelper.WaitForAndExecute(.5f, () => SendLightRays(lightRange)));
    }

    private void OnDestroy()
    {
        foreach (var tile in lightArea)
        {
            // And remove one from the tiles light counter
            _lightManager.litTiles[tile]--;
        }
        EventHandler.current.LightSourcesUpdated();
    }

    private void SendLightRays(int lightRange)
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
        if (gameObject.layer == 20) // Item?
        {
            index = this.transform.parent.GetComponent<Tile>().index;
        }
        //StartThreadLightAreaCalculation(lightObject.currTile.index, lightRange);
        CalculateNewLightArea(index, lightRange);
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
        LightRaySent = true;
        EventHandler.current.LightSourcesUpdated();

        Debug.Log("Calculated new Light Area with range: " + lightRange);
    }
}
