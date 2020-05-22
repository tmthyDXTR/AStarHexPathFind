using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;


/// <summary>
/// This class handles the buildings
/// </summary>
public class InventoryManager : MonoBehaviour
{
    // Holds all current items in inventory with amount
    public static Dictionary<ItemManager.ItemID, int> inventoryItems = new Dictionary<ItemManager.ItemID, int>();


    //private void Start()
    //{
    //    AddItemToInventory(ItemManager.ItemID.FireTreeSeed, 1);
    //}


    public static void AddItemToInventory(ItemManager.ItemID itemID, int amount = 1)
    {
        if (inventoryItems.ContainsKey(itemID))
        {
            inventoryItems[itemID] += amount;
        }
        else
        {
            inventoryItems[itemID] = amount;
        }

        // Event


        Debug.Log("Added " + amount + " " + itemID + " to Inventory.");
    }

    public static void RemoveItemFromInventory(ItemManager.ItemID itemID, int amount = 1)
    {
        if (inventoryItems.ContainsKey(itemID))
        {
            if (inventoryItems[itemID] >= amount)
            {
                inventoryItems[itemID] -= amount;
                if (inventoryItems[itemID] == 0)
                {
                    inventoryItems.Remove(itemID);
                }

                // Event


                Debug.Log("Removed " + amount + " " + itemID + " from Inventory.");

            }
        }
    }
}
