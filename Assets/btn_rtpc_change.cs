using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class btn_rtpc_evrnt : StateMachineBehaviour
{
    public AK.Wwise.RTPC Myrtpc; // 用于播放的Wwise事件名称
    public AK.Wwise.Event myEvent;

    // 当状态开始时播放Wwise事件
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        myEvent.Post(animator.gameObject);
        Myrtpc.SetGlobalValue(5);
    }

    // 移除了OnStateExit方法，因为不需要在状态结束时停止事件
}
