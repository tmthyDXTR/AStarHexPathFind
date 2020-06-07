using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager current;

    public enum SkillId
    {
        None,
        Skill_AxeAttack,
    }


    public static Dictionary<SkillId, Skill> skillDict = new Dictionary<SkillId, Skill>();

    void Awake()
    {
        current = this;

        // Add all skills to the skill dictionary at start
        skillDict.Add(SkillId.Skill_AxeAttack, (Skill)Resources.Load("Skills/" + SkillId.Skill_AxeAttack));

    }
}
