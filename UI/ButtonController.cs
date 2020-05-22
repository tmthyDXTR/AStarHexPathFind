using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Button _button;
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(EndTurn);
    }


    private void EndTurn()
    {
        Debug.Log("Player Turn End.");
        Talker.TypeThis("The darkness slowly creeps in...");

        ResourceManager.EndBurn(1);
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
}
