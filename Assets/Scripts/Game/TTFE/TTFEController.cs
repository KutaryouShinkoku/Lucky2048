using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTFEController : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] int maxSpawnAmount;
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
        for (int i = 0;i < Mathf.Min(deckManager.cubeDeck.Count, maxSpawnAmount,25);) 
        {
            int deckId = Random.Range(0, deckManager.cubeDeck.Count);
            if(!spawnId.Contains (deckId))
            {
                spawnId.Add(deckId);
                Debug.Log($"��{deckId}�����鱻���˵�ѡ���ˣ�");
                SpawnCubeRandom(deckId);
                i++;
            }
        }
    }

    //�����λ������һ�����飬Ȼ�������cube
    public void SpawnCubeRandom(int deckId) 
    {
        //List<int> cellId = new List<int>();
        //���ɷ���
        Debug.Log($"----���ɷ���----");
        bool isCubeSpawn = false;
        //���˾ͷŲ����˵ı���˿
        if (cellId.Count >= 25)
        {
            Debug.Log($"�������ˣ��Ų����ˣ�");
            return;
        }
        if (!isCubeSpawn)
        {
            int whichSpawn = Random.Range(0, allCells.Length);
            if (!cellId.Contains(whichSpawn))
            {
                //����һ������
                cellId.Add(whichSpawn);
                GameObject cell = Instantiate(cube, allCells[whichSpawn]);

                //��cube��Ϣ���µ�cell��
                TTFECubeCell cubecell = cell.GetComponent<TTFECubeCell>();
                cubecell.cellUpdate(deckManager.cubeDeck[deckId]);

                isCubeSpawn = true;
                Debug.Log($"����һ��{deckManager.cubeDeck[deckId].Base.CubeKey}������{whichSpawn}��λ");
            }
            else
            {
                SpawnCubeRandom(deckId);
                return;
            }
        }


        //��ǰ��ѭ���߼����ȷ���
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
