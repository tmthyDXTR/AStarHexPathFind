using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel.DataAnnotations;

public class ItemManager : MonoBehaviour
{

    public static Dictionary<ID, Item> itemDict = new Dictionary<ID, Item>();

    void Awake()
    {
        // Add all items to the item dictionary at start
        itemDict.Add(ID.Item_GlowingTreeSeed, (Item)Resources.Load("Items/Item_GlowingTreeSeed"));
        itemDict.Add(ID.Item_OldGrindingStone, (Item)Resources.Load("Items/Item_OldGrindingStone"));

    }


    public enum ID
    {
        None,
        // Items
        Item_OldGrindingStone,
        Item_GlowingTreeSeed,

        // Weapons
        Weapon_WilfredWaldfried,

    }
    
    public static Item GetItem(ID id)
    {
        Item item = (Item)Resources.Load("Items/" + id);
        return item;
    }

    public void SpawnItemAtTile(Tile tile, ItemManager.ID item)
    {        
        if (item != ItemManager.ID.None)
        {
            var itemObj = CreateItemObject(tile.transform, ItemManager.GetItem(item));

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
