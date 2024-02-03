using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CombatManager;

public class Enemy : MonoBehaviour
{
    //敌人ai在这里写就行
    public int enemyMaxHP; //血量
    public int enemyMaxArmor; //护甲
    [SerializeField] private List<Buff> buffs; //身上的Buff
    [Header("UI")]
    [SerializeField] Animator animator; // 动画组件的引用
    [SerializeField] Image enemyImage; // 敌人形象的UI元素引用
    [SerializeField] Sprite secondPhaseSprite; // 第二阶段的敌人形象
    public int enemyHP;//初始血量
    public int enemyArmor;//初始护甲
    public enum Action { Guard, HeavyHit, Roar, Charge, Overload }//敌人的行为
    private Action currentAction;//当前下回合的行为
    private Action lastAction;//敌人已经做出的行为
    public int DamageReduction { get; set; } // 由于虚弱Buff导致的伤害减少
    public int addDefence { get; set; } // 由于虚弱Buff导致的伤害减少
    public int damage { get; set; } //敌人的伤害

    public bool IsStunned { get; set; } // 是否被晕眩
    private bool isInSecondPhase = false;//敌人是否进入二阶段
    private bool hasOverloadedConsecutively = false;//敌人是否连续两次充能


    void Start()
    {
        enemyHP = enemyMaxHP;
        enemyArmor = 0;
        currentAction = Action.Guard; // 第一回合始终是守护
        buffs = new List<Buff>();
        DecideNextAction();
    }

    
    void Update()
    {
        CheckPhaseTransition();
        DecideNextAction();
        //UpdateNextActionUI();这里大概要加个敌人意图更新
        ProcessBuffs();
        if (IsStunned)
        {
            // 跳过行动逻辑
            return;
        }

        // 行动逻辑
    }

    public void TestDmg()
    {
        TakeDamage(5);
    }
    void CheckPhaseTransition()
    {
        if (enemyHP <= enemyMaxHP * 0.5 && !isInSecondPhase)
        {
            isInSecondPhase = true;
            currentAction = Action.Charge; // 进入第二阶段，第一回合使用充能

            // 播放转换阶段的动画
            animator.SetTrigger("PhaseTransition");

            // 改变敌人形象
            enemyImage.sprite = secondPhaseSprite;

            // 这里可以添加其他UI更新逻辑
        }
    }

    void DecideNextAction()
    {
        if (!isInSecondPhase)
        {
            // 第一阶段的行为逻辑
            switch (currentAction)
            {
                case Action.Guard:
                    currentAction = Random.Range(0f, 1f) < 0.40f ? Action.HeavyHit : Action.Roar;//40%重击	60%咆哮
                    break;
                case Action.HeavyHit:
                    currentAction = Random.Range(0f, 1f) < 0.25f ? Action.Guard : Action.Roar;//25%守护	75%咆哮	
                    break;
                case Action.Roar:
                    currentAction = Random.Range(0f, 1f) < 0.45f ? Action.Guard : Action.HeavyHit;//45%守护	55%重击	
                    break;
            }
        }
        else// 第二阶段的行为逻辑
        {   
            switch (currentAction)
            {
                case Action.Charge:
                    currentAction = Random.Range(0f, 1f) < 0.80f ? Action.HeavyHit : Action.Overload;//80%重击	20%超载	
                    break;
                case Action.HeavyHit:
                    currentAction = Random.Range(0f, 1f) < 0.55f ? Action.Charge : Action.Overload;//55%充能  45%超载	
                    break;
                case Action.Overload:
                    if (hasOverloadedConsecutively)//判断是否连续充能
                    {
                        currentAction = Random.Range(0f, 1f) < 0.64f ? Action.Charge : Action.HeavyHit;//64%充能  36%超载
                        hasOverloadedConsecutively = false;
                    }
                    else
                    {
                        currentAction = Random.Range(0f, 1f) switch
                        {
                            < 0.30f => Action.HeavyHit,//30%重击	
                            > 0.85f => Action.Overload,//25%超载	
                            _ => Action.Charge,//45%充能	
                        };
                        if (currentAction == Action.Overload)
                        {
                            hasOverloadedConsecutively = true;
                        }
                    }
                    break;
            }
        }


        PerformAction();
    }

    public void PerformAction()
    {
        switch (currentAction)
        {
            case Action.Guard:
                PerformGuard();
                break;
            case Action.HeavyHit:
                PerformHeavyHit();
                break;
            case Action.Roar:
                PerformRoar();
                break;
            case Action.Charge:
                PerformCharge();
                break;
            case Action.Overload:
                PerformOverload();
                break;
        }

        lastAction = currentAction;
    }

    private void PerformGuard()
    {
        addDefence=5;// 守护逻辑，增加5点护甲
    }
    private void PerformHeavyHit()
    {
        damage = 4;// 重击逻辑，造成4点伤害
    }

    private void PerformRoar()
    {
        // 咆哮逻辑，给予自身1点力量（提高单次伤害X点）2点敏捷（提高单次护甲X点）
    }

    private void PerformCharge() 
    {
        //充能逻辑，给予自身2点力量，给予自身15点护甲
    }

    private void PerformOverload() 
    {
        //超载逻辑，造成1点伤害两次、给予自身12点护甲
    }
    private void Defense(int addDefence) 
    {
        enemyArmor += addDefence;
        enemyArmor = Mathf.Max(0, enemyArmor); // 确保护甲值不是负数
    }
    private void Enhance() { }
    public void AddBuff(Buff newBuff)
    {
        buffs.Add(newBuff);
    }
    private void ProcessBuffs()
    {
        DamageReduction = 0;
        IsStunned = false;

        for (int i = buffs.Count - 1; i >= 0; i--)
        {
            buffs[i].ApplyBuff(this);
            if (buffs[i].UpdateBuff())
            {
                buffs.RemoveAt(i); // 移除已经结束的Buff
            }
        }
    }
    public void TakeDamage(int damage)
    {
        damage = Mathf.Max(0, damage - enemyArmor - DamageReduction); // 考虑护甲和虚弱Buff
        enemyHP -= damage;
        // 添加受伤害的逻辑
    }

    private void Attack(Player target)
    {
        int attackDamage = damage - DamageReduction;
        attackDamage = Mathf.Max(0, attackDamage); // 确保伤害不是负数
        target.TakeDamage(attackDamage);
    }

}
