using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/// <summary>
/// The building menu
/// </summary>
public class InventoryTab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

        EventHandler.current.onItemAddedToInventory += RefreshInventoryItems;
        EventHandler.current.onItemRemovedFromInventory += RefreshInventoryItems;

    }


    private void OnDestroy()
    {
        EventHandler.current.HoverOverUIEnd();

        EventHandler.current.onItemAddedToInventory -= RefreshInventoryItems;
        EventHandler.current.onItemRemovedFromInventory -= RefreshInventoryItems;

    }

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

    private void RefreshInventoryItems()
    {
        // First clear inventory slots
        for (int i = 1; i < itemSlotContainer.childCount; i++)
        {
            GameObject.Destroy(itemSlotContainer.GetChild(i).gameObject);
        } 
        // Then instantiate new slots
        float x = -0.5f;
        float y = 0;
        float itemSlotCellSize = 30f;
        var inventoryItems = inventory.GetInventoryItems();
        foreach (var itemEntry in inventoryItems)
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.GetComponent<InventorySlot>().item = itemEntry.Key;
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            x+=2;
            if (x > 9)
            {
                x = 0;
                y-=1.8f;
            }
            itemSlotRectTransform.transform.Find("Image").GetComponent<Image>().sprite = itemEntry.Key.sprite;

        }

        for (int i = 0; i < 12 - inventoryItems.Count; i++)
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            x += 2;
            if (x > 10.5f)
            {
                x = 0.5f;
                y -= 1.8f;
            }
            itemSlotRectTransform.transform.Find("Image").gameObject.SetActive(false);
        }
    }

}
