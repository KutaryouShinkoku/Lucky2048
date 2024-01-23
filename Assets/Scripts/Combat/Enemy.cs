using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //敌人ai在这里写就行
    [SerializeField] int maxHP; //血量
    [SerializeField] List<Buff> buffs; //身上的Buff
    public int hp;//初始血量
    public int armor ;//初始护甲

    [Header("UI")]
    [SerializeField] Text txtEnemyHp;
    [SerializeField] Text txtNextAction; // 显示下一回合敌人的意图
    [SerializeField] Animator animator; // 动画组件的引用
    [SerializeField] Image enemyImage; // 敌人形象的UI元素引用
    [SerializeField] Sprite secondPhaseSprite; // 第二阶段的敌人形象

    public enum Action { Guard, HeavyHit, Roar, Charge, Overload }
    private Action currentAction;//当前下回合的行为
    private Action lastAction;//敌人已经做出的行为
    private bool isInSecondPhase = false;//敌人是否进入二阶段
    private bool hasOverloadedConsecutively = false;//敌人是否连续两次充能


    void Start()
    {
        hp = maxHP;
        armor = 0;
        currentAction = Action.Guard; // 第一回合始终是守护
        DecideNextAction();
    }

    
    void Update()
    {
        txtEnemyHp.text = $"{hp}/{maxHP}";
        CheckPhaseTransition();
        DecideNextAction();
        //UpdateNextActionUI();这里大概要加个敌人意图更新
    }

    void CheckPhaseTransition()
    {
        if (hp <= maxHP * 0.5 && !isInSecondPhase)
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
        Defense(5);// 守护逻辑，增加5点护甲
}
private void PerformHeavyHit()
    {
        // 重击逻辑，造成4点伤害
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
private void Attack() { }
private void Defense(int d) 
    {
        armor +=d;
    }
private void Enhance() { }
}
