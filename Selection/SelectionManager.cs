using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    /// <summary>
    /// This class handles the selection through input
    /// </summary>
    /// 
    private Grid _grid;
    private Selectable _selectable;
    //private AStarSearch _aStar;

    #region Variables
    [SerializeField]
    public bool isActive = true;
    [SerializeField]
    private Tile hoveredTile;

    public List<GameObject> currentSelected = new List<GameObject>();
    public List<Tile> highlightArea = new List<Tile>();
    public List<CubeIndex> movePath = new List<CubeIndex>();


    #endregion

    void Start()
    {
        _grid = GameObject.Find("HexGen").GetComponent<Grid>();
        //_aStar = _grid.GetComponent<AStarSearch>();
        // Events
        EventHandler.current.onHoverOverTile += UpdateHoveredTile;
    }

    private void OnDestroy()
    {
        EventHandler.current.onHoverOverTile -= UpdateHoveredTile;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var clickedObj = GetClickedObject();
                if (clickedObj)
                {
                    Debug.Log("Clicked on " + clickedObj.name);

                    // Check if Hit Box collider clicked
                    Selectable _selectable = clickedObj.gameObject.GetComponent<Selectable>();
                    if (_selectable != null)
                    {
                        if (highlightArea.Count > 0)
                        {
                            if (highlightArea.Contains(hoveredTile))
                            {
                                currentSelected[0].GetComponent<Unit>().destTile = hoveredTile;
                                currentSelected[0].GetComponent<Unit>().path = movePath;
                            }
                        }                        
                        DeselectAll();
                        Select(clickedObj.gameObject);                                                     
                    }

                    // If left clicked on not selectable / ground / other things
                    // deselect everything
                    else
                    {
                        DeselectAll();
                    }
                }                    
            }
        }        
    }

    private void UpdateHoveredTile(CubeIndex index)
    {
        if (hoveredTile)
        {
            VisualHandler vH = hoveredTile.GetComponent<VisualHandler>();
            vH.ChangeVisibility(Tile.Visibility.Default);
        }
        hoveredTile = _grid.TileAt(index);
        // This event gets picked up by the visual handlers of hovered tile and
        // sets its tile visibility
        

        // Update Highlight Area
        if (highlightArea.Count > 0)
        {
            foreach (var item in highlightArea)
            {
                VisualHandler vH = item.GetComponent<VisualHandler>();
                vH.ChangeVisibility(Tile.Visibility.Highlighted);
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
                        Tile tile = _grid.TileAt(item);
                        VisualHandler vH = tile.GetComponent<VisualHandler>();
                        vH.ChangeVisibility(Tile.Visibility.Path);
                    }
                }
            }           
        }
        VisualHandler vHnew = hoveredTile.GetComponent<VisualHandler>();
        vHnew.ChangeVisibility(Tile.Visibility.Hovered);
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
        if (obj.layer == 8) // Tile Layer 
        {
            Tile tile = obj.GetComponent<Tile>();
            EventHandler.current.SelectTile(tile);

            VisualHandler vH = obj.GetComponent<VisualHandler>();
            vH.ChangeVisibility(Tile.Visibility.Selected);
            //if (highlightArea.Contains(hoveredTile))
            //{
            //    currentSelected[0].GetComponent<Unit>().destTilePos = hoveredTile;
            //}
        }
        if (obj.layer == 9) // PlayerUnit Layer 
        {
            Unit unit = obj.GetComponent<Unit>();
            EventHandler.current.SelectUnit(unit);

            highlightArea = unit.GetMoveAreaTilesByIndices();
            foreach (var item in highlightArea)
            {
                VisualHandler vH = item.GetComponent<VisualHandler>();
                vH.ChangeVisibility(Tile.Visibility.Highlighted);
            }
        }
        // Add object to selection array if not selected yet
        if (!currentSelected.Contains(obj))
        {
            currentSelected.Add(obj);
        }        
        Debug.Log("Selected " + obj.name);
    }

    private void DeselectAll()
    {
        EventHandler.current.DeselectAll();

        if (currentSelected.Count > 0)
        {
            foreach (GameObject obj in currentSelected)
            {
                _selectable = obj.GetComponent<Selectable>();
                _selectable.IsSelected = false;
                if (obj.layer == 8)
                {
                    VisualHandler vH = obj.GetComponent<VisualHandler>();
                    vH.ChangeVisibility(Tile.Visibility.Default);
                }                
            }
            Debug.Log("Deselected all current selected objects");
        }
        currentSelected.Clear();
        if (highlightArea.Count > 0)
        {
            foreach(var item in highlightArea)
            {
                VisualHandler vH = item.GetComponent<VisualHandler>();
                if (item.lightFromList.Count > 0)
                    vH.ChangeVisibility(Tile.Visibility.Default);
                else
                {
                    vH.ChangeVisibility(Tile.Visibility.Hidden);
                }
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

    public Vector3 GetMousePos()
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
