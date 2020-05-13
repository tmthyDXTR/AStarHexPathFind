using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TilePanel : MonoBehaviour
{
    [SerializeField]
    private Tile selectedTile;

    private TextMeshProUGUI tileName;
    private TextMeshProUGUI tileEffect;


    void OnEnable()
    {
        tileName = transform.Find("TileNameText").GetComponent<TextMeshProUGUI>();
        tileEffect = transform.Find("TileEffectText").GetComponent<TextMeshProUGUI>();


        //EventHandler.current.onTi += UpdatePanel;
    }


    public void UpdatePanel(Tile tile)
    {
        selectedTile = tile;
        if (selectedTile)
        {
            tileName.text = $"Unit: {tile.property}";
            tileEffect.text = $"Effect: blabla";
            Debug.Log("Updated Tile panel");
        }
        else
        {
            Debug.Log("No tile selected: panel not updated");
        }
    }
}
