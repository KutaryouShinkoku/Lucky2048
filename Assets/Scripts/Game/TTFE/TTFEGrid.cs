using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTFEGrid : MonoBehaviour
{
    public TTFEGrid left;
    public TTFEGrid up;
    public TTFEGrid right;
    public TTFEGrid down;

    public TTFECubeCell cell;

    bool hasCombined;

    private void OnEnable()
    {
        TTFEController.slide += OnSlide;
    }

    private void OnDisable()
    {
        TTFEController.slide -= OnSlide;
    }

    //------移动指令，屎山部分，先能跑就行吧，优化不动了
    private void OnSlide(string whatWasSent)
    {
        //Debug.Log(whatWasSent);
        switch (whatWasSent)
        {
            case "left":
                {
                    if (left != null)
                    {
                        return;
                    }
                    TTFEGrid currentGrid = this;
                    SlideLeft(currentGrid);
                }
                return;
            case "up":
                {
                    if (up != null)
                    {
                        return;
                    }
                    TTFEGrid currentGrid = this;
                    SlideUp(currentGrid);
                }
                return;
            case "right":
                {
                    if (right != null)
                    {
                        return;
                    }
                    TTFEGrid currentGrid = this;
                    SlideRight(currentGrid);
                }
                return;
            case "down":
                {
                    if (down != null)
                    {
                        return;
                    }
                    TTFEGrid currentGrid = this;
                    SlideDown(currentGrid);
                }
                return;
        }
        TTFEController.ticker++;
    }


    //控制方向的几个函数，没封装，可能会出问题，改的时候注意四个都要改
    //原理是每个grid看自己的反方向有没有块，有的话就把那个块搬过来
    void SlideLeft(TTFEGrid currentGrid)
    {
        //最边上的不执行
        if (currentGrid.right == null) return;
        //Debug.Log($"Left:{currentGrid.gameObject}");

        //如果这个grid里本身就有块
        if (currentGrid.cell != null)
        {
            TTFEGrid nextGrid = currentGrid.right;

            //确定这个块反方向有没有grid，以及反方向最近的cell。没有grid的话next会输出null
            while (nextGrid.right != null && nextGrid.cell == null)
            {
                nextGrid = nextGrid.right;
            }

            //检测到cell的情况，把那个cell拉到自己跟前，然后判断能不能合成
            if (nextGrid.cell != null)
            {
                //如果两个cell中的cube完全相同
                if (currentGrid.cell.cube.Base.CubeKey == nextGrid.cell.cube.Base.CubeKey)
                {
                    //如果没有上级cube，则无法合成
                    if (currentGrid.cell.cube.Base.NextLevelCube.Base == null)
                    {
                        Debug.Log($"满级了！合不了！");
                    }
                    //低级可以合成更高级
                    else
                    {
                        Debug.Log($"位于{currentGrid.name}合成{currentGrid.cell.cube.Base.name}！");
                        nextGrid.cell.transform.parent = currentGrid.transform; //把上个格子的cell接进来
                        currentGrid.cell = nextGrid.cell; 
                        currentGrid.cell.Combine();
                        nextGrid.cell = null; //清空上个格子
                    }
                }
                //不同就合不了
                else if (currentGrid.right.cell != nextGrid.cell)
                {
                    Debug.Log($"不一样，合不了！");
                    //nextGrid.cell.transform.parent = currentGrid.right.transform;
                    //currentGrid.cell = nextGrid.cell;
                    //nextGrid.cell = null;
                }
            }
        }
        //如果grid里是空的，就直接把最近的块拉过来
        else
        {
            TTFEGrid nextGrid = currentGrid.right;
            while (nextGrid.right != null && nextGrid.cell == null)
            {
                nextGrid = nextGrid.right;
            }
            if (nextGrid.cell != null)
            {
                nextGrid.cell.transform.parent = currentGrid.transform;
                currentGrid.cell = nextGrid.cell;
                nextGrid.cell = null;
                SlideLeft(currentGrid);
                Debug.Log($"向左移动");
            }
        }

        if (currentGrid.right == null) return;
        SlideLeft(currentGrid.right);
    }
    void SlideUp(TTFEGrid currentGrid)
    {
        //如果反方向是空的，就不管了
        if (currentGrid.down == null) return;
        //Debug.Log($"Left:{currentGrid.gameObject}");

        //如果这个grid里本身就有块
        if (currentGrid.cell != null)
        {
            TTFEGrid nextGrid = currentGrid.down;

            //确定这个块反方向有没有grid，以及反方向最近的cell。没有grid的话next会输出null
            while (nextGrid.down != null && nextGrid.cell == null)
            {
                nextGrid = nextGrid.down;
            }

            //检测到cell的情况，把那个cell拉到自己跟前，然后判断能不能合成
            if (nextGrid.cell != null)
            {
                //如果两个cell中的cube完全相同
                if (currentGrid.cell.cube.Base.CubeKey == nextGrid.cell.cube.Base.CubeKey)
                {
                    //如果没有上级cube，则无法合成
                    if (currentGrid.cell.cube.Base.NextLevelCube.Base == null)
                    {
                        Debug.Log($"满级了！合不了！");
                    }
                    //低级可以合成更高级
                    else
                    {
                        Debug.Log($"位于{currentGrid.name}合成{currentGrid.cell.cube.Base.name}！");
                        nextGrid.cell.transform.parent = currentGrid.transform; //把上个格子的cell接进来
                        currentGrid.cell = nextGrid.cell;
                        currentGrid.cell.Combine();
                        nextGrid.cell = null; //清空上个格子
                    }
                }
                //不同就合不了
                else if (currentGrid.down.cell != nextGrid.cell)
                {
                    Debug.Log($"不一样，合不了！");
                    //nextGrid.cell.transform.parent = currentGrid.down.transform;
                    //currentGrid.cell = nextGrid.cell;
                    //nextGrid.cell = null;
                }
            }
        }
        //如果grid里是空的，就直接把最近的块拉过来
        else
        {
            TTFEGrid nextGrid = currentGrid.down;
            while (nextGrid.down != null && nextGrid.cell == null)
            {
                nextGrid = nextGrid.down;
            }
            if (nextGrid.cell != null)
            {
                nextGrid.cell.transform.parent = currentGrid.transform;
                currentGrid.cell = nextGrid.cell;
                nextGrid.cell = null;
                SlideUp(currentGrid);
                Debug.Log($"向上移动");
            }
        }

        if (currentGrid.down == null) return;
        SlideUp(currentGrid.down);
    }
    void SlideRight(TTFEGrid currentGrid)
    {
        //如果反方向是空的，就不管了
        if (currentGrid.left == null) return;
        //Debug.Log($"Left:{currentGrid.gameObject}");

        //如果这个grid里本身就有块
        if (currentGrid.cell != null)
        {
            TTFEGrid nextGrid = currentGrid.left;

            //确定这个块反方向有没有grid，以及反方向最近的cell。没有grid的话next会输出null
            while (nextGrid.left != null && nextGrid.cell == null)
            {
                nextGrid = nextGrid.left;
            }

            //检测到cell的情况，把那个cell拉到自己跟前，然后判断能不能合成
            if (nextGrid.cell != null)
            {
                //如果两个cell中的cube完全相同
                if (currentGrid.cell.cube.Base.CubeKey == nextGrid.cell.cube.Base.CubeKey)
                {
                    //如果没有上级cube，则无法合成
                    if (currentGrid.cell.cube.Base.NextLevelCube.Base == null)
                    {
                        Debug.Log($"满级了！合不了！");
                    }
                    //低级可以合成更高级
                    else
                    {
                        Debug.Log($"位于{currentGrid.name}合成{currentGrid.cell.cube.Base.name}！");
                        nextGrid.cell.transform.parent = currentGrid.transform; //把上个格子的cell接进来
                        currentGrid.cell = nextGrid.cell;
                        currentGrid.cell.Combine();
                        nextGrid.cell = null; //清空上个格子
                    }
                }
                //不同就合不了
                else if (currentGrid.left.cell != nextGrid.cell)
                {
                    Debug.Log($"不一样，合不了！");
                    //nextGrid.cell.transform.parent = currentGrid.left.transform;
                    //currentGrid.cell = nextGrid.cell;
                    //nextGrid.cell = null;
                }
            }
        }
        //如果grid里是空的，就直接把最近的块拉过来
        else
        {
            TTFEGrid nextGrid = currentGrid.left;
            while (nextGrid.left != null && nextGrid.cell == null)
            {
                nextGrid = nextGrid.left;
            }
            if (nextGrid.cell != null)
            {
                nextGrid.cell.transform.parent = currentGrid.transform;
                currentGrid.cell = nextGrid.cell;
                nextGrid.cell = null;
                SlideRight(currentGrid);
                Debug.Log($"向右移动");
            }
        }

        if (currentGrid.left == null) return;
        SlideRight(currentGrid.left);
    }
    void SlideDown(TTFEGrid currentGrid)
    {
        //如果反方向是空的，就不管了
        if (currentGrid.up == null) return;
        //Debug.Log($"Left:{currentGrid.gameObject}");

        //如果这个grid里本身就有块
        if (currentGrid.cell != null)
        {
            TTFEGrid nextGrid = currentGrid.up;

            //确定这个块反方向有没有grid，以及反方向最近的cell。没有grid的话next会输出null
            while (nextGrid.up != null && nextGrid.cell == null)
            {
                nextGrid = nextGrid.up;
            }

            //检测到cell的情况，把那个cell拉到自己跟前，然后判断能不能合成
            if (nextGrid.cell != null)
            {
                //如果两个cell中的cube完全相同
                if (currentGrid.cell.cube.Base.CubeKey == nextGrid.cell.cube.Base.CubeKey)
                {
                    //如果没有上级cube，则无法合成
                    if (currentGrid.cell.cube.Base.NextLevelCube.Base == null)
                    {
                        Debug.Log($"满级了！合不了！");
                    }
                    //低级可以合成更高级
                    else
                    {
                        Debug.Log($"位于{currentGrid.name}合成{currentGrid.cell.cube.Base.name}！");
                        nextGrid.cell.transform.parent = currentGrid.transform; //把上个格子的cell接进来
                        currentGrid.cell = nextGrid.cell;
                        currentGrid.cell.Combine();
                        nextGrid.cell = null; //清空上个格子
                    }
                }
                //不同就合不了
                else if (currentGrid.up.cell != nextGrid.cell)
                {
                    Debug.Log($"不一样，合不了！");
                    //nextGrid.cell.transform.parent = currentGrid.up.transform;
                    //currentGrid.cell = nextGrid.cell;
                    //nextGrid.cell = null;
                }
            }
        }
        //如果grid里是空的，就直接把最近的块拉过来
        else
        {
            TTFEGrid nextGrid = currentGrid.up;
            while (nextGrid.up != null && nextGrid.cell == null)
            {
                nextGrid = nextGrid.up;
            }
            if (nextGrid.cell != null)
            {
                nextGrid.cell.transform.parent = currentGrid.transform;
                currentGrid.cell = nextGrid.cell;
                nextGrid.cell = null;
                SlideDown(currentGrid);
                Debug.Log($"向下移动");
            }
        }

        if (currentGrid.up == null) return;
        SlideDown(currentGrid.up);
    }
}