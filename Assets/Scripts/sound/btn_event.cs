using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class btn_event : StateMachineBehaviour
{
    public AK.Wwise.Event eventName; // 用于播放的Wwise事件名称

    // 当状态开始时播放Wwise事件
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        eventName.Post(animator.gameObject);
    }

    // 移除了OnStateExit方法，因为不需要在状态结束时停止事件
}
