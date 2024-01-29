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
    [SerializeField] float speed;
    public Cube cube;
    bool hasCombined;

    public void cellUpdate(Cube cubeIn)
    {
        cube = cubeIn;
        cellLevel = cubeIn.Base.Level + 1; //��Ϊcube�ȼ��Ǵ�0��ʼ���ģ�����Ҫ+1
        valueDisplay.text = Mathf.Pow(2, cellLevel).ToString();
        spriteDisplay.sprite = cubeIn.Base.Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition != Vector3.zero)
        {
            //�ƶ���������ı�����겻��ԭ�㣬���ƶ���ԭ��
            hasCombined = false;
            TTFEController.isCubeMoving = true;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, speed * Time.deltaTime);
        }
        else if(hasCombined == false)
        {
            //�ϳɣ����һ�����ӳ�����ȫ�µ��������ɾ���������ұ��
            if(transform .parent.GetChild(0) != this.transform)
            {
                Destroy(transform.parent.GetChild(0).gameObject);
            }
            hasCombined = true;
        }
        else
        {
            TTFEController.isCubeMoving = false;
        }
    }

    //�ϳɣ�д��cell������Ͼ������cell��cube�滻�ɸ���һ����
    public void Combine()
    {
        if (cube.Base.NextLevelCube.Base != null)
        {
            cube = cube.Base.NextLevelCube;
            cellUpdate(cube);
        }
    }
}
