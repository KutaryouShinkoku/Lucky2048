using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TTFEController : MonoBehaviour
{
    public static TTFEController instance;
    public static bool isCubeMoving; //管理是否方块处于移动状态
    public static int ticker; //可能需要用来接收一些动作指令的备用变量

    [SerializeField] GameObject cube;
    [SerializeField] int maxSpawnAmount;
    public TTFEGrid[] allCells;

    [Header("Deck")]
    [SerializeField] Deck deckManager;





    //层级如下：
    //Grid（棋盘上的格子）-Cell（格子里面的2048块）-Cube（这个块承载的Cube信息）-Skill（这个Cube对应的技能）
    //建立一个list管理panel上存在的cell位置
    List<int> cellId;
    //而这个list管理panel上cube的种类
    public List<Cube> cubesInPanel;
    //这里放用来统计出现次数的功能，啊啊啊啊啊啊好麻烦


    //------移动------
    public static Action<string> slide;

    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        //临时的，生成deckmanager，后续加入了肉鸽系统以后把生成逻辑转移到肉鸽里
        //Instantiate(deckManager);
        Debug.Log($"数组长度：{allCells.Length}");
        UpdateCubeInfo();
    }
    public void Update()
    {
        //移动输入
        if (!isCubeMoving)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                //音频：块滑动
                ticker = 0;
                slide("left");
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                //音频：块滑动
                ticker = 0;
                slide("up");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                //音频：块滑动
                ticker = 0;
                slide("right");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                //音频：块滑动
                ticker = 0;
                slide("down");
            }
        }



    }

    public void InitializeTTFE()
    {
        //从deck里抽选最多15个方块
        List<Cube> spawnPool = new List<Cube>();
        List<int> spawnId = new List<int>(); //临时list，用来管理本批生成的方块
        //音频：老虎机
        for (int i = 0;i < Mathf.Min(deckManager.cubeDeck.Count, maxSpawnAmount, allCells.Length);) 
        {
            int deckId = UnityEngine.Random.Range(0, deckManager.cubeDeck.Count);
            if(!spawnId.Contains (deckId))
            {
                spawnId.Add(deckId);
                Debug.Log($"第{deckId}个方块被幸运地选中了！");
                SpawnCubeRandom(deckId);
                i++;
            }
        }
    }

    //在随机位置生成一个方块，然后给他绑定cube
    public void SpawnCubeRandom(int deckId) 
    {
        //List<int> cellId = new List<int>();
        //生成方块
        Debug.Log($"----生成方块----");
        bool isCubeSpawn = false;
        //满了就放不下了的保险丝
        if (cellId.Count >= 25)
        {
            Debug.Log($"方块满了！放不下了！");
            return;
        }
        if (!isCubeSpawn)
        {
            int whichSpawn = UnityEngine.Random.Range(0, allCells.Length);
            if (!cellId.Contains(whichSpawn))
            {
                //生成一个方块
                GameObject cell = Instantiate(cube, allCells[whichSpawn].transform);

                //把cube信息更新到cell中
                TTFECubeCell cubeCellComp = cell.GetComponent<TTFECubeCell>();
                allCells[whichSpawn].GetComponent<TTFEGrid>().cell = cubeCellComp; //这一步是在游戏里把cell扔进grid
                cubeCellComp.cellUpdate(deckManager.cubeDeck[deckId]); //然后这里是给这个cell分配cube

                UpdateCubeInfo();
                isCubeSpawn = true;
                Debug.Log($"生成一个{deckManager.cubeDeck[deckId].Base.CubeKey}方块在{whichSpawn}号位");
            }
            else
            {
                SpawnCubeRandom(deckId);
                return;
            }
        }


    }
    //更新棋盘上cube的信息
    public void UpdateCubeInfo()
    {
        cellId = new List<int>();
        cubesInPanel = new List<Cube>();
        for (int i = 0; i < allCells.Length; i++)
        {
            if (allCells[i].cell != null)
            {
                cellId.Add(i);
                cubesInPanel.Add(allCells[i].cell.cube);
                Debug.Log($"棋盘更新：{i}号位的{allCells[i].cell.cube.Base.CubeKey}");
            }
        }
    }
    //---------------------选牌------------------------





}
