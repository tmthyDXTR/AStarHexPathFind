using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionUtil : MonoBehaviour
{
    [SerializeField]
    private Grid _grid;
    [SerializeField]
    private SelectionManager _selection;

    public CubeIndex hoverTile;
    public HashSet<CubeIndex> highlightArea = new HashSet<CubeIndex>();
    public HashSet<CubeIndex> highlightPath = new HashSet<CubeIndex>();

    void Start()
    {
        _grid = GameObject.Find("HexGen").GetComponent<Grid>();
        _selection = GetComponent<SelectionManager>();

        //EventHandler.current.onHoverOverTile += UpdatePath;
    }



    private void UpdatePath(CubeIndex index)
    {
        //Hide old path
        //foreach (var pathTile in highlightPath)
        //{
        //    Selectable select = _grid.TileAt(pathTile).GetComponent<Selectable>();
        //    select.IsPath = false;
        //}

        hoverTile = index;

        //Tile tile = _grid.TileAt(hoverTile);
        //Selectable selected = tile.GetComponent<Selectable>();
        //if (selected.IsMoveArea)
        //{
        //    if (tile != selected.moveStart)
        //    {
        //        var aStar = new AStarSearch(_grid, selected.moveStart.index, hoverTile);
        //        highlightPath = aStar.FindPath();

        //        foreach (var pathTile in highlightPath)
        //        {
        //            Selectable select = _grid.TileAt(pathTile).GetComponent<Selectable>();
        //            select.IsPath = true;
        //        }
        //        Debug.Log("Update Path");
        //    }
        //}      
    }
}
