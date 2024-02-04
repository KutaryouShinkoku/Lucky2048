using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using static Buff;
using static CombatManager;

[System.Serializable]

public class Cube
{
    [SerializeField] Cube_Base _base;
    public Cube_Base Base { get { return _base; } }
    public List<Skill> skills;
    private CombatManager combatManager;
    public void Setup(CombatManager combatManager)
    {
        this.combatManager = combatManager;
        Init(); // 如果需要，也可以在这里调用Init
    }

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
                combatManager.enemy.TakeDamage(skillPar); // 使用CombatManager的实例来访问enemy
                break;
            case SkillEffects.armor:
                //叠甲
                break;
            case SkillEffects.ApplyWeakness:
                //虚弱
                combatManager.enemy.AddBuff(new Buff(BuffType.Weakness, skillPar, skill.Duration));
                break;
            case SkillEffects.ApplyThorns:
                //荆棘
                combatManager.player.AddBuff(new Buff(BuffType.Thorns, skillPar, skill.Duration));
                break;
            case SkillEffects.ApplyBuffer:
                //缓冲
                combatManager.player.AddBuff(new Buff(BuffType.Buffer, skillPar, skill.Duration));
                break;
            case SkillEffects.ApplyPoison:
                //中毒
                combatManager.enemy.AddBuff(new Buff(BuffType.Buffer, skillPar, skill.Duration));
                break;
            case SkillEffects.ApplyBreakdown:
                //破甲
                combatManager.enemy.AddBuff(new Buff(BuffType.Buffer, skillPar, skill.Duration));
                break;
            case SkillEffects.ApplyStun:
                //眩晕
                combatManager.enemy.AddBuff(new Buff(BuffType.Buffer, skillPar, skill.Duration));
                break;
            case SkillEffects.ApplyApple:
                //苹果伤害倍率提升
                combatManager.enemy.AddBuff(new Buff(BuffType.Buffer, skillPar, skill.Duration));
                break;
            case SkillEffects.ApplyHorse:
                //骏马移动次数提升
                break;





        }
    }
}
