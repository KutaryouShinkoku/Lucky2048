using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTFEController : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] Transform[] allCells;
    [SerializeField] Deck deckManager;

    // Start is called before the first frame update
    void Start()
    {
        //临时的，生成deckmanager，后续加入了肉鸽系统以后把生成逻辑转移到肉鸽里
        Instantiate(deckManager);
    }
    public void SpawnCube()
    {
        //从deck里抽选最多15个方块
        List<Cube> spawnPool = new List<Cube>();
        List<int> spawnId = new List<int>();
        for (int i = 0;i < Mathf.Min(deckManager.cubeDeck.Count,15);) 
        {
            int chance = Random.Range(0, deckManager.cubeDeck.Count);
            if(!spawnId.Contains (chance))
            {
                spawnId.Add(chance);
                Debug.Log($"第{chance}个方块被幸运地选中了！");
                i++;
            }
        }
        //把这些方块随机放在格子上
        int whichSpawn = Random.Range(0, allCells.Length);
        Debug.Log(whichSpawn);
        Instantiate(cube, allCells[whichSpawn]);
    }
}
