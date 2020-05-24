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


    private void OnEnable()
    {

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
