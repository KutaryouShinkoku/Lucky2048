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
    public Enemy target; //作为cube的目标，由combatmanager给方块赋予目标

    //实装技能
    public void Init()
    {
        skills = new List<Skill>();
        foreach (var skill in Base.CubeSkill)
        {
            //实装技能
            if (skills.Count <= skills.Capacity) { skills.Add(new Skill(skill .SkillBase)); }
            else break;
        }
    }

    public void ResolveSkills() //结算技能
    {
        foreach(var skill in skills)
        {
            HandleSkill(skill.Base.Effects,skill.SkillPar, skill);
            Debug.Log($"处理技能：{skill.Base.SkillName}，参数为{skill.SkillPar}");
        }
    }

    public int CalculateDamage() //计算伤害
    {
        int damage = 0;
        //技能造成的基础伤害

        //蜜蜂加成

        //苹果加成

        return damage;
    }

    public int CalculateArmor() //计算护甲
    {
        int armor = 0;
        //叠甲

        return armor;
    }

    public void HandleSkill(SkillEffects effect, int skillPar,Skill skill) //对于每一个技能的具体处理，屎山部分
    {
        switch (effect)
        {
            case SkillEffects.damage:
                //造成伤害
                Debug.Log($"造成了{skillPar}点伤害");
                target.enemyHP -= skillPar;
                break;
            case SkillEffects.armor:
                //叠甲
                break;
            case SkillEffects.ApplyWeakness:
                //虚弱
                target.AddBuff(new Buff(Buff.BuffType.Weakness, skillPar, skill.Duration));
                break;
            case SkillEffects.ApplyThorns:
                //荆棘
                target.AddBuff(new Buff(BuffType.Thorns, skillPar, 1));
                break;
               

        }
    }
}
