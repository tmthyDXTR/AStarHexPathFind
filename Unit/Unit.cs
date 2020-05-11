using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;


/// <summary>
/// This class handles unit specific stuff like
/// movement, pathfinding ...
/// </summary>
public class Unit : MonoBehaviour
{
    public Tile currTilePos;
    public Tile destTilePos;
    public int moveRange;

    // Internal Variables
    private Grid _grid;
    private HashSet<CubeIndex> moveArea = new HashSet<CubeIndex>();
    public List<CubeIndex> path = new List<CubeIndex>();
    void Start()
    {
        _grid = GameObject.Find("HexGen").GetComponent<Grid>();
    }

    void Update()
    {
        if (moveArea.Count < 1)
        {
            moveArea = _grid.TilesInMoveRange(currTilePos.index, moveRange);
        }
        if (destTilePos)
        {
            //Debug.Log(path.Count);
            while (this.transform.position != destTilePos.transform.position)
            {
                //Debug.Log("Moving to dest: " + destTilePos);

            }
        }
    }

    // Public Methods
    public List<Tile> GetMoveArea()
    {
        var ret = new List<Tile>();
        foreach (var index in moveArea)
        {
            ret.Add(_grid.TileAt(index));
        }
        return ret;
    }





    // Triggers to give the tile the information that this
    // unit enters or leaves the tile
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) // Tile Layer
        {
            Tile tile = other.gameObject.GetComponent<Tile>();
            tile.unit = this.transform;
            currTilePos = tile;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8) // Tile Layer
        {
            Tile tile = other.gameObject.GetComponent<Tile>();
            tile.unit = null;
        }
    }
}
