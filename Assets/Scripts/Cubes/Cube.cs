using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using static Buff;

[System.Serializable]

public class Cube
{
    [SerializeField] Cube_Base _base;
    public Cube_Base Base { get { return _base; } }
    public List<Skill> skills;
    public Enemy target; //��Ϊcube��Ŀ�꣬��combatmanager�����鸳��Ŀ��

    //ʵװ����
    public void Init()
    {
        skills = new List<Skill>();
        foreach (var skill in Base.CubeSkill)
        {
            //ʵװ����
            if (skills.Count <= skills.Capacity) { skills.Add(new Skill(skill .SkillBase)); }
            else break;
        }
    }

    public void ResolveSkills() //���㼼��
    {
        foreach(var skill in skills)
        {
            HandleSkill(skill.Base.Effects,skill.SkillPar, skill);
            Debug.Log($"�����ܣ�{skill.Base.SkillName}������Ϊ{skill.SkillPar}");
        }
    }

    public int CalculateDamage() //�����˺�
    {
        int damage = 0;
        //������ɵĻ����˺�

        //�۷�ӳ�

        //ƻ���ӳ�

        return damage;
    }

    public int CalculateArmor() //���㻤��
    {
        int armor = 0;
        //����

        return armor;
    }

    public void HandleSkill(SkillEffects effect, int skillPar,Skill skill) //����ÿһ�����ܵľ��崦��ʺɽ����
    {
        switch (effect)
        {
            case SkillEffects.damage:
                //����˺�
                Debug.Log($"�����{skillPar}���˺�");
                target.enemyHP -= skillPar;
                break;
            case SkillEffects.armor:
                //����
                break;
            case SkillEffects.ApplyWeakness:
                //����
                target.AddBuff(new Buff(Buff.BuffType.Weakness, skillPar, skill.Duration));
                break;
            case SkillEffects.ApplyThorns:
                //����
                target.AddBuff(new Buff(BuffType.Thorns, skillPar, 1));
                break;
               

        }
    }
}
