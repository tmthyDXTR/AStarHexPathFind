using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel.DataAnnotations;

public class ItemManager : MonoBehaviour
{
    public static ItemManager current;

    public Dictionary<ItemId, Item> itemDict = new Dictionary<ItemId, Item>();

    void Awake()
    {
        current = this;

        // Add all items to the item dictionary at start
        itemDict.Add(ItemId.Item_GlowingTreeSeed, (Item)Resources.Load("Items/" + ItemId.Item_GlowingTreeSeed));
        itemDict.Add(ItemId.Item_OldGrindingStone, (Item)Resources.Load("Items/" + ItemId.Item_OldGrindingStone));
        itemDict.Add(ItemId.Weapon_WilfredWaldfried, (Item)Resources.Load("Items/" + ItemId.Weapon_WilfredWaldfried));

    }


    public enum ItemId
    {
        None,
        // Items
        Item_OldGrindingStone,
        Item_GlowingTreeSeed,

        // Weapons
        Weapon_WilfredWaldfried,

    }
    
    public Item GetItem(ItemId id)
    {
        Item item = (Item)Resources.Load("Items/" + id);
        return item;
    }

    public void SpawnItemAtTile(Tile tile, ItemManager.ItemId item)
    {        
        if (item != ItemManager.ItemId.None)
        {
            var itemObj = CreateItemObject(tile.transform, ItemManager.current.GetItem(item));

            tile.item = item;

            Debug.Log(itemObj + " spawned at " + tile);
        }        
    }

    private GameObject CreateItemObject(Transform posTransform, Item item)
    {
        GameObject obj = Instantiate(item.prefab, posTransform.position, Quaternion.identity, posTransform);

        return obj;
    }
}
