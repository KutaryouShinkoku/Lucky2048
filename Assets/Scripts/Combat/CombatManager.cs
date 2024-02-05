using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ����UI�����ռ�



public enum CombatState //���׶�
{
    none, //�ս׶Σ�����
    selectR, //ѡ����ϡ�ж�
    selectC, //ѡ����
    roll, //ҡ�ϻ���
    precombine, //ҡ֮ǰ�Ľ׶Σ������֮���
    combine, //2048
    end, //��һغϽ��������㷽�飬�ȵ����ٹ���
    enemy, //���˻غ�
    over, //ʧ��
    win, //ʤ��
}
public class CombatManager : MonoBehaviour
{
    //public Cube cube;
    public Player player;
    public Enemy enemy;
    public CombatState state;
    [Header("UI")]
    public CombatHUD combatHUD;
    public Text gameEndText; // ��Ϸ����ʱ��ʾ���ı�
    bool isCubeResolved;
    float cubeResolveTimer = 1.5f;

    public AK.Wwise.Event MyEvent1;
    public AK.Wwise.Event MyEvent2;

    [SerializeField] TTFEController ttfeController;

    [SerializeField] DeckPool deckPool; //ȫ���Ŀ����Լ�Ȩ��
    [SerializeField] DeckBuilder deckBuilder1;
    [SerializeField] DeckBuilder deckBuilder2;
    [SerializeField] DeckBuilder deckBuilder3;
    //ѡ��
    int normalCount;
    int rareCount;
    int epicCount;

    public Queue<Cube> addedCube { get; set; } = new Queue<Cube>();
    //���list������ʱ��������ѡ�Ƶ�
    public List<int> tempId;


    private int thornsBuffIntensity; // ���� Buff ��ǿ��
    public void Start()
    {
        ResetCount();
        //GenerateDeckBuilder(deckPool.normalDeck);
        isCubeResolved = false;
        player.playerArmor = 0;
        enemy.enemyArmor = 0;
        state = CombatState.selectR;
        //cube.Setup(this); // ��CombatManager�����ô��ݸ�Cube
    }
    public void Update()
    {
        //���¿���
        if (deckBuilder1.addedCube.Count != 0)
        {
            AddCube(deckBuilder1);
            RefreshPick();
        }
        if (deckBuilder2.addedCube.Count != 0)
        {
            AddCube(deckBuilder2);
            RefreshPick();
        }
        if (deckBuilder3.addedCube.Count != 0)
        {
            AddCube(deckBuilder3);
            RefreshPick();
        }
        //Debug.Log($"�׶Σ�{state}");

            if (state == CombatState.roll)
        {
            if (ttfeController.isRoll)
            {
                state = CombatState.precombine;
            }
        }

        if (state == CombatState.precombine)
        {
            state = CombatState.combine;
        }

        if(state == CombatState.combine)
        {
            if (ttfeController.isEnd)
            {
                state = CombatState.end;
            }
        }

        //�غϽ�����ʼ�������
        if(state == CombatState.end)
        {
            cubeResolveTimer -= Time.deltaTime;
            if (!isCubeResolved)
            {
                for (int i = 0; i < ttfeController.cubesInPanel.Count; i++)
                {
                    ttfeController.cubesInPanel[i].Setup(this);
                    //�ȴ�����������
                    //Ȼ�����δ������ļ���
                    ttfeController.cubesInPanel[i].ResolveSkills();
                }
                isCubeResolved = true;
                //����Ϊ������ȡĿ�꣨�Ѽ���Ŀ�긽�����飩-���㷽�鼼�ܣ���cube�ű���-������ܶ�Ŀ��Ľ��
                MyEvent1.Post(gameObject);
                
            }
            //DeathCheck();
            if (cubeResolveTimer < 0)
            {
                state = CombatState.enemy;
                isCubeResolved = false;
                cubeResolveTimer = 1.5f;
            }
        }

        //���˻غ�
        if(state == CombatState.enemy)
        {

            enemy.ProcessBuffs();//������˵�Buff
            enemy.PerformAction();//Ȼ������˵��ж�
            //DeathCheck();// ���ս���Ƿ����
            state = CombatState.selectR; // �غϽ������л������ѡ�񷽿�Ľ׶�
        }
        if (player.playerHP <= 0)
        {
            state = CombatState.over;
        }
        if (enemy.enemyHP <= 0)
        {
            state = CombatState.win;
        }
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
    //public void DeathCheck() //�����һ��ߵ����Ƿ�������
    //{
    //    if (player.playerHP==0)
    //    {
    //        // �����������Ϸʧ��
    //        GameEnd(false);
    //    }
    //    else if (enemy.enemyHP==0)
    //    {
    //        // ������������Ϸ��ʤ
    //        GameEnd(true);
    //    }
    //}
    //public void GameEnd(bool playerWon)
    //{
    //    state = CombatState.none; // ֹͣ��Ϸ״̬����
    //    gameEndText.gameObject.SetActive(true); // ��ʾ��Ϸ�����ı�
    //    gameEndText.text = playerWon ? "��Ϸʤ����" : "��Ϸʧ�ܣ�"; // ��������Ƿ�Ӯ����Ϸ�������ı�
    //}
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
        int cubeId = Random.Range(0, count);
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
    public void SelectRarityNormal()
    {
        GenerateDeckBuilder(deckPool.normalDeck);
        normalCount++;
        state = CombatState.selectC;
    }
    public void SelectRarityRare()
    {
        GenerateDeckBuilder(deckPool.rareDeck);
        rareCount++;
        state = CombatState.selectC;
    }
    public void SelectRarityEpic()
    {
        GenerateDeckBuilder(deckPool.epicDeck);
        epicCount++;
        state = CombatState.selectC;
    }
    public void RefreshPick()
    {
        if (normalCount > 0 && normalCount < 3)
        {
            SelectRarityNormal();
        }
        else if (rareCount > 0 && rareCount < 2)
        {
            SelectRarityRare();
        }
        else EndPick();
    }
    public void EndPick()
    {
        state = CombatState.roll;
        ResetCount();
    }
    public void ResetCount()
    {
        normalCount = 0;
        rareCount = 0;
        epicCount = 0;
    }
    public void XuanpaiTest()
    {
        state = CombatState.selectR;
        Debug.Log("ѡ��");
    }
    public void EndTurn()
    {
        if (ttfeController.isRoll == true)
        {
            state = CombatState.end;
        }
    }
}
