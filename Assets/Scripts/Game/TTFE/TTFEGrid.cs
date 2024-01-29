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

    //------�ƶ�ָ�ʺɽ���֣������ܾ��аɣ��Ż�������
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


    //���Ʒ���ļ���������û��װ�����ܻ�����⣬�ĵ�ʱ��ע���ĸ���Ҫ��
    //ԭ����ÿ��grid���Լ��ķ�������û�п飬�еĻ��Ͱ��Ǹ�������
    void SlideLeft(TTFEGrid currentGrid)
    {
        //����������ǿյģ��Ͳ�����
        if(currentGrid.right == null)return;
        //Debug.Log($"Left:{currentGrid.gameObject}");

        //������grid�ﱾ����п�
        if (currentGrid.cell != null)
        {
            TTFEGrid nextGrid = currentGrid.right;

            //ȷ������鷴������û��grid���Լ������������cell��û��grid�Ļ�next�����null
            while (nextGrid.right != null && nextGrid.cell == null)
            {
                nextGrid = nextGrid.right;
            }

            //��⵽cell����������Ǹ�cell�����Լ���ǰ��Ȼ���ж��ܲ��ܺϳ�
            if (nextGrid.cell != null)
            {
                //�������cell�е�cube��ȫ��ͬ
                if(currentGrid .cell.cube.Base.CubeKey == nextGrid.cell.cube.Base.CubeKey)
                {
                    //���û���ϼ�cube�����޷��ϳ�
                    if (currentGrid.cell.cube.Base.NextLevelCube.Base == null)
                    {
                        Debug.Log($"�����ˣ��ϲ��ˣ�");
                    }
                    //�ͼ����Ժϳɸ��߼�
                    else
                    {
                        nextGrid.cell.Combine();
                        Debug.Log($"λ��{currentGrid.name}�ϳ�{currentGrid.cell.cube.Base.name}��");
                        nextGrid.cell.transform.parent = currentGrid.transform;
                        currentGrid.cell = nextGrid.cell;
                        nextGrid.cell = null;
                    }
                }
                else if (currentGrid.right.cell != nextGrid.cell)
                {
                    Debug.Log($"�ϲ��ˣ�");
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
                Debug.Log($"����λ�����ƶ�1");
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
                        Debug.Log($"�����ˣ��ϲ��ˣ�");
                    }
                    else
                    {
                        Debug.Log($"�ϳɣ�");
                        nextGrid.cell.Combine();
                        nextGrid.cell.transform.parent = currentGrid.transform;
                        currentGrid.cell = nextGrid.cell;
                        nextGrid.cell = null;
                    }
                }
                else if(currentGrid.down.cell != nextGrid.cell)
                {
                    Debug.Log($"�ϲ��ˣ�");
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
                Debug.Log($"����λ�����ƶ�1");
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
                        Debug.Log($"�����ˣ��ϲ��ˣ�");
                    }
                    else
                    {
                        Debug.Log($"�ϳɣ�");
                        nextGrid.cell.Combine();
                        nextGrid.cell.transform.parent = currentGrid.transform;
                        currentGrid.cell = nextGrid.cell;
                        nextGrid.cell = null;
                    }
                }
                else if(currentGrid.left.cell != nextGrid.cell)
                {
                    Debug.Log($"�ϲ��ˣ�");
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
                Debug.Log($"����λ�����ƶ�1");
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
                        Debug.Log($"�����ˣ��ϲ��ˣ�");
                    }
                    else
                    {
                        Debug.Log($"�ϳɣ�");
                        nextGrid.cell.Combine();
                        nextGrid.cell.transform.parent = currentGrid.transform;
                        currentGrid.cell = nextGrid.cell;
                        nextGrid.cell = null;
                    }
                }
                else if(currentGrid.up.cell!=nextGrid.cell)
                {
                    Debug.Log($"�ϲ��ˣ�");
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
                Debug.Log($"����λ�����ƶ�1");
            }
        }

        if (currentGrid.up == null) return;
        SlideDown(currentGrid.up);
    }
}
