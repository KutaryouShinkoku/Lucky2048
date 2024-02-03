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
    [SerializeField] int playerMaxHP; //血量
    [Header("UI")]
    [SerializeField] Text txtPlayerHp;
    public int playerHP;//初始血量
    public int playerArmor;//初始护甲
    public static event Action<int> OnDamageTaken; // 受伤事件
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
        playerHP = Mathf.Max(0, playerHP); // 确保血量不会变成负数
                                           // 可以在这里添加受伤动画或反应
        OnDamageTaken?.Invoke(damage); // 触发受伤事件
    }
    
}
