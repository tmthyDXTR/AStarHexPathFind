using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;


/// <summary>
/// This class handles the buildings
/// </summary>
public class BuildingManager : MonoBehaviour
{
    Map _map;
    SelectionManager _selection;

    public Dictionary<Tile.Property, GameObject> buildings = new Dictionary<Tile.Property, GameObject>();

    public GameObject buildingPrefab;
    public Tile.Property buildingProp;
    void Start()
    {
        _map = GameObject.Find("MapGen").GetComponent<Map>();
        _selection = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();

        buildings.Add(Tile.Property.House, _map.housePrefab);
    }

    public void SelectBuildingToBuild(Tile.Property buildingProp, GameObject buildingPrefab)
    {
        _selection.DeselectAll();
        EventHandler.current.SelectBuildingToBuild();
        Debug.Log("Selceted " + buildingProp + " to build.");
        this.buildingPrefab = Instantiate(buildingPrefab);
        this.buildingProp = buildingProp;
    }

    private void Update()
    {
        if (buildingPrefab)
        {
            buildingPrefab.transform.position = SelectionManager.hoveredTile.transform.position;
            if (Input.GetMouseButtonDown(0) && SelectionManager.hoveredTile.tag == TagHandler.walkGroundString)
            {
                _map.SetTilePropTo(SelectionManager.hoveredTile.transform, Tile.Property.Construction);
                SelectionManager.hoveredTile.gameObject.AddComponent<Building>();
                SelectionManager.hoveredTile.GetComponent<Building>().property = buildingProp;
                GameObject.Destroy(buildingPrefab);
                buildingPrefab = null;

            }
            if (Input.GetMouseButtonDown(1))
            {
                GameObject.Destroy(buildingPrefab);
                buildingPrefab = null;
            }
            else
            {
                Debug.Log("Can't build here.");
            }
        }
    }
}
