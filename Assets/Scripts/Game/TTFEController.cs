using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTFEController : MonoBehaviour
{
    [SerializeField] GameObject cube;
    public Transform[] allCells;
    [SerializeField] Deck deckManager;

    //����һ��list����panel�ϴ��ڵķ���
    List<int> cellId = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        //��ʱ�ģ�����deckmanager���������������ϵͳ�Ժ�������߼�ת�Ƶ������
        Instantiate(deckManager);
        Debug.Log($"���鳤�ȣ�{allCells.Length}");
    }
    public void Update()
    {
    }
    public void InitializeTTFE()
    {
        //��deck���ѡ���15������
        List<Cube> spawnPool = new List<Cube>();
        List<int> spawnId = new List<int>(); //��ʱlist���������������ɵķ���
        for (int i = 0;i < Mathf.Min(deckManager.cubeDeck.Count,15);) 
        {
            int chance = Random.Range(0, deckManager.cubeDeck.Count);
            if(!spawnId.Contains (chance))
            {
                spawnId.Add(chance);
                Debug.Log($"��{chance}�����鱻���˵�ѡ���ˣ�");
                SpawnCubeRandom();
                i++;
            }
        }
    }

    //�����λ������һ������
    public void SpawnCubeRandom() 
    {
        //List<int> cellId = new List<int>();
        //���ɷ���
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
            Debug.Log($"�������ˣ��Ų����ˣ�");
        }
    }
}
