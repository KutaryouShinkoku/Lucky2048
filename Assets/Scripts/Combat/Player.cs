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
    [SerializeField] int playerMaxHP; //Ѫ��
    [Header("UI")]
    [SerializeField] Text txtPlayerHp;
    public int playerHP;//��ʼѪ��
    public int playerArmor;//��ʼ����
    public static event Action<int> OnDamageTaken; // �����¼�
    void Start()
    {
        playerHP = playerMaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        txtPlayerHp.text = $"{playerHP}/{playerMaxHP}";
    }
    public void TakeDamage(int damage)
    {
        playerHP -= damage;
        playerHP = Mathf.Max(0, playerHP); // ȷ��Ѫ�������ɸ���
                                           // ����������������˶�����Ӧ
        OnDamageTaken?.Invoke(damage); // ���������¼�
    }
    
}
