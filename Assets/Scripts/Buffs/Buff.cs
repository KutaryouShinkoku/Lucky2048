using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buff
{
    public enum BuffType { Weakness, Poison, Stun }
    public BuffType type;
    public int intensity; // �������ٵ��˺����ж��ĳ�ʼ�˺�
    public int duration; // Buff�����Ļغ���

    public Buff(BuffType type, int intensity, int duration)
    {
        this.type = type;
        this.intensity = intensity;
        this.duration = duration;
    }

    // ÿ�غ�BuffЧ���Ĵ���
    public void ApplyBuff(Enemy enemy)
    {
        switch (type)
        {
            case BuffType.Weakness:
                enemy.DamageReduction += intensity;
                break;
            case BuffType.Poison:
                enemy.TakeDamage(intensity);
                intensity = Mathf.Max(0, intensity - 1); // �ж��˺��ݼ�
                break;
            case BuffType.Stun:
                enemy.IsStunned = true;
                break;
        }
    }

    // ÿ�غϸ���Buff״̬
    public bool UpdateBuff()
    {
        duration--;
        return duration <= 0; // ���Buff��������true
    }
}
