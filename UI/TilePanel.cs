using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TilePanel : MonoBehaviour
{
    [SerializeField]
    private Tile selectedTile;

    private TextMeshProUGUI tileName;
    private TextMeshProUGUI tileResourceAmount;
    private TextMeshProUGUI tileResourceWorkCost;


    void OnEnable()
    {
        tileName = transform.Find("TileNameText").GetComponent<TextMeshProUGUI>();
        tileResourceAmount = transform.Find("TileResourceAmountText").GetComponent<TextMeshProUGUI>();
        tileResourceWorkCost = transform.Find("TileResourceWorkCostText").GetComponent<TextMeshProUGUI>();


        //EventHandler.current.onTi += UpdatePanel;
    }


    public void UpdatePanel(Tile tile)
    {
        selectedTile = tile;
        if (selectedTile)
        {
            tileName.text = $"Tile: {tile.property}";
            if(selectedTile.GetComponent<Resource>())
            {
                tileResourceAmount.text = $"Amount: {tile.GetComponent<Resource>().Amount}";
                tileResourceWorkCost.text = $"WorkCost: {tile.GetComponent<Resource>().WorkCost}";
            }                
            Debug.Log("Updated Tile panel");
        }
        else
        {
            Debug.Log("No tile selected: panel not updated");
        }
    }
}
