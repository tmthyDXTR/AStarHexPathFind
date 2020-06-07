using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MouseHoverPanel : MonoBehaviour
{
    SelectionManager _selection;


    private TextMeshProUGUI hoverPanel;

    void OnEnable()
    {
        _selection = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        hoverPanel = transform.Find("MouseHoverText").GetComponent<TextMeshProUGUI>();

    }


    public void UpdatePanelText(Tile tile)
    {
        if (_selection.neighbours.Contains(tile))
        {

            if (tile.GetComponent<Resource>() && ( tile.tag == TagHandler.resourceWoodString || tile.tag == TagHandler.resourceStoneString))
            {
                Resource resource = tile.GetComponent<Resource>();
                hoverPanel.text = "Gather " + resource.type.ToString();
            }           
            else if (_selection.currentSelected[0].GetComponent<ConsumeFire>() && tile.tag == TagHandler.buildingBonfireString)
            {
                hoverPanel.text = "Consume Fire Power";
                var hoverAddInfoPanelText = this.transform.Find("mouseHoverAddInfo").GetChild(0).gameObject;
                hoverAddInfoPanelText.GetComponent<TextMeshProUGUI>().text = $"{PlayerResources.Fire}/10";

            }
            else if (tile.tag == TagHandler.buildingBonfireString)
            {
                hoverPanel.text = "Feed Bonfire";
                var hoverAddInfoPanelText = this.transform.Find("mouseHoverAddInfo").GetChild(0).gameObject;
                hoverAddInfoPanelText.GetComponent<TextMeshProUGUI>().text = $"{PlayerResources.Fire}/10";
            }
            else if (tile.tag == TagHandler.buildingCauldronString)
            {
                hoverPanel.text = "Start a Brew";
                var hoverAddInfoPanelText = this.transform.Find("mouseHoverAddInfo").GetChild(0).gameObject;
                hoverAddInfoPanelText.GetComponent<TextMeshProUGUI>().text = $"Yummy soup...";
            }
            else if (tile.tag == TagHandler.buildingConstructionString)
            {
                var building = tile.GetComponent<Building>();
                hoverPanel.text = "Construct " + building.property;
                var hoverAddInfoPanelText = this.transform.Find("mouseHoverAddInfo").GetChild(0).gameObject;
                hoverAddInfoPanelText.GetComponent<TextMeshProUGUI>().text = $"Amount: {building.WorkAmount}\nWorkCost:{building.CostWork}";
            }
            else if (tile.item != ItemManager.ItemId.None) // Item?
            {                
                hoverPanel.text = "Collect " + ItemManager.current.GetItem(tile.item).nameText;
            }
            else
            {
                hoverPanel.text = tile.property.ToString();
            }
        }
        else if (tile.unit != null)
        {
            hoverPanel.text = tile.unit.name;
        }
        else if (tile.item != ItemManager.ItemId.None)
        {
            hoverPanel.text = ItemManager.current.GetItem(tile.item).nameText;
        }
        else if (tile.tag == TagHandler.buildingWoodStorageString)
        {
            hoverPanel.text = tile.property.ToString();
            var hoverAddInfoPanelText = this.transform.Find("mouseHoverAddInfo").GetChild(0).gameObject;
            hoverAddInfoPanelText.GetComponent<TextMeshProUGUI>().text = $"{PlayerResources.Wood}/{PlayerResources.woodMax}";
        }
        else if (tile.tag == TagHandler.buildingStoneStorageString)
        {
            hoverPanel.text = tile.property.ToString();
            var hoverAddInfoPanelText = this.transform.Find("mouseHoverAddInfo").GetChild(0).gameObject;
            hoverAddInfoPanelText.GetComponent<TextMeshProUGUI>().text = $"{PlayerResources.Stone}/{PlayerResources.stoneMax}";
        }
        else if (tile.tag == TagHandler.buildingBonfireString)
        {
            hoverPanel.text = tile.property.ToString();
            var hoverAddInfoPanelText = this.transform.Find("mouseHoverAddInfo").GetChild(0).gameObject;
            hoverAddInfoPanelText.GetComponent<TextMeshProUGUI>().text = $"{PlayerResources.Fire}/10";
        }
        else if (tile.tag == TagHandler.buildingConstructionString)
        {
            var building = tile.GetComponent<Building>();
            hoverPanel.text = "Construction " + building.property;
            var hoverAddInfoPanelText = this.transform.Find("mouseHoverAddInfo").GetChild(0).gameObject;
            hoverAddInfoPanelText.GetComponent<TextMeshProUGUI>().text = $"Amount: {building.WorkAmount}\nWorkCost:{building.CostWork}";
        }
        else if (tile.IsDark && !tile.IsDiscoveredFog)
        {
            hoverPanel.text = "The Darkness";
        }
        else if (tile.IsDiscoveredFog)
        {
            hoverPanel.text = "Creeping Blackness";
        }
        else
        {
            hoverPanel.text = tile.property.ToString();
        }
        
    }
}
