using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Buff;
using static CombatManager;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : MonoBehaviour
{
    public int playerMaxHP; //Ѫ��
    public int playerMaxArmor; //����
    
    public int playerHP;//��ʼѪ��
    public int playerArmor;//��ʼ����
    public static event Action<int> OnDamageTaken; // �����¼�
    void Start()
    {   
        playerHP = playerMaxHP;
        playerArmor = 0;
        // �����¼�
        OnDamageTaken += UpdatePlayerHealthUI;
    }

    // Update is called once per frame
    void Update()
    {
        ;
    }
    public void TestDmg()
    {
        TakeDamage(5);
    }
    public void TakeDamage(int damage)
    {
        int originalHP = playerHP; // ��¼����ǰ������ֵ

        if (playerArmor > 0)
        {
            if (playerArmor >= damage)
            {
                playerArmor -= damage;
                // ���Ż���������Ч
            }
            else
            {
                playerHP -= (damage - playerArmor);
                playerArmor = 0;
                // ���Ż������������������Ч
            }
        }
        else
        {
            playerHP -= damage;
            // ��������ֵ������Ч
        }

        playerHP = Mathf.Max(0, playerHP); // ȷ��Ѫ�������ɸ���

        int damageTaken = originalHP - playerHP; // ����ʵ���ܵ����˺�
        if (damageTaken > 0)
        {
            OnDamageTaken?.Invoke(damageTaken); // ֻ�е�ʵ���ܵ��˺�ʱ�Ŵ����¼�
        }
    }
    private void UpdatePlayerHealthUI(int damage)
    {
        // �����������ֵUI
    }

    private void OnDestroy()
    {
        // ��ֹ�ڴ�й©��ȷ���ڶ�������ʱȡ������
        OnDamageTaken -= UpdatePlayerHealthUI;
    }


}
