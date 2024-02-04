using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 导入UI命名空间

public enum CombatState //各阶段
{
    none, //空阶段，备用
    select, //选方块
    preroll, //摇之前的阶段，检测马之类的
    roll, //摇老虎机
    combine, //2048
    end, //玩家回合结束，结算方块，先叠甲再攻击
    enemy, //敌人回合
}
public class CombatManager : MonoBehaviour
{
    //public Cube cube;
    public Player player;
    public Enemy enemy;
    private CombatState state;
    [Header("UI")]
    public CombatHUD combatHUD;
    public Text gameEndText; // 游戏结束时显示的文本
    [SerializeField] TTFEController ttfeController;

    [SerializeField] DeckPool deckPool; //全部的卡池以及权重
    [SerializeField] DeckBuilder deckBuilder1;
    [SerializeField] DeckBuilder deckBuilder2;
    [SerializeField] DeckBuilder deckBuilder3;
    public Queue<Cube> addedCube { get; set; } = new Queue<Cube>();
    //这个list则是临时用来管理选牌的
    public List<int> tempId;

    private int thornsBuffIntensity; // 荆棘 Buff 的强度
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
        //cube.Setup(this); // 将CombatManager的引用传递给Cube
    }
    public void Update()
    {
        //更新卡组
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


        //选方块
        if (state == CombatState.select)
        {
            GenerateDeckBuilder(deckPool.normalDeck);
        }
        //回合结束开始处理方块
        if(state == CombatState.end)
        {
            for(int i = 0; i < ttfeController.cubesInPanel.Count;i++)
            {
                ttfeController.cubesInPanel[i].Setup(this);
                //先处理稻穗的升级
                //然后依次处理方块的技能
                ttfeController.cubesInPanel[i].ResolveSkills();
            }
            //流程为给方块取目标（把技能目标附给方块）-结算方块技能（在cube脚本）-处理技能对目标的结果
            DeathCheck();
            state = CombatState.enemy;
        }

        //敌人回合
        if(state == CombatState.enemy)
        {
            enemy.ProcessBuffs();//处理敌人的Buff
            enemy.PerformAction();//然后处理敌人的行动
            DeathCheck();// 检查战斗是否结束
            state = CombatState.select; // 回合结束，切换到玩家选择方块的阶段
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
            enemy.TakeDamage(thornsBuffIntensity); // 对敌人造成荆棘伤害
        }
    }

    public void AddThornsBuff(int intensity)
    {
        thornsBuffIntensity += intensity; // 增加荆棘 Buff 强度
    }

    //游戏结束的判定
    public void DeathCheck() //检查玩家或者敌人是否有人死
    {
        if (player.playerHP==0)
        {
            // 玩家死亡，游戏失败
            GameEnd(false);
        }
        else if (enemy.enemyHP==0)
        {
            // 敌人死亡，游戏获胜
            GameEnd(true);
        }
    }
    public void GameEnd(bool playerWon)
    {
        state = CombatState.none; // 停止游戏状态更新
        gameEndText.gameObject.SetActive(true); // 显示游戏结束文本
        gameEndText.text = playerWon ? "游戏胜利！" : "游戏失败！"; // 根据玩家是否赢得游戏来更新文本
    }
    //-------------------------卡组相关------------------------
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
            Debug.Log($"从deckbuilder里抓{message.Base.CubeName}来用");
            addedCube.Enqueue(message);
        }
    }
}
