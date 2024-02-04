using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buff
{
    public enum BuffType { Weakness, Poison, Stun, Thorns, Buffer, Breakdown, }
    public BuffType type;
    public int SkillPar; // ���屩¶������
    public int duration; // Buff�����Ļغ���

    public Buff(BuffType type, int SkillPar,int duration)
    {
        this.type = type;
        this.SkillPar = SkillPar;
        this.duration = duration;
    }

    // ÿ�غ�BuffЧ���Ĵ���
    public void ApplyBuffEnemy(Enemy enemy)
    {
        switch (type)
        {
            case BuffType.Weakness://����
                enemy.damage = (enemy.damage * 25) / 100;
                duration += SkillPar;
                break;
            case BuffType.Poison://�ж�
                enemy.TakeDamage(SkillPar);//X���ж�
                SkillPar = Mathf.Max(0, SkillPar - 1); // �ж��˺��ݼ�
                break;
            case BuffType.Stun://ѣ��
                enemy.IsStunned = true;
                break;
            case BuffType.Thorns://����
                // Thorns Buff���ܲ���Ҫ����������Ӧ��Ч��
                break;
            case BuffType.Breakdown://�Ƽ�
                break;

        }
    }
    public void ApplyBuffPlayer(Player player)
    {
        switch (type)
        {
            case BuffType.Buffer://����
                player.playerBuffer += SkillPar;
                player.IsBuffer = true;
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
