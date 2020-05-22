using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel.DataAnnotations;

public class ItemManager : MonoBehaviour
{
    public GameObject itemDummyPrefab;



    void Awake()
    {
        itemDummyPrefab = (GameObject)Resources.Load("Items/ItemDummy");

    }


    public enum ItemID
    {
        None,
        FireTreeSeed,
    }

    public enum Effect
    {
        None,
        LightSoure,


    }

    public void SpawnItemAtTile(Tile tile, ItemID itemID, int amount=1)
    {
        if (itemID != ItemID.None)
        {
            var item = CreateItemObject(tile, itemID);

            tile.item = itemID;
            var pos = tile.transform.position;
            item.transform.position = pos;

            Debug.Log(amount + " " + itemID + " spawned at " + tile);
        }  
        else
        {

        }
    }

    private GameObject CreateItemObject(Tile tile, ItemID itemID)
    {
        // Instantiate Item dummy at tile position
        var itemObj = Instantiate(itemDummyPrefab, tile.transform);

        
        // Initialize Item properties
        InitializeItem(itemObj, itemID);


        return itemObj;
    }



    private void InitializeItem(GameObject itemObj, ItemID itemID)
    {
        // Add Item script
        var itemInfo = itemObj.AddComponent<ItemInfo>();

        var item = (Item)Resources.Load("Items/" + itemID);
        itemObj.name = "Item";

        itemInfo.title = item.title;
        itemInfo.infoText = item.infoText;

        Instantiate(item.prefab, itemObj.transform);

        if (item.effect_1 != ItemManager.Effect.None)
        {
            if (item.effect_1 == ItemManager.Effect.LightSoure)
            {
                var lightSource = itemObj.AddComponent<LightSource>();
                lightSource.LightRange = item.effect1Range;
            }
        }
        if (item.effect_2 != ItemManager.Effect.None)
        {

        }
        if (item.effect_3 != ItemManager.Effect.None)
        {

        }
    }
}
