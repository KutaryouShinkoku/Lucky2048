using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int playerMaxHP; //血量
    public int playerHP;//初始血量
    public int playerArmor;//初始护甲
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        playerHP -= damage;
        playerHP = Mathf.Max(0, playerHP); // 确保血量不会变成负数
        // 可以在这里添加受伤动画或反应
    }
}
