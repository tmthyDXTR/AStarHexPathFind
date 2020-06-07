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

    [SerializeField]
    private List<Item> collectedItems = new List<Item>();
    private void Start()
    {
        ///
        ///Inventory Debug
        ///

        Item item = (Item)Resources.Load("Items/Item_GlowingTreeSeed");
        Item item2 = (Item)Resources.Load("Items/Weapon_WilfredWaldfried");
        AddItemToInventory(item, 1, true);
        AddItemToInventory(item2, 1, true);

        EquipItem(item2, GameObject.Find("Lumber Jack"));
        RemoveItemFromInventory(item2);
        //StartCoroutine(SysHelper.WaitForAndExecute(2, () => EquipItem(item2, GameObject.Find("Lumber Jack"))));

        //StartCoroutine(SysHelper.WaitForAndExecute(2, () => UnequipItem(item, GameObject.Find("Lumber Jack"))));
        //StartCoroutine(SysHelper.WaitForAndExecute(5, () => UnequipItem(item2, GameObject.Find("Lumber Jack"))));
    }

    public Dictionary<Item, int> GetInventoryItems()
    {
        return inventoryItems;
    }



    public static void UnequipItem(Item item, GameObject unitObj, int slot = 0)
    {
        UnitInventory unitInventory = unitObj.GetComponent<UnitInventory>();
        unitInventory.equippedItems.Remove(item);
        switch (item.type)
        {
            case Item.Type.Weapon:
                unitInventory.SetWeaponItem(null);
                break;
            case Item.Type.Curiosity:
                if (slot == 1)
                {
                    unitInventory.SetCuriosityItem1(null);
                }
                if (slot == 2)
                {
                    unitInventory.SetCuriosityItem2(null);
                }
                break;
        }
        foreach (var effect in item.effects)
        {
            unitObj.GetComponent<Effects>().unitEffects.Remove(effect);
            EffectManager.DeinitializeEffect(effect, unitObj);
        }
        //AddItemToInventory(item);
        Debug.Log(unitObj.name + " unequipped " + item);
    }
    public static void EquipItem(Item item, GameObject unitObj, int slot = 0)
    {
        UnitInventory unitInventory = unitObj.GetComponent<UnitInventory>();
        unitInventory.equippedItems.Add(item);

        switch (item.type)
        {
            case Item.Type.Weapon:
                unitInventory.SetWeaponItem(item);
                break;
            case Item.Type.Curiosity:
                if (slot == 1)
                {
                    unitInventory.SetCuriosityItem1(item);
                }
                if (slot == 2)
                {
                    unitInventory.SetCuriosityItem2(item);
                }
                break;
        }

        foreach (var effect in item.effects)
        {
            unitObj.GetComponent<Effects>().unitEffects.Add(effect);
            EffectManager.InitializeEffect(effect, unitObj);
        }
        //RemoveItemFromInventory(item);
        Debug.Log(unitObj.name + " equipped " + item);
    }

    public static void AddItemToInventory(Item item, int amount = 1, bool newItem = false)
    {
        if (inventoryItems.ContainsKey(item))
        {
            inventoryItems[item] += amount;
        }
        else
        {
            inventoryItems.Add(item, amount);
        }
        if (newItem)
        {
            GameObject.Find("InventoryManager").GetComponent<InventoryManager>().collectedItems.Add(item);
        }
        // Event
        EventHandler.current.ItemAddedToInventory();

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
                EventHandler.current.ItemRemovedFromInventory();

                Debug.Log("Removed " + amount + " " + item + " from Inventory.");

            }
        }
    }

}
