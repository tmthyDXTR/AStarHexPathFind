using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private CanvasGroup canvasGroup;

    private Item item;
    private InventorySlot slot;

    private void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        this.slot = this.transform.parent.GetComponent<InventorySlot>();
        this.item = this.slot.item;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        //canvasGroup.blocksRaycasts = false;
        //canvasGroup.alpha = 0.6f;

        // Start Drag
        ItemDrag.current.SetItem(item, slot);
        if (transform.parent.GetComponent<InventorySlot>().type != InventorySlot.Type.Bagpack)
        {
            ItemDrag.current.fromBagpack = false;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }
    public void OnDrag(PointerEventData eventData)
    {
        //this.transform.position = eventData.position;
        ItemDrag.current.draggedItem.transform.position = eventData.position;
    }

}
