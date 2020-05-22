using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// This class handles unit specific stuff like
/// movement, pathfinding ...
/// </summary>
public class Unit : MonoBehaviour
{
    public Tile currTile;
    public Tile destTile;
    public int moveRange;
    public int remainingMoves = 3;

    // Internal Variables
    private SelectionManager _selection;
    private Grid _grid;
    private LightSource _light;
    private bool areaCalcStarted = false;
    [SerializeField]
    private bool areaCalcFinished = false;
    public bool MoveAreaCalculated
    {
        get { return areaCalcFinished; }
        set
        {
            areaCalcFinished = value;
            if (areaCalcFinished)
            {
                //_selection.UpdateNeighboursAndHighlightArea();
                //EventHandler.current.MoveAreaCalculated();
            }
        }
    }

    private HashSet<CubeIndex> moveArea = new HashSet<CubeIndex>();
    public List<CubeIndex> path = new List<CubeIndex>();

    void Start()
    {
        _selection = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        _grid = GameObject.Find("HexGen").GetComponent<Grid>();
        _light = GetComponent<LightSource>();


        EventHandler.current.onResourceDestroyed += () => StartThreadMoveAreaCalculation(currTile.index, moveRange);
        EventHandler.current.onBeginBuildingConstruction += () => StartThreadMoveAreaCalculation(currTile.index, moveRange);
        // Calc the move area on new Thread
        //StartThreadMoveAreaCalculation(currTile.index, moveRange);
        StartCoroutine(SysHelper.WaitForAndExecute(0.1f, () => StartThreadMoveAreaCalculation(currTile.index, moveRange)));
    }

    private void OnDisable()
    {
        EventHandler.current.onResourceDestroyed -= () => StartThreadMoveAreaCalculation(currTile.index, moveRange);
        EventHandler.current.onBeginBuildingConstruction -= () => StartThreadMoveAreaCalculation(currTile.index, moveRange);
    }

    void Update()
    {


        // Move along path if destination set
        if (destTile)
        {
            if (path.Count > 0)
            {
                if (!GetComponent<PathFollow>().enabled)
                {
                    currTile.Passable = true;
                    FollowPath(path);
                    // Must check if moveRange is greater 0
                    // otherwise its stuck at start calculation
                    remainingMoves--;
                    if (remainingMoves < 1)
                        moveArea.Clear();
                    else
                        StartThreadMoveAreaCalculation(destTile.index, moveRange);
                }
                // Check if target reached
                if (Vector3.Distance(this.transform.position, destTile.transform.position) <= 0.05f)
                {
                    _light.LightRaySent = false;
                    destTile = null;
                    path.Clear();
                    GetComponent<PathFollow>().enabled = false;
                    Debug.Log("Path finished");
                    EventHandler.current.MoveAreaCalculated();
                }
            }
            else
            {
                Debug.Log("Something went wrong with path nodes /path follow");
                destTile = null;
            }
        }
    }

    public void StartThreadMoveAreaCalculation(CubeIndex start, int moveRange)
    {
        Thread myThread = new Thread(() => CalculateNewMoveArea(start, moveRange));
        myThread.Start();
        Debug.Log("Started new Thread to calculate move area");
        areaCalcStarted = true;
        MoveAreaCalculated = false;
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
        MoveAreaCalculated = true; // Triggers event to update highlghted area
        areaCalcStarted = false;
        Debug.Log("Calculated new Move Area with range: " + moveRange);
        
    }


    // Public Methods
    public List<Tile> GetMoveAreaTilesByIndices()
    {
        var ret = new List<Tile>();
        foreach (var index in moveArea)
        {
            if (!index.Equals(currTile.index)) // && _light.GetLightArea().Contains(index)
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
            tile.Passable = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8) // Tile Layer
        {
            Tile tile = other.gameObject.GetComponent<Tile>();
            tile.unit = null;
            tile.Passable = true;
        }
    }
}
