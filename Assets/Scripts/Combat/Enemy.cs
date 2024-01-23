using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //����ai������д����
    [SerializeField] int maxHP; //Ѫ��
    [SerializeField] List<Buff> buffs; //���ϵ�Buff
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
