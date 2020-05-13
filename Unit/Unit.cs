using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

/// <summary>
/// This class handles unit specific stuff like
/// movement, pathfinding ...
/// </summary>
public class Unit : MonoBehaviour
{
    public Tile currTile;
    public Tile destTile;
    public int moveRange;

    // Internal Variables
    private Grid _grid;
    private Light _light;
    [SerializeField]
    private bool areaCalcStarted = false;
    [SerializeField]
    private bool areaCalcFinished = false;
    private HashSet<CubeIndex> moveArea = new HashSet<CubeIndex>();
    public List<CubeIndex> path = new List<CubeIndex>();

    void Start()
    {
        _grid = GameObject.Find("HexGen").GetComponent<Grid>();
        _light = GetComponent<Light>();


    // Calc the move area on new Thread
    StartThreadMoveAreaCalculation(currTile.index, moveRange);
    }

    void Update()
    {
        if (destTile)
        {
            if (!GetComponent<PathFollow>().enabled)
            {
                FollowPath(path);
                // Must check if moveRange is greater 0
                // otherwise its stuck at start calculation
                moveRange -= path.Count;
                if (moveRange < 1)
                    moveArea.Clear();
                else
                    StartThreadMoveAreaCalculation(destTile.index, moveRange);
            }
            // Check if target reached
            if (Vector3.Distance(this.transform.position, destTile.transform.position) <= 0.05f)
            {
                _light.lightRaySent = false;

                destTile = null;
                path.Clear();
                GetComponent<PathFollow>().enabled = false;
                Debug.Log("Path finished");
            }
        }        
    }

    private void StartThreadMoveAreaCalculation(CubeIndex start, int moveRange)
    {
        Thread myThread = new Thread(() => CalculateNewMoveArea(start, moveRange));
        myThread.Start();
        Debug.Log("Started new Thread to calculate move area");
        areaCalcStarted = true;
        areaCalcFinished = false;
    }

    private void FollowPath(List<CubeIndex> path)
    {
        PathFollow pathFollow = GetComponent<PathFollow>();
        pathFollow.pathNodes = path;
        pathFollow.enabled = true;
    }

    public void CalculateNewMoveArea(CubeIndex index, int moveRange)
    {
        moveArea = _grid.TilesInMoveRange(index, moveRange);
        areaCalcFinished = true;
        areaCalcStarted = false;
        Debug.Log("Calculated new Move Area with range: " + moveRange);
    }

    private IEnumerator MoveToNextNode(CubeIndex pathNode)
    {
        var target = _grid.TileAt(pathNode).transform.position;
        while (Vector3.Distance(this.transform.position, target) > 0.05f)
        {
            Debug.Log("Moving to next node");
            transform.position = Vector3.Lerp(transform.position, target, 0.3f * Time.deltaTime);

            yield return null;
        }
        destTile = null;
        Debug.Log("Node reached");
        yield return null;
    }

    // Public Methods
    public List<Tile> GetMoveAreaTilesByIndices()
    {
        var ret = new List<Tile>();
        foreach (var index in moveArea)
        {
            if (!index.Equals(currTile.index))
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
            currTile = tile;
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
