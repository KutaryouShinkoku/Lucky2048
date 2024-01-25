using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTFEController : MonoBehaviour
{
    [SerializeField] GameObject cube;
    public Transform[] allCells;
    [SerializeField] Deck deckManager;

    //建立一个list管理panel上存在的方块
    List<int> cellId = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        //临时的，生成deckmanager，后续加入了肉鸽系统以后把生成逻辑转移到肉鸽里
        Instantiate(deckManager);
        Debug.Log($"数组长度：{allCells.Length}");
    }
    public void Update()
    {
    }
    public void InitializeTTFE()
    {
        //从deck里抽选最多15个方块
        List<Cube> spawnPool = new List<Cube>();
        List<int> spawnId = new List<int>(); //临时list，用来管理本批生成的方块
        for (int i = 0;i < Mathf.Min(deckManager.cubeDeck.Count,15);) 
        {
            int chance = Random.Range(0, deckManager.cubeDeck.Count);
            if(!spawnId.Contains (chance))
            {
                spawnId.Add(chance);
                Debug.Log($"第{chance}个方块被幸运地选中了！");
                SpawnCubeRandom();
                i++;
            }
        }
    }

    //在随机位置生成一个方块
    public void SpawnCubeRandom() 
    {
        //List<int> cellId = new List<int>();
        //生成方块
        int cubeNum = cellId.Count;
        while (cubeNum == cellId.Count&&cellId .Count<25)
        {
            int whichSpawn = Random.Range(0, allCells.Length);
            if (!cellId.Contains(whichSpawn))
            {
                cellId.Add(whichSpawn);
                Instantiate(cube, allCells[whichSpawn]);
            }
        }
        if (cellId.Count >= 25)
        {
            Debug.Log($"方块满了！放不下了！");
        }
    }
}
