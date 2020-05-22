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



    public static void ConstructBuilding(Unit unit, Building building)
    {
        if (unit.remainingMoves >= building.CostWork)
        {
            unit.remainingMoves -= building.CostWork;
            building.WorkAmount--;
            Debug.Log("Construction Work done: " + building.property);
            EventHandler.current.ConstructionWorkDone(building.GetComponent<Tile>());
            if (building.WorkAmount < 1)
            {
                // Construction complete
                var _map = GameObject.Find("MapGen").GetComponent<Map>();
                _map.SetTilePropTo(building.transform, building.property);
                Debug.Log("Construction complete: " + building.property);
            }
        }
    }

    public void SelectBuildingToBuild(Tile.Property buildingProp, GameObject buildingPrefab)
    {
        
        this.buildingProp = buildingProp;
        var buildingInfo = new Building();
        buildingInfo.InitializeBuildingInfo(buildingProp);
        if (PlayerResources.current.wood >= buildingInfo.CostWood && PlayerResources.current.stone >= buildingInfo.CostStone)
        {
            this.buildingPrefab = Instantiate(buildingPrefab);
            _selection.DeselectAll();
            EventHandler.current.SelectBuildingToBuild();
            Debug.Log("Selceted " + buildingProp + " to build.");
        }
        else
        {
            Debug.Log("Not enough resources.");
            Talker.TypeThis("Not enough resources.");
        }

    }
    private void Update()
    {
        if (buildingPrefab)
        {

            buildingPrefab.transform.position = SelectionManager.hoveredTile.transform.position;
            if (Input.GetMouseButtonDown(0) && SelectionManager.hoveredTile.tag == TagHandler.walkGroundString)
            {
                var buildingInfo = new Building();
                buildingInfo.InitializeBuildingInfo(buildingProp);
                if (PlayerResources.current.wood >= buildingInfo.CostWood && PlayerResources.current.stone >= buildingInfo.CostStone)
                {
                    ResourceManager.AddRemoveResource(ResourceManager.ResourceType.Wood, buildingInfo.CostWood, MethodHandler.Command.Remove);
                    ResourceManager.AddRemoveResource(ResourceManager.ResourceType.Stone, buildingInfo.CostStone, MethodHandler.Command.Remove);
                    
                    _map.SetTilePropTo(SelectionManager.hoveredTile.transform, Tile.Property.Construction);
                    var building = SelectionManager.hoveredTile.gameObject.AddComponent<Building>();
                    building.property = buildingProp;
                    // Initialize building info
                    building.InitializeBuildingInfo(building.property);
                    EventHandler.current.BeginBuildingConstruction();

                    GameObject.Destroy(buildingPrefab);
                    buildingPrefab = null;
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                GameObject.Destroy(buildingPrefab);
                buildingPrefab = null;
            }
            else
            {
                Debug.Log("Can't build here.");
                Talker.TypeThis("Can't build here.");
            }
        }
    }
}
