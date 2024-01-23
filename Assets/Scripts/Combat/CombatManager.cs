using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState //���׶�
{
    none, //�ս׶Σ�����
    select, //ѡ����
    preroll, //ҡ֮ǰ�Ľ׶Σ������֮���
    roll, //ҡ�ϻ���
    combine, //2048
    end, //��һغϽ��������㷽�飬�ȵ����ٹ���
    enemy, //���˻غ�
}
public class CombatManager : MonoBehaviour
{
    public int playerHP; //��ʱ����ת����player�ű�
    public int playerArmor; //��ʱ����ת����player�ű�
    public int enemyHP; //��ʱ����ת����enemy�ű�
    public int enemyArmor; //��ʱ����ת����enemy�ű�
    private CombatState state;
    public void Start()
    {
        playerArmor = 0;
        enemyArmor = 0;
        state = CombatState.none;
    }
    public void Update()
    {
        //�غϽ�����ʼ������
        if(state == CombatState.end)
        {
            //�ȴ����������
            //Ȼ�����δ�����ļ���
            //����Ϊ������ȡĿ�꣨�Ѽ���Ŀ�긽�����飩-���㷽�鼼�ܣ���cube�ű���-�����ܶ�Ŀ��Ľ��
            DeathCheck();
            state = CombatState.enemy;
        }

        //���˻غ�
        if(state == CombatState.enemy)
        {
            //������˵�Buff
            //Ȼ������˵��ж�
            DeathCheck();
            state = CombatState.select;
        }
    }

    //��Ϸ�������ж�
    public void DeathCheck() //�����һ��ߵ����Ƿ�������
    {

    }
    public void GameEnd()
    {
        //������ûд��ʤ�������Ȳ���
        state = CombatState.none;
    }
}
