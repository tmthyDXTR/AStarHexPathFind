using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInventory : MonoBehaviour
{
    public List<Item> equippedItems = new List<Item>();

    [SerializeField]
    private Item weaponItem;
    [SerializeField]
    private Item curiosityItem1;
    [SerializeField]
    private Item curiosityItem2;

    public Item GetWeaponItem()
    {
        return weaponItem;
    }
    public Item GetcuriosityItem1()
    {
        return curiosityItem1;
    }
    public Item GetcuriosityItem2()
    {
        return curiosityItem2;
    }

    public void SetWeaponItem(Item item)
    {
        weaponItem = item;
    }
    public void SetCuriosityItem1(Item item)
    {
        curiosityItem1 = item;
    }
    public void SetCuriosityItem2(Item item)
    {
        curiosityItem2 = item;
    }

}
