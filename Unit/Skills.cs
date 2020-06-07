using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public Skill selectedSkill;

    public Skill unitSkill_1;
    public Skill unitSkill_2;
    public Skill unitSkill_3;



    public void SelectSkill(Skill skill)
    {
        if (selectedSkill != skill)
        {
            selectedSkill = skill;
            //Event used to activate spell ranges and highlight area
            EventHandler.current.SkillSelected(skill);
            Debug.Log("Selected skill: " + skill.nameText);
        }        
    }

    public void DeselectSkill()
    {
        selectedSkill = null;
    }
}
