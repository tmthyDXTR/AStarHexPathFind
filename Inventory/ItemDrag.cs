using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour
{
    public static ItemDrag current { get; private set; }

    private GameObject itemObj;
    private Transform itemDragCanvas;


    [SerializeField]
    private Item item;


    private Sprite sprite;

    public GameObject draggedItem;
    public bool fromBagpack = true;
    public InventorySlot draggedSlot;
    private void Awake()
    {
        current = this;
        itemDragCanvas = GameObject.Find("UI_PanelCanvas").transform;
        itemObj = (GameObject)Resources.Load("UI/Item");
    }


    public void SetItem(Item item, InventorySlot slot = null)
    {
        this.item = item;
        this.sprite = item.sprite;
        this.draggedSlot = slot;
        InstantiateSprite(sprite);
    }

    public void ResetDrag()
    {
        this.item = null;
        this.sprite = null;
        GameObject.Destroy(draggedItem);
        this.draggedItem = null;
        this.fromBagpack = true;
        this.draggedSlot = null;
    }

    public Item GetItem()
    {
        return item;
    }

    private void InstantiateSprite(Sprite sprite)
    {
        var item = Instantiate(itemObj, itemDragCanvas);
        item.transform.localScale -= new Vector3(0.5f, 0.5f, 0);
        item.GetComponent<Image>().sprite = this.sprite;
        item.GetComponent<Image>().raycastTarget = false;
        item.GetComponent<CanvasGroup>().alpha = 0.5f;
        item.GetComponent<CanvasGroup>().blocksRaycasts = false;
        draggedItem = item;
    }
}
