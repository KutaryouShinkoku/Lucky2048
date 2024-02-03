using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatHUD : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Text txtPlayerHp;
    [SerializeField] Text txtEnemyHp;
    [SerializeField] Text txtPlayerArmor;
    [SerializeField] Text txtEnemyArmor;
    [SerializeField] Text txtNextAction; // 显示下一回合敌人的意图

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHp()
    {

    } 
}
