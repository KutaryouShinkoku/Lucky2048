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
    public Cube cube;

    public void cellUpdate(Cube cubeIn)
    {
        cube = cubeIn;
        cellLevel = cubeIn.Base.Level + 1; //因为cube等级是从0开始数的，所以要+1
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
