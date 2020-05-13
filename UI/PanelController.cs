using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    private GameObject unitPanelPrefab;
    private GameObject tilePanelPrefab;


    // Runtime instantiated panels
    [SerializeField]
    private GameObject unitPanel;
    [SerializeField]
    private GameObject tilePanel;
    void Start()
    {
        unitPanelPrefab = (GameObject)Resources.Load("UI/UnitPanel");
        tilePanelPrefab = (GameObject)Resources.Load("UI/TilePanel");

        EventHandler.current.onUnitSelected += CreateUnitPanel;
        EventHandler.current.onTileSelected += CreateTilePanel;

        EventHandler.current.onDeselectedAll += DestroyAllPanels;
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
    }

    private void CreateUnitPanel(Unit unit)
    {
        unitPanel = Instantiate(unitPanelPrefab, this.transform);
        unitPanel.GetComponent<UnitPanel>().UpdatePanel(unit);
    }

    private void CreateTilePanel(Tile tile)
    {
        tilePanel = Instantiate(tilePanelPrefab, this.transform);
        tilePanel.GetComponent<TilePanel>().UpdatePanel(tile);
    }


    private void OnDestroy()
    {
        EventHandler.current.onUnitSelected -= CreateUnitPanel;
        EventHandler.current.onTileSelected -= CreateTilePanel;

        EventHandler.current.onDeselectedAll -= DestroyAllPanels;
    }
}
