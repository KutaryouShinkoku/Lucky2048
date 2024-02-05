using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static Buff;
using static CombatManager;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : MonoBehaviour
{
    [SerializeField] private List<Buff> buffs; //���ϵ�Buff
    public int playerHP;//��ʼѪ��
    public int playerMaxHP; //Ѫ��
    public int playerMaxArmor; //����
    public int playerArmor;//��ʼ����
    public int playerBuffer;//�������
    public bool IsBuffer { get; set; }//�Ƿ񻺳�
    public static event Action<int> OnDamageTaken; // �����¼�
    public AK.Wwise.Event MyEvent1;
    public AK.Wwise.Event MyEvent2;
  
    void Start()
    {   
        playerHP = playerMaxHP;
        playerArmor = 0;
        playerBuffer = 0;
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
                MyEvent1.Post(gameObject);//有护甲
            }
            else
            {
                playerArmor = 0;// ���Ż���������Ч
                MyEvent2.Post(gameObject);
                if (!IsBuffer)
                {
                    playerHP -= (damage - playerArmor); // ��������������Ч

                }
                else
                {
                    playerBuffer -= 1;
                }
            }
        }
        else
        {
            playerHP -= damage;
            // ��������ֵ������Ч
            MyEvent2.Post(gameObject);
        }

        playerHP = Mathf.Max(0, playerHP); // ȷ��Ѫ�������ɸ���

        //int damageTaken = originalHP - playerHP; // ����ʵ���ܵ����˺�
        //if (damageTaken > 0)
        //{
        //    OnDamageTaken?.Invoke(damageTaken); // ֻ�е�ʵ���ܵ��˺�ʱ�Ŵ����¼�
        //}
    }
    private void UpdatePlayerHealthUI(int damage)
    {
        // �����������ֵUI
    }
    public void AddBuff(Buff newBuff)//��buff
    {
        buffs.Add(newBuff);
    }
    public void RemoveBuff(Buff newBuff)//�Ƴ�buff
    { 
        buffs.Remove(newBuff); 
    }
    public void ProcessBuffs() 
    {
        if (playerBuffer == 0)
        {
            //�Ƴ�Buffer���Buff
            //buffs.RemoveAll(buff => buff.type == "Buffer");
        }
    }
    public void AddArmor(int AddA) 
    {
        playerArmor = Mathf.Clamp(playerArmor + AddA, 0, 99);
        Debug.Log($"��Ҽӻ���");
    }

    private void OnDestroy()
    {
        // ��ֹ�ڴ�й©��ȷ���ڶ�������ʱȡ������
        OnDamageTaken -= UpdatePlayerHealthUI;
    }


}
