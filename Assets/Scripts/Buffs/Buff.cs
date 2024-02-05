using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buff
{
    public enum BuffType { Weakness, Poison, Stun, Thorns, Buffer, Breakdown, Levelup }
    public BuffType type;
    public int SkillPar; // 具体暴露的数据
    public int duration; // Buff持续的回合数
    public Buff(BuffType type, int SkillPar,int duration)
    {
        this.type = type;
        this.SkillPar = SkillPar;
        this.duration = duration;
    }

    // 每回合Buff效果的处理
    public void ApplyBuffEnemy(Enemy enemy)
    {
        switch (type)
        {
            case BuffType.Weakness://虚弱
                enemy.damage = (enemy.damage * 25) / 100;
                duration += SkillPar;
                break;
            case BuffType.Poison://中毒
                enemy.TakeDamage(SkillPar);//X层中毒
                SkillPar = Mathf.Max(0, SkillPar - 1); // 中毒伤害递减
                break;
            case BuffType.Stun://眩晕
                float rad= SkillPar / 100;
                if (Random.Range(0f, 1f) <rad) 
                {
                    enemy.IsStunned = true; 
                }
                    break; 
            case BuffType.Thorns://荆棘
                break;
            case BuffType.Breakdown://破甲
                break;
            case BuffType.Levelup:
                enemy.damage *= SkillPar; //苹果
                break;

        }
    }
    public void ApplyBuffPlayer(Player player)
    {
        switch (type)
        {
            case BuffType.Buffer://缓冲
                player.playerBuffer += SkillPar;
                player.IsBuffer = true;
                break;
        }
    }

    // 每回合更新Buff状态
    public bool UpdateBuff()
    {
        duration--;
        return duration <= 0; // 如果Buff结束返回true
    }
}
