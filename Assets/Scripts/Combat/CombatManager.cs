using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 导入UI命名空间

public enum CombatState //各阶段
{
    none, //空阶段，备用
    select, //选方块
    preroll, //摇之前的阶段，检测马之类的
    roll, //摇老虎机
    combine, //2048
    end, //玩家回合结束，结算方块，先叠甲再攻击
    enemy, //敌人回合
}
public class CombatManager : MonoBehaviour
{
    public Player player;
    public Enemy enemy;
    private CombatState state;
    [Header("UI")]
    public CombatHUD combatHUD;
    public Text gameEndText; // 游戏结束时显示的文本

    private int thornsBuffIntensity; // 荆棘 Buff 的强度
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
        //回合结束开始处理方块
        if(state == CombatState.end)
        {
            //先处理稻穗的升级
            //然后依次处理方块的技能
            //流程为给方块取目标（把技能目标附给方块）-结算方块技能（在cube脚本）-处理技能对目标的结果
            DeathCheck();
            state = CombatState.enemy;
        }

        //敌人回合
        if(state == CombatState.enemy)
        {
            //处理敌人的Buff
            //然后处理敌人的行动
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
            enemy.TakeDamage(thornsBuffIntensity); // 对敌人造成荆棘伤害
        }
    }

    public void AddThornsBuff(int intensity)
    {
        thornsBuffIntensity += intensity; // 增加荆棘 Buff 强度
    }
    public interface IDamageable
    {
    }
    public interface IAddBuff
    {
    }
    //游戏结束的判定
    public void DeathCheck() //检查玩家或者敌人是否有人死
    {
        if (player.playerHP==0)
        {
            // 玩家死亡，游戏失败
            GameEnd(false);
        }
        else if (enemy.enemyHP==0)
        {
            // 敌人死亡，游戏获胜
            GameEnd(true);
        }
    }
    public void GameEnd(bool playerWon)
    {
        state = CombatState.none; // 停止游戏状态更新
        gameEndText.gameObject.SetActive(true); // 显示游戏结束文本
        gameEndText.text = playerWon ? "游戏胜利！" : "游戏失败！"; // 根据玩家是否赢得游戏来更新文本
    }
}
