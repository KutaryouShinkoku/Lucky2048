using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cube", menuName = "Cube/Create a new cube")]
public class Cube_Base : ScriptableObject
{
    [SerializeField] string cubeKey; //cube唯一名称，用来识别cube
    [SerializeField] string cubeName; //名称
    [SerializeField] int level; //阶数
    [SerializeField] Cube nextLevelCube;
    [SerializeField] Cube lastLevelCube;

    [SerializeField] CubeRarity cubeRarity; //稀有度
    [SerializeField] CubeRace cubeRace; //种族
    [SerializeField] CubeType cubeType; //种类（攻击防御等）

    [TextArea]
    [SerializeField] string description;  //描述
    [SerializeField] Sprite sprite;  //贴图

    [SerializeField] List<CubeSkill> cubeSkill; //技能列表

    //每次定义了新的参数记得在下方开放一下
    public string CubeKey
    {
        get { return cubeKey; }
    }
    public string CubeName
    {
        get { return $"{Localize.GetInstance().GetTextByKey($"{cubeName}")}"; }
    }
    public int Level
    {
        get { return level; }
    }
    public Cube NextLevelCube
    {
        get { return nextLevelCube; }
    }
    public Cube LastLevelCube
    {
        get { return lastLevelCube; }
    }
    public string Description
    {
        get { return $"{Localize.GetInstance().GetTextByKey($"{description}")}"; }
    }
    public Sprite Sprite
    {
        get { return sprite; }
    }
    
    public List<CubeSkill> CubeSkill
    {
        get { return cubeSkill; }
    }
}

//方块的技能
[System.Serializable]
public class CubeSkill
{
    [SerializeField] Skill_Base skillBase;
    [SerializeField] int skillPar; //技能参数

    public Skill_Base SkillBase
    {
        get { return skillBase; }
    }
    public int SkillPar
    {
        get { return skillPar; }
    }
}

public enum CubeRarity
{
    normal, //普通
    rare, //稀有
    epic, //罕见
    legend, //传奇
}

public enum CubeRace //只有需要用到的特殊种族需要加这个，相当于额外tag
{
    none, //啥都没
    bee, //蜜蜂类
    apple, //加攻苹果
}

public enum CubeType
{
    attack, //进攻型
    defend, //防御型
    special, //特殊型
}

