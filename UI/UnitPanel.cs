using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitPanel : MonoBehaviour
{
    [SerializeField]
    private Unit selectedUnit;

    private TextMeshProUGUI unitName;
    private TextMeshProUGUI unitMoves;
    

    void OnEnable()
    {
        unitName = transform.Find("UnitNameText").GetComponent<TextMeshProUGUI>();
        unitMoves = transform.Find("UnitMovesText").GetComponent<TextMeshProUGUI>();


        //EventHandler.current.onUnitSelected += UpdatePanel;
    }


    public void UpdatePanel(Unit unit)
    {
        selectedUnit = unit;
        if (selectedUnit)
        {
            unitName.text = $"Unit: {selectedUnit.gameObject.name}";
            unitMoves.text = $"Moves: {selectedUnit.moveRange}";
            Debug.Log("Updated Unit panel");
        }
        else
        {
            Debug.Log("No unit selected: panel not updated");
        }
    }
}
