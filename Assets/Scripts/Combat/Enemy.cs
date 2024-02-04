using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] Animator animator; // �������������
    [SerializeField] Image enemyImage; // ���������UIԪ������
    [SerializeField] Sprite secondPhaseSprite; // �ڶ��׶εĵ�������
    public int enemyHP;//��ʼѪ��
    public int enemyArmor;//��ʼ����
    public enum Action { Guard, HeavyHit, Roar, Charge, Overload }//���˵���Ϊ
    private Action currentAction;//��ǰ�»غϵ���Ϊ
    private Action lastAction;//�����Ѿ���������Ϊ
    public int addDefence { get; set; } // ��������Buff���µ��˺�����
    public int damage { get; set; } //���˵��˺�
    public int AttackCount { get; set; }//

    public AK.Wwise.Event bgm_switch;

    public bool IsStunned { get; set; } // �Ƿ���ѣ
    private bool isInSecondPhase = false;//�����Ƿ������׶�
    private bool hasOverloadedConsecutively = false;//�����Ƿ��������γ���


    void Start()
    {
        enemyHP = enemyMaxHP;
        enemyArmor = 0;
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
        if (IsStunned)
        {
            // �����ж��߼�
            return;
        }

        // �ж��߼�
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
            currentAction = Action.Charge; // ����ڶ��׶Σ���һ�غ�ʹ�ó���

            // ����ת���׶εĶ���
            animator.SetTrigger("PhaseTransition");

            // �ı��������
            enemyImage.sprite = secondPhaseSprite;

            bgm_switch.Post(gameObject);

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
                    currentAction = Random.Range(0f, 1f) < 0.40f ? Action.HeavyHit : Action.Roar;//40%�ػ�	60%����
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
        Defense(5);// �ػ��߼�������5�㻤��
    }
    private void PerformHeavyHit()
    {
        damage = 4;// �ػ��߼������4���˺�
    }

    private void PerformRoar()
    {
        // �����߼�����������1����������ߵ����˺�X�㣩2����ݣ���ߵ��λ���X�㣩
    }

    private void PerformCharge() 
    {
        Defense(15);//�����߼�����������2����������������15�㻤��
    }

    private void PerformOverload() 
    {
        //�����߼������1���˺����Ρ���������12�㻤��
    }
    private void Defense(int addDefence) 
    {
        enemyArmor += addDefence;
        enemyArmor = Mathf.Max(0, enemyArmor); // ȷ������ֵ���Ǹ���
    }
    private void Enhance() { }
    public void AddBuff(Buff newBuff)
    {
        buffs.Add(newBuff);
    }
    public void ProcessBuffs()
    {
        IsStunned = false;

        for (int i = buffs.Count - 1; i >= 0; i--)
        {
            buffs[i].ApplyBuff(this);
            if (buffs[i].UpdateBuff())
            {
                buffs.RemoveAt(i); // �Ƴ��Ѿ�������Buff
            }
        }
    }
    public void TakeDamage(int damage)
    {
        damage = Mathf.Max(0, damage - enemyArmor); // ���ǻ���
        enemyHP -= damage;
        // ������˺����߼�
    }

    private void Attack(Player target)
    {
        int attackDamage = damage;
        attackDamage = Mathf.Max(0, attackDamage); // ȷ���˺����Ǹ���
        target.TakeDamage(attackDamage);
    }

}
