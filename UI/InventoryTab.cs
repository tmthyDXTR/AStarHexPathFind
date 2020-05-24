using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// The building menu
/// </summary>
public class InventoryTab : MonoBehaviour
{
    SelectionManager _selection;

    //private InventoryManager _inventory;
    public GameObject buttonPrefab;

    private void OnEnable()
    {
        _selection = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        //_inventory = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        buttonPrefab = (GameObject)Resources.Load("UI/InventoryButton");
        EventHandler.current.HoverOverUIStart();
    }

    //public void UpdateInventoryTab()
    //{
    //    // Create a Button for every item in the building manager list
    //    foreach (var itemEntry in InventoryManager.inventoryItems)
    //    {
    //        var buttonObj = Instantiate(buttonPrefab, this.transform);
    //        var buttonText = buttonObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    //        buttonText.text = "[" + itemEntry.Value.ToString() + "] " + itemEntry.Key.ToString();
    //        var button = buttonObj.GetComponent<InventoryButton>();
    //        button.itemID = itemEntry.Key;
    //        //button.onClick.RemoveAllListeners();
    //        //button.onClick.AddListener(() => _inventory.SelectBuildingToBuild(itemEntry.Key, itemEntry.Value));
    //    }


    //    Debug.Log("Inventory tab updated");
    //}



    private void OnDestroy()
    {
        EventHandler.current.HoverOverUIEnd();
    }
}
