using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TTFECubeCell : MonoBehaviour
{
    //这个函数管理cell的属性
    //grid:用来移动的2048载体
    //cell:载体里的内容，包含各种信息，包括代表的cube
    public int cellLevel;
    [SerializeField] Text valueDisplay; //显示在外面的数值，用2的次方形式
    [SerializeField] Image spriteDisplay;
    [SerializeField] float speed;
    public Cube cube;
    bool hasCombined;

    public void cellUpdate(Cube cubeIn)
    {
        cube = cubeIn;
        cellLevel = cubeIn.Base.Level + 1; //因为cube等级是从0开始数的，所以要+1
        valueDisplay.text = Mathf.Pow(2, cellLevel).ToString();
        spriteDisplay.sprite = cubeIn.Base.Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition != Vector3.zero)
        {
            //移动，子物体改变后，坐标不是原点，则移动到原点
            hasCombined = false;
            TTFEController.isCubeMoving = true;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, speed * Time.deltaTime);
        }
        else if(hasCombined == false)
        {
            //合成，如果一个格子出现了全新的物件，则删除它，并且标记
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

    //合成，写在cell里，本质上就是这个cell的cube替换成更高一级的
    public void Combine()
    {
        if (cube.Base.NextLevelCube.Base != null)
        {
            cube = cube.Base.NextLevelCube;
            cellUpdate(cube);
        }
    }
}
