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

    //�㼶���£�
    //Grid�������ϵĸ��ӣ�-Cell�����������2048�飩-Cube���������ص�Cube��Ϣ��-Skill�����Cube��Ӧ�ļ��ܣ�
    //����һ��list����panel�ϴ��ڵķ���
    List<int> cellId = new List<int>();

    //------�ƶ�------
    public static Action<string> slide;

    // Start is called before the first frame update
    void Start()
    {
        //��ʱ�ģ�����deckmanager���������������ϵͳ�Ժ�������߼�ת�Ƶ������
        Instantiate(deckManager);
        Debug.Log($"���鳤�ȣ�{allCells.Length}");
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
        //��deck���ѡ���15������
        List<Cube> spawnPool = new List<Cube>();
        List<int> spawnId = new List<int>(); //��ʱlist���������������ɵķ���
        for (int i = 0;i < Mathf.Min(deckManager.cubeDeck.Count, maxSpawnAmount,25);) 
        {
            int deckId = UnityEngine.Random.Range(0, deckManager.cubeDeck.Count);
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
            int whichSpawn = UnityEngine.Random.Range(0, allCells.Length);
            if (!cellId.Contains(whichSpawn))
            {
                //����һ������
                cellId.Add(whichSpawn);
                GameObject cell = Instantiate(cube, allCells[whichSpawn]);

                //��cube��Ϣ���µ�cell��
                TTFECubeCell cubeCellComp = cell.GetComponent<TTFECubeCell>();
                allCells[whichSpawn].GetComponent<TTFEGrid>().cell = cubeCellComp; //��һ��������Ϸ���cell�ӽ�grid
                cubeCellComp.cellUpdate(deckManager.cubeDeck[deckId]); //Ȼ�������Ǹ����cell����cube

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
