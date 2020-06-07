using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// The unit skill tab 
/// </summary>
public class UnitSkillTab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Unit.Id unitId;

    public Skills unitSkills;

    private Transform skillSlotContainer;

    [SerializeField]
    private SkillSlot skillSlot_1;
    [SerializeField]
    private SkillSlot skillSlot_2;
    [SerializeField]
    private SkillSlot skillSlot_3;

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

    private void OnEnable()
    {
        skillSlotContainer = transform.Find("SkillSlotContainer");

        switch (unitId)
        {
            case Unit.Id.LumberJack:
                this.unitSkills = GameObject.Find("Lumber Jack").GetComponent<Skills>();
                break;
        }

        RefreshSkillTab();
    }


    private void RefreshSkillTab()
    {
        Debug.Log("Refresh Skill Tab for " + unitId);

        skillSlot_1 = skillSlotContainer.Find("Skill_1").GetComponent<SkillSlot>();
        if (unitSkills.unitSkill_1)
        {
            skillSlot_1.skill = unitSkills.unitSkill_1;
            skillSlot_1.transform.Find("Image").gameObject.SetActive(true);
            skillSlot_1.transform.Find("Image").GetComponent<Image>().sprite = unitSkills.unitSkill_1.sprite;
        }

        skillSlot_2 = skillSlotContainer.Find("Skill_2").GetComponent<SkillSlot>();
        if (unitSkills.unitSkill_2)
        {
            skillSlot_2.skill = unitSkills.unitSkill_2;
            skillSlot_2.transform.Find("Image").gameObject.SetActive(true);
            skillSlot_2.transform.Find("Image").GetComponent<Image>().sprite = unitSkills.unitSkill_2.sprite;
        }

        skillSlot_3 = skillSlotContainer.Find("Skill_3").GetComponent<SkillSlot>();
        if (unitSkills.unitSkill_3)
        {
            skillSlot_3.skill = unitSkills.unitSkill_3;
            skillSlot_3.transform.Find("Image").gameObject.SetActive(true);
            skillSlot_3.transform.Find("Image").GetComponent<Image>().sprite = unitSkills.unitSkill_3.sprite;
        }
    }
}