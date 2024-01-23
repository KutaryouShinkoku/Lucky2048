using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int playerHP; //临时，后转移至player脚本
    public int playerArmor; //临时，后转移至player脚本
    public int enemyHP; //临时，后转移至enemy脚本
    public int enemyArmor; //临时，后转移至enemy脚本
    private CombatState state;
    public void Start()
    {
        playerArmor = 0;
        enemyArmor = 0;
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

    //游戏结束的判定
    public void DeathCheck() //检查玩家或者敌人是否有人死
    {

    }
    public void GameEnd()
    {
        //案子里没写获胜奖励，先不管
        state = CombatState.none;
    }
}
