using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] Image enemyImage; // 敌人形象的UI元素引用
    [SerializeField] Sprite secondPhaseSprite; // 第二阶段的敌人形象
    public Animator animator;
    public int enemyHP;//初始血量
    public int enemyArmor;//初始护甲
    public enum Action { Guard, HeavyHit, Roar, Charge, Overload }//敌人的行为
    private Action currentAction;//当前下回合的行为
    public int addDefence { get; set; } // 由于虚弱Buff导致的伤害减少
    public int damage { get; set; } //敌人受到的的伤害
    public int AttackCount { get; set; }//敌人伤害次数
    public int AttackSin { get; set; }//敌人单次伤害
    public int Strength { get; set; }//敌人力量
    public int Agility {  get; set; }//敌人敏捷
    public int Breakdown { get; set; }//敌人破甲
    public bool IsStunned = false; // 是否被晕眩
    private bool isInSecondPhase = false;//敌人是否进入二阶段
    private bool hasOverloadedConsecutively = false;//敌人是否连续两次充能
    private CombatManager combatManager;
    public Player player;
    //public void setup(CombatManager combatManager) 
    //{
    //    this.combatManager = combatManager;
    //}

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyHP = enemyMaxHP;
        enemyArmor = 0;
        Strength = 0;
        Agility = 0;
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
        

        // 行动逻辑
    }

    public void TestDmg()
    {
        TakeDamage(51);
    }
    void CheckPhaseTransition()
    {
        if (enemyHP <= enemyMaxHP * 0.5 && !isInSecondPhase)
        {
            Debug.Log($"InSecondPhase1");
            isInSecondPhase = true;
            currentAction = Action.Charge; // 进入第二阶段，第一回合使用充能
            animator.SetBool("InSecondPhase", true);
            // 播放转换阶段的动画
            //animator.SetTrigger("PhaseTransition");

            // 改变敌人形象
            //enemyImage.sprite = secondPhaseSprite;

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
    }

    public void PerformAction()
    {
        if (!IsStunned)
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
        }
    }

    private void PerformGuard()
    {
        Defense(5);// 守护逻辑，增加5点护甲
    }
    private void PerformHeavyHit()
    {
        AttackSin = 4 + Strength;// 重击逻辑，造成4点伤害
        AttackCount = 1;//一次
        Attack(player);

    }

    private void PerformRoar()
    {
        Strength += 1;
        Agility += 2;// 咆哮逻辑，给予自身1点力量（提高单次伤害X点）2点敏捷（提高单次护甲X点）
    }

    private void PerformCharge() 
    {
        Strength += 2;
        Defense(15);//充能逻辑，给予自身2点力量，给予自身15点护甲
    }

    private void PerformOverload() 
    {
        AttackSin = 1 + Strength;
        AttackCount = 2;
        Attack(combatManager.player);
        Defense(15);
        //超载逻辑，造成1点伤害两次、给予自身12点护甲
    }
    private void Defense(int addDefence) 
    {
        enemyArmor = Mathf.Clamp(enemyArmor+addDefence + Agility, 0, 99);// 确保护甲值不是负数
    }
    public void AddBuff(Buff newBuff)
    {
        buffs.Add(newBuff);
    }
    public void ProcessBuffs()
    {
        IsStunned = false;

        for (int i = buffs.Count - 1; i >= 0; i--)
        {
            buffs[i].ApplyBuffEnemy(this);
            if (buffs[i].UpdateBuff())
            {
                buffs.RemoveAt(i); // 移除已经结束的Buff
            }
        }
    }
    public void TakeDamage(int damage)
    {   
        damage = Mathf.Max(0, damage - enemyArmor); // 考虑护甲
        enemyArmor = Mathf.Clamp(enemyArmor - damage, 0, 99);
        enemyHP -= damage;
        // 添加受伤害的逻辑
    }

    public void Attack(Player target)
    {
        int attackDamage = AttackSin*AttackCount;
        attackDamage = Mathf.Max(0, attackDamage); // 确保伤害不是负数
        target.TakeDamage(attackDamage);
    }

}
