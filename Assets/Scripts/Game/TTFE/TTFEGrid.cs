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
    private void OnSlide(string whatWasSent)
    {
        //Debug.Log(whatWasSent);
        switch (whatWasSent)
        {
            case "left":
                {

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
                return;
            case "down":
                return;
        }
    }

    void SlideUp(TTFEGrid currentGrid)
    {
        if(currentGrid.down == null)
        {
            return;
        }
        Debug.Log($"up:{currentGrid.gameObject}");
        if (currentGrid.cell != null)
        {
            TTFEGrid nextGrid = currentGrid.down;
            while (nextGrid.down != null && nextGrid.cell == null)
            {
                nextGrid = nextGrid.down;
            }
            if (nextGrid.cell != null)
            {
                if(currentGrid .cell.cube.Base.CubeKey == nextGrid.cell.cube.Base.CubeKey)
                {
                    if (currentGrid.cell.cube.Base.Level >= 2)
                    {
                        Debug.Log($"满级了！合不了！");
                    }
                    else
                    {
                        Debug.Log($"合成！");
                        nextGrid.cell.transform.parent = currentGrid.transform;
                        currentGrid.cell = nextGrid.cell;
                        nextGrid.cell = null;
                    }
                }
                else
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
                Debug.Log($"朝空位移动");
            }
        }

        if (currentGrid.down == null) return;
        SlideUp(currentGrid.down);
    }

}
