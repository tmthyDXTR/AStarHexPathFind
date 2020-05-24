using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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
    private RectTransform currItemBox;

    private void OnEnable()
    {
        nameBox = (GameObject)Resources.Load("UI/UI_InventoryItemHoverName");
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On Drop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<Transform>().position = GetComponent<Transform>().position;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse enter inv item");
        // Fix namebox positioning     
        currItemBox = Instantiate(nameBox, this.transform.position, Quaternion.identity, transform.parent.parent).GetComponent<RectTransform>();
        currItemBox.anchoredPosition = new Vector2(0, 30);
        if (item != null)
        {
            currItemBox.Find("NameText").GetComponent<TextMeshProUGUI>().text = item.nameText;
        }
        else
        {
            currItemBox.Find("NameText").GetComponent<TextMeshProUGUI>().text = type + " Slot";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exit inv item");
        if (currItemBox)
        {
            GameObject.Destroy(currItemBox.gameObject);
        }
    }
}
