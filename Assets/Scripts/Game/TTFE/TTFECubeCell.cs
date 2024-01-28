using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TTFECubeCell : MonoBehaviour
{
    //�����������cell������
    //grid:�����ƶ���2048����
    //cell:����������ݣ�����������Ϣ�����������cube
    public int cellLevel;
    [SerializeField] Text valueDisplay; //��ʾ���������ֵ����2�Ĵη���ʽ
    [SerializeField] Image spriteDisplay;
    public Cube cube;

    public void cellUpdate(Cube cubeIn)
    {
        cube = cubeIn;
        cellLevel = cubeIn.Base.Level + 1; //��Ϊcube�ȼ��Ǵ�0��ʼ���ģ�����Ҫ+1
        valueDisplay.text = Mathf.Pow(2, cellLevel).ToString();
        spriteDisplay.sprite = cubeIn.Base.Sprite;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
