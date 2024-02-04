using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Cube/Create a new skill")]
public class Skill_Base : ScriptableObject
{
    [SerializeField] string skillName; //����

    [SerializeField] SkillType type; //�������ͣ������������硰һ�����鼼���е��˺�����ֵ�������Ĺ�������
    [SerializeField] SkillEffects effects; //ÿ��ͬ��Ч���ܵ���дЧ��������һ�㣬��ÿ������ֻ��һ������Ϊֹ
    [SerializeField] private int duration; // ��ӳ���ʱ���ֶ�
    //ÿ�ζ������µĲ����ǵ����·�����һ��
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
    public int Duration
    {
        get { return duration; }
    }

}

public enum SkillEffects //Ч�������⣬һ������дһ��ö��
{
    damage, //�˺�
    armor, //����
    ApplyWeakness,//������
    ApplyThorns,//�Ͼ���
    ApplyBuffer,//�ϻ���
    ApplyPoison,//���ж�
    ApplyStun,//��ѣ��
    ApplyBreakdown,//�ϱ���
    ApplyApple,//ƻ��ר��
}


public enum SkillType //����ȫ�ӣ��������ټ�
{
    none,
    damage, //�˺��༼��
}
