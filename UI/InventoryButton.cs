using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ItemManager.ItemID itemID;
    private ItemDragHandler dragHandler;
    
    void OnEnable()
    {
        dragHandler = GameObject.Find("InventoryManager").GetComponent<ItemDragHandler>();
    }


    public void OnBeginDrag(PointerEventData eventData) 
    {
        Debug.Log("Begin Item Drag");
        dragHandler.draggedItem = itemID;


    }
    public void OnDrag(PointerEventData eventData) 
    {
        Debug.Log("Item Dragging");
        var dirToCam = (Camera.main.transform.position - SelectionManager.GetMousePos()).normalized;
        this.transform.position = SelectionManager.GetMousePos() + dirToCam * 10;

    }
    public void OnEndDrag(PointerEventData eventData) 
    {
        Debug.Log("End Item Drag");
             

    }


    public void OnPointerEnter(PointerEventData eventData)
    {

        Debug.Log("Mouse Enter " + this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        Debug.Log("Mouse Exit " + this);
    }
}
