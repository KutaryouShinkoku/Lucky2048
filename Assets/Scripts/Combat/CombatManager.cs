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
    public Player player;
    public Enemy enemy;
    public int playerHP; //��ʱ����ת����player�ű�
    public int playerArmor; //��ʱ����ת����player�ű�
    public int enemyHP; //��ʱ����ת����enemy�ű�
    public int enemyArmor; //��ʱ����ת����enemy�ű�
    private CombatState state;
    private int thornsBuffIntensity; // ���� Buff ��ǿ��
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
    private void HandleThornsEffect(int damage)
    {
        if (thornsBuffIntensity > 0)
        {
            enemy.TakeDamage(thornsBuffIntensity); // �Ե�����ɾ����˺�
        }
    }

    public void AddThornsBuff(int intensity)
    {
        thornsBuffIntensity += intensity; // ���Ӿ��� Buff ǿ��
    }
    public interface IDamageable
    {
        void TakeDamage(int damage);
        void AddBuff(Buff buff);
        // ���������Ӹ��������˺���صķ���
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
