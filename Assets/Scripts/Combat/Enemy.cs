using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static CombatManager;

public class Enemy : MonoBehaviour
{
    //����ai������д����
    public int enemyMaxHP; //Ѫ��
    public int enemyMaxArmor; //����
    [SerializeField] private List<Buff> buffs; //���ϵ�Buff
    [Header("UI")]
    [SerializeField] Image enemyImage; // ���������UIԪ������
    [SerializeField] Sprite secondPhaseSprite; // �ڶ��׶εĵ�������
    public Animator animator;
    public int enemyHP;//��ʼѪ��
    public int enemyArmor;//��ʼ����
    public enum Action { Guard, HeavyHit, Roar, Charge, Overload }//���˵���Ϊ
    private Action currentAction;//��ǰ�»غϵ���Ϊ
    public int addDefence { get; set; } // ��������Buff���µ��˺�����
    public int damage { get; set; } //�����ܵ��ĵ��˺�
    public int AttackCount { get; set; }//�����˺�����
    public int AttackSin { get; set; }//���˵����˺�
    public int Strength { get; set; }//��������
    public int Agility {  get; set; }//�������
    public int Breakdown { get; set; }//�����Ƽ�
    public bool IsStunned = false; // �Ƿ���ѣ
    private bool isInSecondPhase = false;//�����Ƿ������׶�
    private bool hasOverloadedConsecutively = false;//�����Ƿ��������γ���
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
        currentAction = Action.Guard; // ��һ�غ�ʼ�����ػ�
        buffs = new List<Buff>();
        DecideNextAction();
    }

    
    void Update()
    {   
        CheckPhaseTransition();
        DecideNextAction();
        //UpdateNextActionUI();������Ҫ�Ӹ�������ͼ����
        ProcessBuffs();
        

        // �ж��߼�
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
            currentAction = Action.Charge; // ����ڶ��׶Σ���һ�غ�ʹ�ó���
            animator.SetBool("InSecondPhase", true);
            // ����ת���׶εĶ���
            //animator.SetTrigger("PhaseTransition");

            // �ı��������
            //enemyImage.sprite = secondPhaseSprite;

            // ��������������UI�����߼�
         
        }

    }

    void DecideNextAction()
    {
        if (!isInSecondPhase)
        {
            // ��һ�׶ε���Ϊ�߼�
            switch (currentAction)
            {
                case Action.Guard:
                    currentAction = Random.Range(0f, 1f) < 0.70f ? Action.HeavyHit : Action.Roar;//40%�ػ�	60%����
                    break;
                case Action.HeavyHit:
                    currentAction = Random.Range(0f, 1f) < 0.25f ? Action.Guard : Action.Roar;//25%�ػ�	75%����	
                    break;
                case Action.Roar:
                    currentAction = Random.Range(0f, 1f) < 0.45f ? Action.Guard : Action.HeavyHit;//45%�ػ�	55%�ػ�	
                    break;
            }
        }
        else// �ڶ��׶ε���Ϊ�߼�
        {   
            switch (currentAction)
            {
                case Action.Charge:
                    currentAction = Random.Range(0f, 1f) < 0.80f ? Action.HeavyHit : Action.Overload;//80%�ػ�	20%����	
                    break;
                case Action.HeavyHit:
                    currentAction = Random.Range(0f, 1f) < 0.55f ? Action.Charge : Action.Overload;//55%����  45%����	
                    break;
                case Action.Overload:
                    if (hasOverloadedConsecutively)//�ж��Ƿ���������
                    {
                        currentAction = Random.Range(0f, 1f) < 0.64f ? Action.Charge : Action.HeavyHit;//64%����  36%����
                        hasOverloadedConsecutively = false;
                    }
                    else
                    {
                        currentAction = Random.Range(0f, 1f) switch
                        {
                            < 0.30f => Action.HeavyHit,//30%�ػ�	
                            > 0.85f => Action.Overload,//25%����	
                            _ => Action.Charge,//45%����	
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
        Defense(5);// �ػ��߼�������5�㻤��
    }
    private void PerformHeavyHit()
    {
        AttackSin = 8 + Strength;// �ػ��߼������4���˺�
        AttackCount = 1;//һ��
        Attack(player);

    }

    private void PerformRoar()
    {
        Strength += 1;
        Agility += 1;// �����߼�����������1����������ߵ����˺�X�㣩2����ݣ���ߵ��λ���X�㣩
    }

    private void PerformCharge() 
    {
        Strength += 2;
        Defense(10);//�����߼�����������2����������������15�㻤��
    }

    private void PerformOverload() 
    {
        AttackSin = 0 + Strength;
        AttackCount = 2;
        Attack(combatManager.player);
        Defense(10);
        //�����߼������1���˺����Ρ���������12�㻤��
    }
    private void Defense(int addDefence) 
    {
        enemyArmor = Mathf.Clamp(enemyArmor+addDefence + Agility, 0, 99);// ȷ������ֵ���Ǹ���
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
                buffs.RemoveAt(i); // �Ƴ��Ѿ�������Buff
            }
        }
    }
    public void TakeDamage(int damage)
    {   
        damage = Mathf.Max(0, damage - enemyArmor); // ���ǻ���
        enemyArmor = Mathf.Clamp(enemyArmor - damage, 0, 99);
        enemyHP -= damage;


        // ������˺����߼�
    }

    public void Attack(Player target)
    {
        int attackDamage = AttackSin*AttackCount;
        attackDamage = Mathf.Max(0, attackDamage); // ȷ���˺����Ǹ���
        target.TakeDamage(attackDamage);
    }

}
