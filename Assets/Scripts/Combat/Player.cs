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
    public int playerMaxHP; //血量
    public int playerMaxArmor; //护甲
    
    public int playerHP;//初始血量
    public int playerArmor;//初始护甲
    public static event Action<int> OnDamageTaken; // 受伤事件
    void Start()
    {   
        playerHP = playerMaxHP;
        playerArmor = 0;
        // 订阅事件
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
        int originalHP = playerHP; // 记录受伤前的生命值

        if (playerArmor > 0)
        {
            if (playerArmor >= damage)
            {
                playerArmor -= damage;
                // 播放护甲受损音效
            }
            else
            {
                playerHP -= (damage - playerArmor);
                playerArmor = 0;
                // 播放护甲受损和生命受损音效
            }
        }
        else
        {
            playerHP -= damage;
            // 播放生命值受损音效
        }

        playerHP = Mathf.Max(0, playerHP); // 确保血量不会变成负数

        int damageTaken = originalHP - playerHP; // 计算实际受到的伤害
        if (damageTaken > 0)
        {
            OnDamageTaken?.Invoke(damageTaken); // 只有当实际受到伤害时才触发事件
        }
    }
    private void UpdatePlayerHealthUI(int damage)
    {
        // 更新玩家生命值UI
    }

    private void OnDestroy()
    {
        // 防止内存泄漏，确保在对象销毁时取消订阅
        OnDamageTaken -= UpdatePlayerHealthUI;
    }


}
