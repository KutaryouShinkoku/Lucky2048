using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public Skill_Base Base { get; set; }
    public int SkillPar { get; set; }
    public int Duration { get; private set; } // ��ӳ���ʱ���ֶ�

    public Skill(Skill_Base hBase)
    {
        Base = hBase;
        Duration = hBase.Duration; // �� Skill_Base ��ȡ����ʱ��
    }
    //public int Skillpar
    //{
       //get{ return skillPar; }
    //}
}
