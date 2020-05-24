using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// The building menu
/// </summary>
public class InventoryTab : MonoBehaviour
{

    private InventoryManager inventory;

    [SerializeField]
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");

        inventory = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();

        
    }

    private void OnEnable()
    {
        RefreshInventoryItems();
        EventHandler.current.HoverOverUIStart();

    }
    private void OnDestroy()
    {
        EventHandler.current.HoverOverUIEnd();
    }

    private void RefreshInventoryItems()
    {
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 30f;
        foreach (var itemEntry in inventory.GetInventoryItems())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.GetComponent<InventorySlot>().item = itemEntry.Key;
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            x+=2;
            if (x > 12)
            {
                x = 0;
                y+=2;
            }
            //var itemSlot = Instantiate(itemSlotContainer, this.transform);
            itemSlotRectTransform.transform.Find("Image").GetComponent<Image>().sprite = itemEntry.Key.sprite;
            itemSlotRectTransform.transform.Find("Image").gameObject.AddComponent<DragDrop>();

        }
    }

}
