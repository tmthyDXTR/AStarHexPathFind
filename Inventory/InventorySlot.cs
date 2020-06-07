using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public Type type;
    public enum Type
    {
        Bagpack,
        Weapon,
        Curiosity,
    }


    private GameObject nameBox;
    private RectTransform currNameBox;

    private void OnEnable()
    {
        nameBox = (GameObject)Resources.Load("UI/UI_InventoryItemHoverName");
    }

    public void OnDrop(PointerEventData eventData)
    {
        var droppedItem = ItemDrag.current.GetItem();
        //this.item = droppedItem;

        if (eventData.pointerDrag != null)
        {
            Debug.Log("OnDrop");
            //eventData.pointerDrag.GetComponent<Transform>().position = GetComponent<Transform>().position;

            //UpdateVisual(item);
        }
        switch (type)
        {
            case Type.Bagpack:
                Debug.Log("Dropped: " + droppedItem + " in Bagpack slot");
                if (!ItemDrag.current.fromBagpack)
                {
                    InventoryManager.AddItemToInventory(droppedItem);
                    this.item = droppedItem;
                    UpdateVisual(item);
                    ClearSlot(ItemDrag.current.draggedSlot);
                }
                break;
            case Type.Weapon:
                Debug.Log("Dropped: " + droppedItem + " in Weapon slot");
                if (ItemDrag.current.fromBagpack)
                {
                    InventoryManager.RemoveItemFromInventory(droppedItem);
                    this.item = droppedItem;
                    UpdateVisual(item);
                }
                EventHandler.current.ItemDroppedInUnitInventory(this);

                break;
            case Type.Curiosity:
                Debug.Log("Dropped: " + droppedItem + " in Curiosity slot");
                if (ItemDrag.current.fromBagpack)
                {
                    InventoryManager.RemoveItemFromInventory(droppedItem);
                    this.item = droppedItem;
                    UpdateVisual(item);
                }
                EventHandler.current.ItemDroppedInUnitInventory(this);

                break;

        }

        ItemDrag.current.ResetDrag();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Fix namebox positioning     
        currNameBox = Instantiate(nameBox, this.transform.position, Quaternion.identity, transform.parent.parent).GetComponent<RectTransform>();
        currNameBox.anchoredPosition = new Vector2(0, 30);
        if (item != null)
        {
            currNameBox.Find("NameText").GetComponent<TextMeshProUGUI>().text = item.nameText;
        }
        else
        {
            currNameBox.Find("NameText").GetComponent<TextMeshProUGUI>().text = type + " Slot";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currNameBox)
        {
            GameObject.Destroy(currNameBox.gameObject);
        }
    }

    void UpdateVisual(Item item)
    {
        Debug.Log("Update Visual.");
        transform.Find("Image").gameObject.SetActive(true);
        transform.Find("Image").GetComponent<Image>().sprite = item.sprite;
    }

    void ClearSlot(InventorySlot slot) 
    {
        Debug.Log("Clear Slot.");
        slot.item = null;
        slot.transform.Find("Image").gameObject.SetActive(false);
    }
}
