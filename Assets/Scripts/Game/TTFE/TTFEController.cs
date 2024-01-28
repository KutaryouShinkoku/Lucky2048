using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TTFEController : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] int maxSpawnAmount;
    public Transform[] allCells;
    [SerializeField] Deck deckManager;

    //层级如下：
    //Grid（棋盘上的格子）-Cell（格子里面的2048块）-Cube（这个块承载的Cube信息）-Skill（这个Cube对应的技能）
    //建立一个list管理panel上存在的方块
    List<int> cellId = new List<int>();

    //------移动------
    public static Action<string> slide;

    // Start is called before the first frame update
    void Start()
    {
        //临时的，生成deckmanager，后续加入了肉鸽系统以后把生成逻辑转移到肉鸽里
        Instantiate(deckManager);
        Debug.Log($"数组长度：{allCells.Length}");
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            slide("left");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            slide("up");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            slide("right");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            slide("down");
        }
    }
    public void InitializeTTFE()
    {
        //从deck里抽选最多15个方块
        List<Cube> spawnPool = new List<Cube>();
        List<int> spawnId = new List<int>(); //临时list，用来管理本批生成的方块
        for (int i = 0;i < Mathf.Min(deckManager.cubeDeck.Count, maxSpawnAmount,25);) 
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
                cellId.Add(whichSpawn);
                GameObject cell = Instantiate(cube, allCells[whichSpawn]);

                //把cube信息更新到cell中
                TTFECubeCell cubeCellComp = cell.GetComponent<TTFECubeCell>();
                allCells[whichSpawn].GetComponent<TTFEGrid>().cell = cubeCellComp; //这一步是在游戏里把cell扔进grid
                cubeCellComp.cellUpdate(deckManager.cubeDeck[deckId]); //然后这里是给这个cell分配cube

                isCubeSpawn = true;
                Debug.Log($"生成一个{deckManager.cubeDeck[deckId].Base.CubeKey}方块在{whichSpawn}号位");
            }
            else
            {
                SpawnCubeRandom(deckId);
                return;
            }
        }


        //以前的循环逻辑，先放着
        //while (cubeNum == cellId.Count&&cellId .Count<25)
        //{
        //    int whichSpawn = Random.Range(0, allCells.Length);
        //    if (!cellId.Contains(whichSpawn))
        //    {
        //        cellId.Add(whichSpawn);
        //        Instantiate(cube, allCells[whichSpawn]);
        //    }
        //}
    }
}
