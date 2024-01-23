using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Cube/Create a new skill")]
public class Skill_Base : ScriptableObject
{
    [SerializeField] string skillName; //名称

    [SerializeField] SkillType type; //技能类型，用来处理诸如“一个方块技能中的伤害类数值翻倍”的诡异需求
    [SerializeField] SkillEffects effects; //每个同功效技能单独写效果，拆碎一点，拆到每个技能只有一个参数为止

    //每次定义了新的参数记得在下方开放一下
    public string SkillName
    {
        get { return $"{Localize.GetInstance().GetTextByKey($"{skillName}")}"; }
    }
    public SkillType Type
    {
        get { return type; }
    }
    public SkillEffects Effects
    {
        get { return effects; }
    }
}

public enum SkillEffects //效果都放这，一个技能写一个枚举
{
    damage, //伤害
    armor, //叠甲
    buffenemy, //给敌人上buff
}


public enum SkillType //不用全加，有需求再加
{
    none,
    damage, //伤害类技能
}
