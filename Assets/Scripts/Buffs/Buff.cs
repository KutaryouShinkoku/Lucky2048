using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buff
{
    public enum BuffType { Weakness, Poison, Stun }
    public BuffType type;
    public int intensity; // 虚弱减少的伤害，中毒的初始伤害
    public int duration; // Buff持续的回合数

    public Buff(BuffType type, int intensity, int duration)
    {
        this.type = type;
        this.intensity = intensity;
        this.duration = duration;
    }

    // 每回合Buff效果的处理
    public void ApplyBuff(Enemy enemy)
    {
        switch (type)
        {
            case BuffType.Weakness:
                enemy.DamageReduction += intensity;
                break;
            case BuffType.Poison:
                enemy.TakeDamage(intensity);
                intensity = Mathf.Max(0, intensity - 1); // 中毒伤害递减
                break;
            case BuffType.Stun:
                enemy.IsStunned = true;
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
