using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ����UI�����ռ�

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
    private CombatState state;
    [Header("UI")]
    public CombatHUD combatHUD;
    public Text gameEndText; // ��Ϸ����ʱ��ʾ���ı�

    private int thornsBuffIntensity; // ���� Buff ��ǿ��
    public void Start()
    {
        Instantiate(player);
        Instantiate(enemy);
        InitializePlayerAndEnemy();
        player.playerArmor = 0;
        enemy.enemyArmor = 0;
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
    public void InitializePlayerAndEnemy()
    {

    }

    public void UpdateCombatStats()
    {

    }

    public void UpdateCombatHUD()
    {

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
    }
    public interface IAddBuff
    {
    }
    //��Ϸ�������ж�
    public void DeathCheck() //�����һ��ߵ����Ƿ�������
    {
        if (player.playerHP==0)
        {
            // �����������Ϸʧ��
            GameEnd(false);
        }
        else if (enemy.enemyHP==0)
        {
            // ������������Ϸ��ʤ
            GameEnd(true);
        }
    }
    public void GameEnd(bool playerWon)
    {
        state = CombatState.none; // ֹͣ��Ϸ״̬����
        gameEndText.gameObject.SetActive(true); // ��ʾ��Ϸ�����ı�
        gameEndText.text = playerWon ? "��Ϸʤ����" : "��Ϸʧ�ܣ�"; // ��������Ƿ�Ӯ����Ϸ�������ı�
    }
}
