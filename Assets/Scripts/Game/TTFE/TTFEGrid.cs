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
        //如果反方向是空的，就不管了
        if(currentGrid.right == null)return;
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
                if(currentGrid .cell.cube.Base.CubeKey == nextGrid.cell.cube.Base.CubeKey)
                {
                    //如果没有上级cube，则无法合成
                    if (currentGrid.cell.cube.Base.NextLevelCube.Base == null)
                    {
                        Debug.Log($"满级了！合不了！");
                    }
                    //低级可以合成更高级
                    else
                    {
                        nextGrid.cell.Combine();
                        Debug.Log($"位于{currentGrid.name}合成{currentGrid.cell.cube.Base.name}！");
                        nextGrid.cell.transform.parent = currentGrid.transform;
                        currentGrid.cell = nextGrid.cell;
                        nextGrid.cell = null;
                    }
                }
                else if (currentGrid.right.cell != nextGrid.cell)
                {
                    Debug.Log($"合不了！");
                    nextGrid.cell.transform.parent = currentGrid.right.transform;
                    currentGrid.cell = nextGrid.cell;
                    nextGrid.cell = null;
                }
            }
        }
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
                Debug.Log($"朝空位向左移动1");
            }
        }

        if (currentGrid.right == null) return;
        SlideLeft(currentGrid.right);
    }
    void SlideUp(TTFEGrid currentGrid)
    {
        if (currentGrid.down == null)
        {
            return;
        }
        //Debug.Log($"Up:{currentGrid.gameObject}");
        if (currentGrid.cell != null)
        {
            TTFEGrid nextGrid = currentGrid.down;
            while (nextGrid.down != null && nextGrid.cell == null)
            {
                nextGrid = nextGrid.down;
            }
            if (nextGrid.cell != null)
            {
                if (currentGrid.cell.cube.Base.CubeKey == nextGrid.cell.cube.Base.CubeKey)
                {
                    if (currentGrid.cell.cube.Base.Level >= 2)
                    {
                        Debug.Log($"满级了！合不了！");
                    }
                    else
                    {
                        Debug.Log($"合成！");
                        nextGrid.cell.Combine();
                        nextGrid.cell.transform.parent = currentGrid.transform;
                        currentGrid.cell = nextGrid.cell;
                        nextGrid.cell = null;
                    }
                }
                else if(currentGrid.down.cell != nextGrid.cell)
                {
                    Debug.Log($"合不了！");
                    nextGrid.cell.transform.parent = currentGrid.down.transform;
                    currentGrid.cell = nextGrid.cell;
                    nextGrid.cell = null;
                }
            }
        }
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
                Debug.Log($"朝空位向上移动1");
            }
        }

        if (currentGrid.down == null) return;
        SlideUp(currentGrid.down);
    }
    void SlideRight(TTFEGrid currentGrid)
    {
        if (currentGrid.left == null)
        {
            return;
        }
        //Debug.Log($"Right:{currentGrid.gameObject}");
        if (currentGrid.cell != null)
        {
            TTFEGrid nextGrid = currentGrid.left;
            while (nextGrid.left != null && nextGrid.cell == null)
            {
                nextGrid = nextGrid.left;
            }
            if (nextGrid.cell != null)
            {
                if (currentGrid.cell.cube.Base.CubeKey == nextGrid.cell.cube.Base.CubeKey)
                {
                    if (currentGrid.cell.cube.Base.Level >= 2)
                    {
                        Debug.Log($"满级了！合不了！");
                    }
                    else
                    {
                        Debug.Log($"合成！");
                        nextGrid.cell.Combine();
                        nextGrid.cell.transform.parent = currentGrid.transform;
                        currentGrid.cell = nextGrid.cell;
                        nextGrid.cell = null;
                    }
                }
                else if(currentGrid.left.cell != nextGrid.cell)
                {
                    Debug.Log($"合不了！");
                    nextGrid.cell.transform.parent = currentGrid.left.transform;
                    currentGrid.cell = nextGrid.cell;
                    nextGrid.cell = null;
                }
            }
        }
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
                Debug.Log($"朝空位向右移动1");
            }
        }

        if (currentGrid.left == null) return;
        SlideRight(currentGrid.left);
    }
    void SlideDown(TTFEGrid currentGrid)
    {
        if (currentGrid.up == null)
        {
            return;
        }
        //Debug.Log($"Down:{currentGrid.gameObject}");
        if (currentGrid.cell != null)
        {
            TTFEGrid nextGrid = currentGrid.up;
            while (nextGrid.up != null && nextGrid.cell == null)
            {
                nextGrid = nextGrid.up;
            }
            if (nextGrid.cell != null)
            {
                if (currentGrid.cell.cube.Base.CubeKey == nextGrid.cell.cube.Base.CubeKey)
                {
                    if (currentGrid.cell.cube.Base.Level >= 2)
                    {
                        Debug.Log($"满级了！合不了！");
                    }
                    else
                    {
                        Debug.Log($"合成！");
                        nextGrid.cell.Combine();
                        nextGrid.cell.transform.parent = currentGrid.transform;
                        currentGrid.cell = nextGrid.cell;
                        nextGrid.cell = null;
                    }
                }
                else if(currentGrid.up.cell!=nextGrid.cell)
                {
                    Debug.Log($"合不了！");
                    nextGrid.cell.transform.parent = currentGrid.up.transform;
                    currentGrid.cell = nextGrid.cell;
                    nextGrid.cell = null;
                }
            }
        }
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
                Debug.Log($"朝空位向下移动1");
            }
        }

        if (currentGrid.up == null) return;
        SlideDown(currentGrid.up);
    }
}
