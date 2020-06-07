using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Button _btn;
    TextMeshProUGUI _btnText;
    void Start()
    {
        _btn = GetComponent<Button>();
        _btnText = _btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _btn.onClick.RemoveAllListeners();
        _btn.onClick.AddListener(EndTurn);
    }


    private void EndTurn()
    {
        GameManager.current.EndTurn();
        _btnText.text = GameManager.current.gameState.ToString();
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


    private void OnDestroy()
    {
        _btn.onClick.RemoveAllListeners();
    }
}
