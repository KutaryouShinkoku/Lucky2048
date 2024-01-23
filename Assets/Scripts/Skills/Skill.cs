using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public Skill_Base Base { get; set; }
    public int SkillPar { get; set; }

    public Skill(Skill_Base hBase)
    {
        Base = hBase;
    }
    //public int Skillpar
    //{
    //    get{ return skillPar; }
    //}
}
