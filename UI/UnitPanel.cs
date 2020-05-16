using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;

public class UnitPanel : MonoBehaviour
{
    [SerializeField]
    private Unit selectedUnit;

    private TextMeshProUGUI unitName;
    private TextMeshProUGUI unitMoves;
    

    private void OnEnable()
    {
        unitName = transform.Find("UnitNameText").GetComponent<TextMeshProUGUI>();
        unitMoves = transform.Find("UnitMovesText").GetComponent<TextMeshProUGUI>();

        EventHandler.current.onResourceGathered += UpdatePanel;
    }
    private void OnDisable()
    {
        EventHandler.current.onResourceGathered -= UpdatePanel;

    }

    public void UpdatePanel(Unit unit, ResourceManager.ResourceType type)
    {
        selectedUnit = unit;
        if (selectedUnit)
        {
            unitName.text = $"Unit: {selectedUnit.gameObject.name}";
            unitMoves.text = $"Moves: {selectedUnit.remainingMoves}";
            Debug.Log("Updated Unit panel");
        }
        else
        {
            Debug.Log("No unit selected: panel not updated");
        }
    }
}
