using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //敌人ai在这里写就行
    [SerializeField] int maxHP; //血量
    [SerializeField] List<Buff> buffs; //身上的Buff
    public int hp;

    [Header("UI")]
    [SerializeField] Text txtEnemyHp;
    
    void Start()
    {
        hp = maxHP;
    }

    
    void Update()
    {
        txtEnemyHp.text = $"{hp}/{maxHP}";
    }
}
