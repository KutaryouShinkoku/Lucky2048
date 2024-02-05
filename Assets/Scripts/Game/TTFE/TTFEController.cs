using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TTFEController : MonoBehaviour
{
    public static TTFEController instance;
    public static bool isCubeMoving; //�����Ƿ񷽿鴦���ƶ�״̬
    public static int ticker; //������Ҫ��������һЩ����ָ��ı��ñ���

    [SerializeField] GameObject cube;
    [SerializeField] int maxSpawnAmount;
    public TTFEGrid[] allCells;
    public int maxMoveTime;
    public int moveTime;
    public bool isRoll;
    public bool isEnd;
    public AK.Wwise.Event MyEvent;

    [Header("Deck")]
    [SerializeField] Deck deckManager;


    //�㼶���£�
    //Grid�������ϵĸ��ӣ�-Cell�����������2048�飩-Cube���������ص�Cube��Ϣ��-Skill�����Cube��Ӧ�ļ��ܣ�
    //����һ��list����panel�ϴ��ڵ�cellλ��
    List<int> cellId;
    //�����list����panel��cube������
    public List<Cube> cubesInPanel;
    //���������ͳ�Ƴ��ִ����Ĺ��ܣ����������������鷳


    //------�ƶ�------
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
        Debug.Log($"���鳤�ȣ�{allCells.Length}");
        UpdateCubeInfo();
        isRoll = false;
    }
    public void Update()
    {
        //�ƶ�����
        if (!isCubeMoving)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                SlideLeft();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                SlideUp();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                SlideRight();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                SlideDown();
            }
        }

        //����һ��ת������

    }

    public void SlideLeft()
    {
        if (moveTime < maxMoveTime)
        {
            //��Ƶ���黬��
            ticker = 0;
            slide("left");
            UpdateCubeInfo();
            moveTime++;
            MyEvent.Post(gameObject);
        }
        else return;
    }
    public void SlideUp()
    {
        if (moveTime < maxMoveTime)
        {
            //��Ƶ���黬��
            ticker = 0;
            slide("up");
            UpdateCubeInfo();
            moveTime++;
            MyEvent.Post(gameObject);
        }
        else return;

    }
    public void SlideRight()
    {
        if (moveTime < maxMoveTime)
        {
            //��Ƶ���黬��
            ticker = 0;
            slide("right");
            UpdateCubeInfo();
            moveTime++;
            MyEvent.Post(gameObject);
        }
        else return;

    }
    public void SlideDown()
    {
        if (moveTime < maxMoveTime)
        {
            //��Ƶ���黬��
            ticker = 0;
            slide("down");
            UpdateCubeInfo();
            moveTime++;
            MyEvent.Post(gameObject);
        }
        else return;

    }
    public void InitializeTTFE()
    {
        if (!isRoll)
        {
            //�Ȱ�֮ǰ�������Ϣ���
            cubesInPanel = new List<Cube>();
            //��deck���ѡ���15������
            List<Cube> spawnPool = new List<Cube>();
            List<int> spawnId = new List<int>(); //��ʱlist����������������ɵķ���
            //��Ƶ���ϻ���
            for (int i = 0; i < Mathf.Min(deckManager.cubeDeck.Count, maxSpawnAmount, allCells.Length);)
            {
                int deckId = UnityEngine.Random.Range(0, deckManager.cubeDeck.Count);
                if (!spawnId.Contains(deckId))
                {
                    spawnId.Add(deckId);
                    Debug.Log($"��{deckId}�����鱻���˵�ѡ���ˣ�");
                    SpawnCubeRandom(deckId);
                    i++;
                }
            }
            isRoll = true;
        }
        isEnd = false;
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
                GameObject cell = Instantiate(cube, allCells[whichSpawn].transform);

                //��cube��Ϣ���µ�cell��
                TTFECubeCell cubeCellComp = cell.GetComponent<TTFECubeCell>();
                allCells[whichSpawn].GetComponent<TTFEGrid>().cell = cubeCellComp; //��һ��������Ϸ���cell�ӽ�grid
                cubeCellComp.cellUpdate(deckManager.cubeDeck[deckId]); //Ȼ�������Ǹ����cell����cube

                UpdateCubeInfo();
                isCubeSpawn = true;
                Debug.Log($"����һ��{deckManager.cubeDeck[deckId].Base.CubeKey}������{whichSpawn}��λ");
            }
            else
            {
                SpawnCubeRandom(deckId);
                return;
            }
        }


    }
    //����������cube����Ϣ
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
                Debug.Log($"���̸��£�{i}��λ��{allCells[i].cell.cube.Base.CubeKey}");
            }
        }
    }

    public void EndTurn()
    {
        if(isRoll==true)
        {
            isRoll = false;
            isEnd = true;
            moveTime = 0;
            Debug.Log($"�����غ�");
        }
    }

    //---------------------ѡ��------------------------





}
