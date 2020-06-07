using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    public Skill skill;

    private GameObject nameBox;
    private RectTransform currNameBox;

    void Start()
    {
        nameBox = (GameObject)Resources.Load("UI/UI_InventoryItemHoverName");

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (skill)
        {
            SelectionManager.current.currentSelected[0].GetComponent<Skills>().SelectSkill(skill);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Fix namebox positioning     
        currNameBox = Instantiate(nameBox, this.transform.position, Quaternion.identity, transform.parent.parent).GetComponent<RectTransform>();
        currNameBox.anchoredPosition = new Vector2(0, 30);
        if (skill != null)
        {
            currNameBox.Find("NameText").GetComponent<TextMeshProUGUI>().text = skill.nameText;
        }
        else
        {
            currNameBox.Find("NameText").GetComponent<TextMeshProUGUI>().text = "Skill Slot";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currNameBox)
        {
            GameObject.Destroy(currNameBox.gameObject);
        }
    }
}
