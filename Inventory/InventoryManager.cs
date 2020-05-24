using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;


/// <summary>
/// This class handles the Inventory and item transfer methods
/// </summary>
public class InventoryManager : MonoBehaviour
{
    // Holds all current items in inventory with amount
    private static Dictionary<Item, int> inventoryItems = new Dictionary<Item, int>();

    private void Start()
    {
        Item item = (Item)Resources.Load("Items/FireTreeSeed");
        inventoryItems.Add(item, 1);

        EquipItem(item, GameObject.Find("Lumber Jack"));

        StartCoroutine(SysHelper.WaitForAndExecute(2, () => UnequipItem(item, GameObject.Find("Lumber Jack"))));
    }


    public static void UnequipItem(Item item, GameObject obj)
    {
        obj.GetComponent<UnitInventory>().equippedItems.Remove(item);

        foreach (var effect in item.effects)
        {
            obj.GetComponent<Effects>().unitEffects.Remove(effect);
            EffectManager.DeinitializeEffect(effect, obj);
        }

        Debug.Log(obj.name + " unequipped " + item);
    }
    public static void EquipItem(Item item, GameObject obj)
    {
        obj.GetComponent<UnitInventory>().equippedItems.Add(item);

        foreach (var effect in item.effects)
        {
            obj.GetComponent<Effects>().unitEffects.Add(effect);
            EffectManager.InitializeEffect(effect, obj);
        }

        Debug.Log(obj.name + " equipped " + item);
    }

    public static void AddItemToInventory(Item item, int amount = 1)
    {
        if (inventoryItems.ContainsKey(item))
        {
            inventoryItems[item] += amount;
        }
        else
        {
            inventoryItems[item] = amount;
        }

        // Event


        Debug.Log("Added " + amount + " " + item + " to Inventory.");
    }

    public static void RemoveItemFromInventory(Item item, int amount = 1)
    {
        if (inventoryItems.ContainsKey(item))
        {
            if (inventoryItems[item] >= amount)
            {
                inventoryItems[item] -= amount;
                if (inventoryItems[item] == 0)
                {
                    inventoryItems.Remove(item);
                }

                // Event


                Debug.Log("Removed " + amount + " " + item + " from Inventory.");

            }
        }
    }

}
