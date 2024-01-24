using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cube", menuName = "Cube/Create a new cube")]
public class Cube_Base : ScriptableObject
{
    [SerializeField] string cubeKey; //cubeΨһ���ƣ�����ʶ��cube
    [SerializeField] string cubeName; //����
    [SerializeField] int level; //����
    [SerializeField] CubeRarity cubeRarity; //ϡ�ж�
    [SerializeField] CubeRace cubeRace; //����
    [SerializeField] CubeType cubeType; //���ࣨ���������ȣ�

    [TextArea]
    [SerializeField] string description;  //����
    [SerializeField] Sprite sprite;  //��ͼ

    [SerializeField] List<CubeSkill> cubeSkill; //�����б�

    //ÿ�ζ������µĲ����ǵ����·�����һ��
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

    public string Description
    {
        get { return $"{Localize.GetInstance().GetTextByKey($"{description}")}"; }
    }

    
    public List<CubeSkill> CubeSkill
    {
        get { return cubeSkill; }
    }
}

//����ļ���
[System.Serializable]
public class CubeSkill
{
    [SerializeField] Skill_Base skillBase;
    [SerializeField] int skillPar; //���ܲ���

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
    normal, //��ͨ
    rare, //ϡ��
    epic, //����
    legend, //����
}

public enum CubeRace //ֻ����Ҫ�õ�������������Ҫ��������൱�ڶ���tag
{
    none, //ɶ��û
    bee, //�۷���
    apple, //�ӹ�ƻ��
}

public enum CubeType
{
    attack, //������
    defend, //������
    special, //������
}

