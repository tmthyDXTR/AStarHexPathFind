using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    private GameObject unitPanelPrefab;
    private GameObject tilePanelPrefab;
    private GameObject mouseHoverPanelPrefab;


    // Runtime instantiated panels
    [SerializeField]
    private GameObject unitPanel;
    [SerializeField]
    private GameObject tilePanel;
    [SerializeField]
    GameObject mouseHoverPanel;

    void Start()
    {
        unitPanelPrefab = (GameObject)Resources.Load("UI/UnitPanel");
        tilePanelPrefab = (GameObject)Resources.Load("UI/TilePanel");
        mouseHoverPanelPrefab = (GameObject)Resources.Load("UI/MouseHoverPanel");

        EventHandler.current.onFireConsumed += () => CreateMouseHoverPanel(SelectionManager.hoveredTile);
        EventHandler.current.onFireFed += () => CreateMouseHoverPanel(SelectionManager.hoveredTile);
        EventHandler.current.onHoverOverTile += CreateMouseHoverPanel;
        EventHandler.current.onTileSelected += CreateMouseHoverPanel;
        EventHandler.current.onDeselectedAll += DestroyAllPanels;
    }
    private void OnDestroy()
    {
        EventHandler.current.onFireConsumed -= () => CreateMouseHoverPanel(SelectionManager.hoveredTile);
        EventHandler.current.onFireFed -= () => CreateMouseHoverPanel(SelectionManager.hoveredTile);
        EventHandler.current.onHoverOverTile -= CreateMouseHoverPanel;
        EventHandler.current.onTileSelected -= CreateMouseHoverPanel;
        EventHandler.current.onDeselectedAll -= DestroyAllPanels;
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
    }

    private void CreateMouseHoverPanel(Tile tile)
    {
        if (mouseHoverPanel)
        {
            GameObject.Destroy(mouseHoverPanel);
        }

        var panelPos = new Vector3(tile.transform.position.x, 4, tile.transform.position.z);
        this.transform.position = panelPos;
        mouseHoverPanel = Instantiate(mouseHoverPanelPrefab, panelPos, Camera.main.transform.rotation, GameObject.Find("UI_WorldSpaceCanvas").transform);
        var mouseHoverAddInfo = mouseHoverPanel.transform.Find("mouseHoverAddInfo").gameObject;
        
        if (tile.tag != TagHandler.buildingWoodStorageString && tile.tag != TagHandler.buildingBonfireString)
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
