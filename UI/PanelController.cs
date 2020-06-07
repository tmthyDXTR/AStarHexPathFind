using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    private GameObject unitPanelPrefab;
    private GameObject tilePanelPrefab;
    private GameObject mouseHoverPanelPrefab;
    private GameObject buildingTabPrefab;
    private GameObject inventoryTabPrefab;
    private GameObject unitInventoryTabPrefab;
    private GameObject unitSkillTabPrefab;


    // Runtime instantiated panels
    [SerializeField]
    private GameObject unitPanel;
    [SerializeField]
    private GameObject tilePanel;
    [SerializeField]
    GameObject mouseHoverPanel;
    [SerializeField]
    GameObject buildingTab; 
    [SerializeField]
    GameObject inventoryTab;
    [SerializeField]
    GameObject unitInventoryTab;
    [SerializeField]
    GameObject unitSkillTab;

    void Start()
    {
        unitPanelPrefab = (GameObject)Resources.Load("UI/UnitPanel");
        tilePanelPrefab = (GameObject)Resources.Load("UI/TilePanel");
        mouseHoverPanelPrefab = (GameObject)Resources.Load("UI/MouseHoverPanel");
        buildingTabPrefab = (GameObject)Resources.Load("UI/BuildingTab");
        inventoryTabPrefab = (GameObject)Resources.Load("UI/UI_Inventory");
        unitInventoryTabPrefab = (GameObject)Resources.Load("UI/UI_UnitInventory");
        unitSkillTabPrefab = (GameObject)Resources.Load("UI/UI_UnitSkillTab");

        EventHandler.current.onFireConsumed += () => CreateMouseHoverPanel(SelectionManager.hoveredTile);
        EventHandler.current.onFireFed += () => CreateMouseHoverPanel(SelectionManager.hoveredTile);
        EventHandler.current.onHoverOverTile += CreateMouseHoverPanel;
        //EventHandler.current.onTileSelected += CreateMouseHoverPanel;
        EventHandler.current.onDeselectedAll += DestroyAllPanels;
        EventHandler.current.onResourceDestroyed += () => SysHelper.WaitForAndExecute(0.5f, () => CreateMouseHoverPanel(SelectionManager.hoveredTile));
        EventHandler.current.onHoverOverDarkness += CreateMouseHoverPanel;

        EventHandler.current.onOpenedBuildingTab += CreateBuildingTab;
        EventHandler.current.onOpenedInventoryTab += CreateInventoryTab;

        EventHandler.current.onSelectedBuildingToBuild += DestroyAllPanels;
        EventHandler.current.onConstructionWorkDone += CreateMouseHoverPanel;
        EventHandler.current.onItemEquipped += CreateInventoryTab;
        EventHandler.current.onItemUnequipped += CreateInventoryTab;

        EventHandler.current.onUnitSelected += CreateUnitUI;
    }
    private void OnDestroy()
    {
        EventHandler.current.onFireConsumed -= () => CreateMouseHoverPanel(SelectionManager.hoveredTile);
        EventHandler.current.onFireFed -= () => CreateMouseHoverPanel(SelectionManager.hoveredTile);
        EventHandler.current.onHoverOverTile -= CreateMouseHoverPanel;
        //EventHandler.current.onTileSelected -= CreateMouseHoverPanel;
        EventHandler.current.onResourceDestroyed -= () => SysHelper.WaitForAndExecute(0.5f, () => CreateMouseHoverPanel(SelectionManager.hoveredTile));
        EventHandler.current.onDeselectedAll -= DestroyAllPanels;
        EventHandler.current.onHoverOverDarkness -= CreateMouseHoverPanel;

        EventHandler.current.onOpenedBuildingTab -= CreateBuildingTab;
        EventHandler.current.onOpenedInventoryTab -= CreateInventoryTab;
        EventHandler.current.onSelectedBuildingToBuild -= DestroyAllPanels;
        EventHandler.current.onConstructionWorkDone -= CreateMouseHoverPanel;
        EventHandler.current.onItemEquipped -= CreateInventoryTab;
        EventHandler.current.onItemUnequipped -= CreateInventoryTab;

        EventHandler.current.onUnitSelected -= CreateUnitUI;


    }
    private void DestroyAllPanels()
    {
        if (unitPanel)
        {
            GameObject.Destroy(unitPanel);
        }
        if (tilePanel)
        {
            GameObject.Destroy(tilePanel);
        }
        if (mouseHoverPanel)
        {
            GameObject.Destroy(mouseHoverPanel);
        }
        if (buildingTab)
        {
            GameObject.Destroy(buildingTab);
        }
        if (inventoryTab)
        {
            GameObject.Destroy(inventoryTab);
        }
        if (unitInventoryTab)
        {
            GameObject.Destroy(unitInventoryTab);
        }
        if (unitSkillTab)
        {
            GameObject.Destroy(unitSkillTab);
        }
    }

    private void CreateBuildingTab()
    {
        DestroyAllPanels();
        var panelPos = new Vector3(SelectionManager.hoveredTile.transform.position.x, 4.25f, SelectionManager.hoveredTile.transform.position.z);
        buildingTab = Instantiate(buildingTabPrefab, panelPos, Camera.main.transform.rotation, GameObject.Find("UI_WorldSpaceCanvas").transform);
        buildingTab.GetComponent<BuildingTab>().UpdateBuildingTab();
    }
    private void CreateInventoryTab()
    {
        unitInventoryTab = Instantiate(unitInventoryTabPrefab, GameObject.Find("UI_PanelCanvas").transform);

        inventoryTab = Instantiate(inventoryTabPrefab, GameObject.Find("UI_PanelCanvas").transform);
        EventHandler.current.HoverOverUIStart();
    }

    private void CreateUnitUI(Unit unit)
    {
        DestroyAllPanels();
        CreateUnitInventoryTab(unit);
        CreateUnitSkillTab(unit);
    }
    private void CreateUnitInventoryTab(Unit unit)
    {
        unitInventoryTab = Instantiate(unitInventoryTabPrefab, GameObject.Find("UI_PanelCanvas").transform);
        unitInventoryTab.transform.position += new Vector3(-120, -120, 0);
    }
    private void CreateUnitSkillTab(Unit unit)
    {
        unitSkillTab = Instantiate(unitSkillTabPrefab, GameObject.Find("UI_PanelCanvas").transform);
        unitSkillTab.transform.position += new Vector3(0, -120, 0);
    }

    private void CreateMouseHoverPanel(Tile tile)
    {
        if (mouseHoverPanel)
        {
            GameObject.Destroy(mouseHoverPanel);
        }
        var panelPos = new Vector3(tile.transform.position.x, 6.25f, tile.transform.position.z);
        //this.transform.position = panelPos;
        mouseHoverPanel = Instantiate(mouseHoverPanelPrefab, panelPos, Camera.main.transform.rotation, GameObject.Find("UI_WorldSpaceCanvas").transform);
        var mouseHoverAddInfo = mouseHoverPanel.transform.Find("mouseHoverAddInfo").gameObject;
        
        if (tile.tag != TagHandler.buildingWoodStorageString && tile.tag != TagHandler.buildingBonfireString && tile.tag != TagHandler.buildingStoneStorageString && tile.tag != TagHandler.buildingConstructionString)
            mouseHoverAddInfo.SetActive(false);

        mouseHoverPanel.GetComponent<MouseHoverPanel>().UpdatePanelText(tile);

    }

    private void CreateUnitPanel(Unit unit)
    {
        unitPanel = Instantiate(unitPanelPrefab, this.transform);
        unitPanel.GetComponent<UnitPanel>().UpdatePanel(unit, ResourceManager.ResourceType.Wood);
    }

    private void CreateTilePanel(Tile tile)
    {
        tilePanel = Instantiate(tilePanelPrefab, this.transform);
        tilePanel.GetComponent<TilePanel>().UpdatePanel(tile);
    }



}
