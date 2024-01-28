using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public Skill_Base Base { get; set; }
    public int SkillPar { get; set; }
    public int Duration { get; private set; } // 添加持续时间字段

    public Skill(Skill_Base hBase)
    {
        Base = hBase;
        Duration = hBase.Duration; // 从 Skill_Base 获取持续时间
    }
    //public int Skillpar
    //{
       //get{ return skillPar; }
    //}
}
