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
        //��ʱ�ģ�����deckmanager���������������ϵͳ�Ժ�������߼�ת�Ƶ������
        Instantiate(deckManager);
    }
    public void SpawnCube()
    {
        //��deck���ѡ���15������
        List<Cube> spawnPool = new List<Cube>();
        List<int> spawnId = new List<int>();
        for (int i = 0;i < Mathf.Min(deckManager.cubeDeck.Count,15);) 
        {
            int chance = Random.Range(0, deckManager.cubeDeck.Count);
            if(!spawnId.Contains (chance))
            {
                spawnId.Add(chance);
                Debug.Log($"��{chance}�����鱻���˵�ѡ���ˣ�");
                i++;
            }
        }
        //����Щ����������ڸ�����
        int whichSpawn = Random.Range(0, allCells.Length);
        Debug.Log(whichSpawn);
        Instantiate(cube, allCells[whichSpawn]);
    }
}
