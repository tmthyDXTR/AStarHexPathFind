using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// The building menu
/// </summary>
public class UnitInventoryTab : MonoBehaviour
{
    SelectionManager _selection;

    public GameObject buttonPrefab;

    private void OnEnable()
    {

        _selection = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        buttonPrefab = (GameObject)Resources.Load("UI/InventoryButton");
        EventHandler.current.HoverOverUIStart();
    }

    public void UpdateUnitInventoryTab()
    {
        
        // Create a Button for every item in the inventory manager list
        


        Debug.Log("Unit Inventory tab updated");
    }



    private void OnDestroy()
    {
        EventHandler.current.HoverOverUIEnd();
    }
}
