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

    //public enum Effect
    //{
    //    None,
    //    AddLightSource,
    //    AddMoveRange,
    //    MinusMoveRange,

    //}
    

    public void SpawnItemAtTile(Tile tile, Item item)
    {        
        //var item = CreateItemObject(tile.transform, itemID);

        tile.item = item;
        //var pos = tile.transform.position;
        //item.transform.position = pos;

        Debug.Log(item + " spawned at " + tile);        
    }

    
}
