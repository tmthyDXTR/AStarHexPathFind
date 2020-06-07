using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// The unit inventory 
/// </summary>
public class UnitInventoryTab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Unit.Id unitId;

    public UnitInventory inventory;

    private Transform itemSlotContainer;

    [SerializeField]
    private InventorySlot unitWeaponSlot;
    [SerializeField]
    private InventorySlot unitCuriositySlot1;
    [SerializeField]
    private InventorySlot unitCuriositySlot2;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse entered Button");
        EventHandler.current.HoverOverUIStart(); // used to activate and deactivate selection manager
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse exited Button");
        EventHandler.current.HoverOverUIEnd();
    }

    private void OnEnable()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        //EventHandler.current.HoverOverUIStart();
        //EventHandler.current.onItemDroppedInUnitInventory += () => RefreshInventoryItems();
        EventHandler.current.onItemDroppedInUnitInventory += RefreshInventoryItems;

        switch (unitId)
        {
            case Unit.Id.LumberJack:
                this.inventory = GameObject.Find("Lumber Jack").GetComponent<UnitInventory>();
                break;
        }

        RefreshInventoryItems();
    }


    public void RefreshInventoryItems(InventorySlot slot = null)
    {
        unitWeaponSlot = itemSlotContainer.Find("WeaponSlot").GetComponent<InventorySlot>();
        if (slot == null || slot == unitWeaponSlot)
        {
            if (inventory.GetWeaponItem())
            {
                unitWeaponSlot.item = inventory.GetWeaponItem();
                unitWeaponSlot.transform.Find("Image").gameObject.SetActive(true);
                unitWeaponSlot.transform.Find("Image").GetComponent<Image>().sprite = inventory.GetWeaponItem().sprite;
            }
        }

        unitCuriositySlot1 = itemSlotContainer.Find("CuriositySlot1").GetComponent<InventorySlot>();
        if (slot == null || slot == unitCuriositySlot1)
        {
            if (inventory.GetcuriosityItem1())
            {
                unitCuriositySlot1.item = inventory.GetcuriosityItem1();
                unitCuriositySlot1.transform.Find("Image").gameObject.SetActive(true);
                unitCuriositySlot1.transform.Find("Image").GetComponent<Image>().sprite = inventory.GetcuriosityItem1().sprite;
            }
        }

        unitCuriositySlot2 = itemSlotContainer.Find("CuriositySlot2").GetComponent<InventorySlot>();
        if (slot == null || slot == unitCuriositySlot2)
        {
            if (inventory.GetcuriosityItem2())
            {
                unitCuriositySlot2.item = inventory.GetcuriosityItem2();
                unitCuriositySlot2.transform.Find("Image").gameObject.SetActive(true);
                unitCuriositySlot2.transform.Find("Image").GetComponent<Image>().sprite = inventory.GetcuriosityItem2().sprite;
            }
        }
    }


    public void CleanSlots()
    {
        if (unitWeaponSlot.item)
        {
            if (!inventory.equippedItems.Contains(unitWeaponSlot.item))
            {
                InventoryManager.AddItemToInventory(unitWeaponSlot.item);
            }
        }
        else if (inventory.GetWeaponItem())
        {
            InventoryManager.RemoveItemFromInventory(inventory.GetWeaponItem());
        }

        if (unitCuriositySlot1.item)
        {
            if (!inventory.equippedItems.Contains(unitCuriositySlot1.item))
            {
                InventoryManager.AddItemToInventory(unitCuriositySlot1.item);
            }
        }
        else if (inventory.GetcuriosityItem1())
        {
            InventoryManager.RemoveItemFromInventory(inventory.GetcuriosityItem1());
        }

        if (unitCuriositySlot2.item)
        {
            if (!inventory.equippedItems.Contains(unitCuriositySlot2.item))
            {
                InventoryManager.AddItemToInventory(unitCuriositySlot2.item);
            }
        }
        else if (inventory.GetcuriosityItem2())
        {
            InventoryManager.RemoveItemFromInventory(inventory.GetcuriosityItem2());
        }
    }

    public Item GetWeapon()
    {
        return unitWeaponSlot.item;
    }
    public Item GetCurio1()
    {
        return unitCuriositySlot1.item;
    }
    public Item GetCurio2()
    {
        return unitCuriositySlot2.item;
    }


    private void OnDestroy()
    {
        CleanSlots();

        EventHandler.current.HoverOverUIEnd();
        //EventHandler.current.onItemDroppedInUnitInventory -= () => RefreshInventoryItems();
        EventHandler.current.onItemDroppedInUnitInventory -= RefreshInventoryItems;

    }
}
