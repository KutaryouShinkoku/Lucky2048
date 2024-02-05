using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 引入UI命名空间

public class BuffUIManager : MonoBehaviour
{
    // 为每个buff定义一个公共变量，以便在Inspector中设置
    public GameObject ThornUI; // 荆棘buff图标
    public GameObject PoisonUI; // 中毒buff图标
    // 根据需要添加更多buff图标

    // 方法用于显示特定buff图标
    public void ShowBuff(string buffName)
    {
        switch (buffName)
        {
            case "Thorn":
                ThornUI.SetActive(true);
                break;
            case "Poison":
                Debug.Log($"进2");
                PoisonUI.SetActive(true);
                break;
            // 根据需要添加更多case
            default:
                break;
        }
    }

    // 方法用于隐藏特定buff图标
    public void HideBuff(string buffName)
    {
        switch (buffName)
        {
            case "Thorn":
                ThornUI.SetActive(false);
                break;
            case "Poison":
                PoisonUI.SetActive(false);
                break;
            // 根据需要添加更多case
            default:
                break;
        }
    }
}
