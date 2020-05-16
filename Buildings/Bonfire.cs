using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public Tile currTile;

    public Resource _resource;
   

    private LightSource _light;
    private Grid _grid;
    void Start()
    {
        _light = GetComponent<LightSource>();
        _grid = GameObject.Find("HexGen").GetComponent<Grid>();
            
        // Adjust the tiles within this building at start
        var buildingSizeTiles = _grid.TilesInRange(currTile.index, 1);
        buildingSizeTiles.Remove(currTile);
        Map.SetBuildingSize(buildingSizeTiles, Tile.Property.Bonfire);
        _resource = transform.parent.GetComponent<Resource>();

        UpdateBonfireLevel();

        EventHandler.current.onFireFed += UpdateBonfireLevel;
        EventHandler.current.onFireConsumed += UpdateBonfireLevel;
    }

    private void OnDisable()
    {
        EventHandler.current.onFireFed -= UpdateBonfireLevel;
        EventHandler.current.onFireConsumed -= UpdateBonfireLevel;
    }


    public void UpdateBonfireLevel()
    {
        var newLevel = _resource.Amount;
        _light.LightRange = newLevel + 1;
        Debug.Log("Set New Bonfire Level to: " + newLevel);
    }
}
