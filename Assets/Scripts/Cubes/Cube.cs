using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Buff;
using static BuffUIManager;
using static CombatManager;

[System.Serializable]

public class Cube
{
    [SerializeField] Cube_Base _base;
    public Cube_Base Base { get { return _base; } }
    public List<CubeSkill> skills;
    private CombatManager combatManager;
    public void Setup(CombatManager combatManager)
    {
        this.combatManager = combatManager;
        Init(); // �����Ҫ��Ҳ�������������Init
    }

    //ʵװ����
    public void Init()
    {
        skills = new List<CubeSkill>();
        foreach (var skill in Base.CubeSkill)
        {
            //ʵװ����
            if (skills.Count <= skills.Capacity) { skills.Add(skill); }
            else break;
        }
    }

    public void ResolveSkills() //���㼼��
    {
        foreach(var skill in skills)
        {
            HandleSkill(skill.SkillBase.Effects,skill.SkillPar, skill.SkillBase);
            Debug.Log($"������ܣ�{skill.SkillBase.SkillName}������Ϊ{skill.SkillPar}");
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

    public void HandleSkill(SkillEffects effect, int skillPar,Skill_Base skill) //����ÿһ�����ܵľ��崦���ʺɽ����
    {
        switch (effect)
        {
            case SkillEffects.damage:
                //����˺�
                Debug.Log($"�����{skillPar}���˺�");
                combatManager.enemy.TakeDamage(skillPar); // ʹ��CombatManager��ʵ��������enemy
                break;
            case SkillEffects.armor:
                //����
                combatManager.player.AddArmor(skillPar);
                break;
            case SkillEffects.ApplyWeakness:
                //����
                combatManager.enemy.AddBuff(new Buff(BuffType.Weakness, skillPar, skill.Duration));
                break;
            case SkillEffects.ApplyThorns:
                //����
                combatManager.player.AddBuff(new Buff(BuffType.Thorns, skillPar, skill.Duration));
                break;
            case SkillEffects.ApplyBuffer:
                //����
                combatManager.player.AddBuff(new Buff(BuffType.Buffer, skillPar, skill.Duration));
                break;
            case SkillEffects.ApplyPoison:
                //�ж�
                combatManager.enemy.AddBuff(new Buff(BuffType.Poison, skillPar, skill.Duration));
                break;
            case SkillEffects.ApplyBreakdown:
                //�Ƽ�
                combatManager.enemy.AddBuff(new Buff(BuffType.Breakdown, skillPar, skill.Duration));
                break;
            case SkillEffects.ApplyStun:
                //ѣ��
                combatManager.enemy.AddBuff(new Buff(BuffType.Stun, skillPar, skill.Duration));
                break;
            case SkillEffects.ApplyApple:
                //ƻ���˺���������
                combatManager.enemy.AddBuff(new Buff(BuffType.Levelup, skillPar, skill.Duration));
                break;
            case SkillEffects.ApplyHorse:
                //�����ƶ���������
                break;





        }
    }
}
