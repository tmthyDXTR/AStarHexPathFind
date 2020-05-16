using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEditorInternal;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    /// <summary>
    /// This class handles the selection through input
    /// </summary>
    /// 
    private Grid _grid;
    private Selectable _selectable;

    #region Variables
    [SerializeField]
    public bool isActive = true;
    [SerializeField]
    public static Tile hoveredTile;

    public List<GameObject> currentSelected = new List<GameObject>();
    public List<Tile> highlightArea = new List<Tile>();
    public List<Tile> neighbours = new List<Tile>();

    public List<CubeIndex> movePath = new List<CubeIndex>();

    #endregion

    void Start()
    {
        _grid = GameObject.Find("HexGen").GetComponent<Grid>();
        //_aStar = _grid.GetComponent<AStarSearch>();
        // Events
        EventHandler.current.onHoverOverTile += UpdateHoveredTile;
        EventHandler.current.onMoveAreaCalculated += UpdateNeighboursAndHighlightArea;
        // Very important for "instant" updating highlight area after resource tile got passable because of 
        // resource depletion
        EventHandler.current.onResourceDestroyed += () => StartCoroutine(SysHelper.WaitForAndExecute(0.1f, () => UpdateNeighboursAndHighlightArea()));
        EventHandler.current.onResourceDestroyed += () => StartCoroutine(SysHelper.WaitForAndExecute(0.2f, () => UpdateHoveredTile(hoveredTile)));


    }

    private void OnDestroy()
    {
        EventHandler.current.onHoverOverTile -= UpdateHoveredTile;
        EventHandler.current.onMoveAreaCalculated -= UpdateNeighboursAndHighlightArea;
        EventHandler.current.onResourceDestroyed -= () => StartCoroutine(SysHelper.WaitForAndExecute(0.5f, () => UpdateNeighboursAndHighlightArea()));
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            // Right Click Deselect
            if (Input.GetMouseButtonDown(1))
            {
                DeselectAll();
            }
            if (Input.GetMouseButtonDown(0))
            {
                var clickedObj = GetClickedObject();
                if (clickedObj && clickedObj.GetComponent<Selectable>())
                {
                    // If nothing is selected, select the obj
                    if (currentSelected.Count == 0)
                    {
                        Select(clickedObj);
                    }
                    // If a unit is selected
                    if (currentSelected[0].GetComponent<Unit>())
                    {
                        if (clickedObj.GetComponent<Unit>() && currentSelected[0] != clickedObj)
                        {
                            DeselectAll();
                            Select(clickedObj);
                        }
                        // Move?
                        if (highlightArea.Count > 0 && highlightArea.Contains(hoveredTile) && !currentSelected[0].GetComponent<Unit>().destTile)
                        {
                            neighbours.Clear();
                            var unit = currentSelected[0].GetComponent<Unit>();
                            if (unit.remainingMoves > 0)
                            {
                                unit.destTile = hoveredTile;
                                unit.path = movePath;
                            }                            
                        }
                        // Action to neighbour tile?
                        else if (neighbours.Contains(hoveredTile))
                        {
                            // Gather?
                            if (hoveredTile.GetComponent<Resource>())
                            {
                                var unit = currentSelected[0].GetComponent<Unit>();
                                ResourceManager.Gather(unit, hoveredTile.GetComponent<Resource>());
                                //DeselectAll();
                                //Select(unit.gameObject);
                                //UpdateNeighboursAndHighlightArea();
                                //unit.CalculateNewMoveArea(unit.currTile.index, unit.moveRange);
                                    //SysHelper.WaitForAndExecute(1f, () => UpdateNeighboursAndHighlightArea(currentSelected[0].GetComponent<Unit>()));
                            }
                            // Feed Fire?
                            else if (currentSelected[0].GetComponent<FeedFire>() && hoveredTile.tag == TagHandler.buildingBonfireString)
                            {
                                ResourceManager.FeedFire(1, PlayerResources.current.bonfire.transform.parent.GetComponent<Resource>());
                            }
                            else if (currentSelected[0].GetComponent<ConsumeFire>() && hoveredTile.tag == TagHandler.buildingBonfireString)
                            {
                                ResourceManager.ConsumeFirePower(1, PlayerResources.current.bonfire.transform.parent.GetComponent<Resource>(), currentSelected[0].GetComponent<Unit>());
                            }
                        }
                    }
                    else
                    {
                        DeselectAll();
                        Select(clickedObj);
                    }
                }

            }


            //if (Input.GetMouseButtonDown(0))
            //{
            //    var clickedObj = GetClickedObject();
            //    if (clickedObj)
            //    {
            //        Debug.Log("Clicked on " + clickedObj.name);

            //        Selectable _selectable = clickedObj.gameObject.GetComponent<Selectable>();
            //        if (_selectable != null)
            //        {
            //            Select(clickedObj);
            //            if (highlightArea.Count > 0)
            //            {
            //                if (highlightArea.Contains(hoveredTile))
            //                {
            //                    var unit = currentSelected[0].GetComponent<Unit>();
            //                    if (unit.remainingMoves > 0)
            //                    {
            //                        unit.destTile = hoveredTile;
            //                        unit.path = movePath;
            //                    }
            //                }
            //            }         
            //            if (neighbours.Contains(hoveredTile))
            //            {
            //                if (hoveredTile.GetComponent<Resource>())
            //                    ResourceManager.Gather(currentSelected[0].GetComponent<Unit>(), hoveredTile.GetComponent<Resource>());       
            //            }
            //            else
            //            {
            //                DeselectAll();
            //                Select(clickedObj.gameObject);
            //            }
            //        }

            //        // If left clicked on not selectable / ground / other things
            //        // deselect everything
            //        else
            //        {
            //            DeselectAll();
            //        }
            //    }                    
            //}
        }        
    }

    private void UpdateHoveredTile(Tile tile)
    {
        if (hoveredTile)
        {
            SelectionStatusHandler vH = hoveredTile.GetComponent<SelectionStatusHandler>();
            vH.ChangeSelectionStatus(Tile.SelectionStatus.Default);
        }
        hoveredTile = tile;
        // This event gets picked up by the visual handlers of hovered tile and
        // sets its tile visibility
        

        // Update Highlight Area
        if (highlightArea.Count > 0)
        {
            foreach (var item in highlightArea)
            {
                SelectionStatusHandler vH = item.GetComponent<SelectionStatusHandler>();
                vH.ChangeSelectionStatus(Tile.SelectionStatus.Highlighted);
            }
            if (highlightArea.Contains(hoveredTile))
            {
                //_aStar.
                var search = new AStarSearch(_grid, currentSelected[0].GetComponent<Unit>().currTile.index, hoveredTile.index); 
                movePath = search.FindPath();
                // Then Update Path
                if (movePath.Count > 0)
                {
                    foreach (var item in movePath)
                    {
                        Tile _t = _grid.TileAt(item);
                        SelectionStatusHandler vH = _t.GetComponent<SelectionStatusHandler>();
                        vH.ChangeSelectionStatus(Tile.SelectionStatus.Path);
                    }
                }
            }           
        }
        SelectionStatusHandler vHnew = hoveredTile.GetComponent<SelectionStatusHandler>();
        vHnew.ChangeSelectionStatus(Tile.SelectionStatus.Hovered);
    }

    public void SetSelectManagerActive(bool value)
    {
        isActive = value;
        if (isActive)
            Debug.Log("Selection Manager activated");
        else
            Debug.Log("Selection Manager deactivated");
    }


    private void Select(GameObject obj)
    {
        _selectable = obj.GetComponent<Selectable>();
        _selectable.IsSelected = true;

        // Add object to selection array if not selected yet
        if (!currentSelected.Contains(obj))
        {
            currentSelected.Add(obj);
        }

        if (obj.layer == 8) // Tile Layer 
        {
            Tile tile = obj.GetComponent<Tile>();
            EventHandler.current.TileSelected(tile);

            SelectionStatusHandler vH = obj.GetComponent<SelectionStatusHandler>();
            vH.ChangeSelectionStatus(Tile.SelectionStatus.Selected);
            //if (highlightArea.Contains(hoveredTile))
            //{
            //    currentSelected[0].GetComponent<Unit>().destTilePos = hoveredTile;
            //}
        }
        if (obj.layer == 9) // PlayerUnit Layer 
        {
            Unit unit = obj.GetComponent<Unit>();
            EventHandler.current.SelectUnit(unit);

            // If the unit can gather resources add the neighbour tiles
            // to the neighbour list
            UpdateNeighboursAndHighlightArea();
        }
        Debug.Log("Selected " + obj.name);
    }

    public void UpdateNeighboursAndHighlightArea()
    {
        if (currentSelected.Count > 0)
        {
            Unit unit = null;
            if (currentSelected[0].GetComponent<Unit>())
                unit = currentSelected[0].GetComponent<Unit>();

            GetNeighbours(unit);

            foreach (var item in highlightArea)
            {
                SelectionStatusHandler vH = item.GetComponent<SelectionStatusHandler>();
                vH.ChangeSelectionStatus(Tile.SelectionStatus.Default);
            }
            highlightArea = unit.GetMoveAreaTilesByIndices();
            foreach (var item in highlightArea)
            {
                SelectionStatusHandler vH = item.GetComponent<SelectionStatusHandler>();
                vH.ChangeSelectionStatus(Tile.SelectionStatus.Highlighted);
            }
            Debug.Log("Updated Neighbours and Highlight Area");
        }


    }

    private void GetNeighbours(Unit unit)
    {
        neighbours.Clear();
        var allNeighbours = _grid.Neighbours(unit.currTile);

        if (unit.GetComponent<GatherResource>() != null)
        {
            foreach (var neighbour in allNeighbours)
            {
                if (neighbour.GetComponent<Resource>() && !neighbours.Contains(neighbour))
                {
                    neighbours.Add(neighbour);
                }
            }
        }
        if (unit.GetComponent<FeedFire>() != null)
        {
            foreach (var neighbour in allNeighbours)
            {
                if (neighbour.tag == TagHandler.buildingBonfireString && !neighbours.Contains(neighbour))
                {
                    neighbours.Add(neighbour);
                }
            }
        }
        if (unit.GetComponent<ConsumeFire>() != null)
        {
            foreach (var neighbour in allNeighbours)
            {
                if (neighbour.tag == TagHandler.buildingBonfireString && !neighbours.Contains(neighbour))
                {
                    neighbours.Add(neighbour);
                }
            }
        }
        if (unit.GetComponent<CookSoup>() != null)
        {
            foreach (var neighbour in allNeighbours)
            {
                if (neighbour.tag == TagHandler.buildingCauldronString && !neighbours.Contains(neighbour))
                {
                    neighbours.Add(neighbour);
                }
            }
        }
    }

    private void DeselectAll()
    {
        EventHandler.current.DeselectAll();
        ResetSelection();
        ResetHighLightArea();
        neighbours.Clear();
    }

    private void ResetSelection()
    {
        if (currentSelected.Count > 0)
        {
            foreach (GameObject obj in currentSelected)
            {
                _selectable = obj.GetComponent<Selectable>();
                _selectable.IsSelected = false;
                if (obj.layer == 8)
                {
                    SelectionStatusHandler vH = obj.GetComponent<SelectionStatusHandler>();
                    vH.ChangeSelectionStatus(Tile.SelectionStatus.Default);
                }
            }
            Debug.Log("Deselected all current selected objects");
        }
        currentSelected.Clear();
    }

    private void ResetHighLightArea()
    {
        if (highlightArea.Count > 0)
        {
            foreach (var item in highlightArea)
            {
                SelectionStatusHandler vH = item.GetComponent<SelectionStatusHandler>();
                vH.ChangeSelectionStatus(Tile.SelectionStatus.Default);
            }
        }
        highlightArea.Clear();
    }

    public GameObject GetClickedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        GameObject clickedObj = null;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                clickedObj = hit.collider.gameObject;
            }
        }
        return clickedObj;
    }

    public static Vector3 GetMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return new Vector3(hit.point.x, 0, hit.point.z);
        }
        return Vector3.zero;
    }
}
