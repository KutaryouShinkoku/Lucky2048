using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ����UI�����ռ�

public enum CombatState //���׶�
{
    none, //�ս׶Σ�����
    select, //ѡ����
    preroll, //ҡ֮ǰ�Ľ׶Σ������֮���
    roll, //ҡ�ϻ���
    combine, //2048
    end, //��һغϽ��������㷽�飬�ȵ����ٹ���
    enemy, //���˻غ�
}
public class CombatManager : MonoBehaviour
{
    //public Cube cube;
    public Player player;
    public Enemy enemy;
    private CombatState state;
    [Header("UI")]
    public CombatHUD combatHUD;
    public Text gameEndText; // ��Ϸ����ʱ��ʾ���ı�
    [SerializeField] TTFEController ttfeController;

    [SerializeField] DeckPool deckPool; //ȫ���Ŀ����Լ�Ȩ��
    [SerializeField] DeckBuilder deckBuilder1;
    [SerializeField] DeckBuilder deckBuilder2;
    [SerializeField] DeckBuilder deckBuilder3;
    public Queue<Cube> addedCube { get; set; } = new Queue<Cube>();
    //���list������ʱ��������ѡ�Ƶ�
    public List<int> tempId;

    private int thornsBuffIntensity; // ���� Buff ��ǿ��
    public void Start()
    {
        GenerateDeckBuilder(deckPool.normalDeck);
        addedCube.Enqueue(deckBuilder1.cube);
        Debug.Log($"{addedCube}");
        Instantiate(player);
        Instantiate(enemy);
        InitializePlayerAndEnemy();
        player.playerArmor = 0;
        enemy.enemyArmor = 0;
        state = CombatState.none;
        //cube.Setup(this); // ��CombatManager�����ô��ݸ�Cube
    }
    public void Update()
    {
        //���¿���
        if (deckBuilder1.addedCube.Count != 0)
        {
            AddCube(deckBuilder1);
        }
        if (deckBuilder2.addedCube.Count != 0)
        {
            AddCube(deckBuilder2);
        }
        if (deckBuilder3.addedCube.Count != 0)
        {
            AddCube(deckBuilder3);
        }


        //ѡ����
        if (state == CombatState.select)
        {
            GenerateDeckBuilder(deckPool.normalDeck);
        }
        //�غϽ�����ʼ������
        if(state == CombatState.end)
        {
            for(int i = 0; i < ttfeController.cubesInPanel.Count;i++)
            {
                ttfeController.cubesInPanel[i].Setup(this);
                //�ȴ����������
                //Ȼ�����δ�����ļ���
                ttfeController.cubesInPanel[i].ResolveSkills();
            }
            //����Ϊ������ȡĿ�꣨�Ѽ���Ŀ�긽�����飩-���㷽�鼼�ܣ���cube�ű���-�����ܶ�Ŀ��Ľ��
            DeathCheck();
            state = CombatState.enemy;
        }

        //���˻غ�
        if(state == CombatState.enemy)
        {
            enemy.ProcessBuffs();//������˵�Buff
            enemy.PerformAction();//Ȼ������˵��ж�
            DeathCheck();// ���ս���Ƿ����
            state = CombatState.select; // �غϽ������л������ѡ�񷽿�Ľ׶�
        }
    }
    public void InitializePlayerAndEnemy()
    {

    }

    public void UpdateCombatStats()
    {

    }

    public void UpdateCombatHUD()
    {

    }

    private void HandleThornsEffect(int damage)
    {
        if (thornsBuffIntensity > 0)
        {
            enemy.TakeDamage(thornsBuffIntensity); // �Ե�����ɾ����˺�
        }
    }

    public void AddThornsBuff(int intensity)
    {
        thornsBuffIntensity += intensity; // ���Ӿ��� Buff ǿ��
    }

    //��Ϸ�������ж�
    public void DeathCheck() //�����һ��ߵ����Ƿ�������
    {
        if (player.playerHP==0)
        {
            // �����������Ϸʧ��
            GameEnd(false);
        }
        else if (enemy.enemyHP==0)
        {
            // ������������Ϸ��ʤ
            GameEnd(true);
        }
    }
    public void GameEnd(bool playerWon)
    {
        state = CombatState.none; // ֹͣ��Ϸ״̬����
        gameEndText.gameObject.SetActive(true); // ��ʾ��Ϸ�����ı�
        gameEndText.text = playerWon ? "��Ϸʤ����" : "��Ϸʧ�ܣ�"; // ��������Ƿ�Ӯ����Ϸ�������ı�
    }
    //-------------------------�������------------------------
    public void GenerateDeckBuilder(List<Cube> pool)
    {
        tempId = new List<int>();

        deckBuilder1.Init(pool[GenerateUniqueId(pool.Count)]);
        deckBuilder2.Init(pool[GenerateUniqueId(pool.Count)]);
        deckBuilder3.Init(pool[GenerateUniqueId(pool.Count)]);
    }
    public int GenerateUniqueId(int count)
    {
        int cubeId = UnityEngine.Random.Range(0, count);
        if (!tempId.Contains(cubeId))
        {
            tempId.Add(cubeId);
            return cubeId;
        }
        else GenerateUniqueId(count);
        return 0;
    }
    public void AddCube(DeckBuilder deckBuilder)
    {
        while (deckBuilder.addedCube.Count > 0)
        {
            var message = deckBuilder.addedCube.Dequeue();
            Debug.Log($"��deckbuilder��ץ{message.Base.CubeName}����");
            addedCube.Enqueue(message);
        }
    }
}
