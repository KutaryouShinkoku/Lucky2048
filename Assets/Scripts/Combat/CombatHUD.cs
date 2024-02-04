using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatHUD : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Text txtPlayerHp;
    [SerializeField] Slider sldPlayerHP;
    [SerializeField] Text txtPlayerArmor;
    [SerializeField] Slider sldPlayerArmor;
    [SerializeField] Text txtEnemyHp;
    [SerializeField] Slider sldEnemyHP;
    [SerializeField] Text txtEnemyArmor;
    [SerializeField] Slider sldEnemyArmor;

    [Header("Game")]
    public  CombatManager manager;
    public  Player player;
    public  Enemy enemy;
    //[SerializeField] Text txtNextAction; // 显示下一回合敌人的意图

    // Start is called before the first frame update
    void Start()
    {
        player = manager.player;
        enemy = manager.enemy;
        InitializeHUD();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHp();
    }

    void InitializeHUD()
    {
        //血条初始化
        sldPlayerHP.maxValue = player.playerMaxHP;
        sldPlayerHP.value = player.playerMaxHP;
        sldPlayerArmor.maxValue = player.playerMaxArmor;
        sldPlayerArmor.value = player.playerArmor;
        sldEnemyHP.maxValue = enemy.enemyMaxHP;
        sldEnemyHP.value = enemy.enemyMaxHP;
        sldEnemyArmor.maxValue = enemy.enemyMaxArmor;
        sldEnemyArmor.value = enemy.enemyArmor;
    }
    public void UpdateHp()
    {
        txtPlayerHp.text = $"{player.playerHP}/{player.playerMaxHP}";
        txtPlayerArmor.text = $"{player.playerArmor}/{player.playerMaxArmor}";
        txtEnemyHp.text = $"{enemy.enemyHP}/{enemy.enemyMaxHP}";
        txtEnemyArmor.text = $"{enemy.enemyArmor}/{enemy.enemyMaxArmor}";

        sldPlayerHP.value = player.playerHP;
        sldPlayerArmor.value = player.playerArmor;
        sldEnemyHP.value = enemy.enemyHP;
        sldEnemyArmor.value = enemy.enemyArmor;
    }



    public void SetHPBarSmoothly()
    {

    }
}
