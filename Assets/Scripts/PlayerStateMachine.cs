using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    //可读不可写
    public PlayerState currentState {  get; private set; }

    //初始化状态
    public void Initialize(PlayerState _startState)
    {
        //赋值状态
        currentState = _startState;
        //进入状态
        currentState.Enter();
    }

    //改变状态
    public void ChangeState(PlayerState _newState)
    {
        //先退出上一个状态
        currentState.Exit();
        //再赋值为新状态
        currentState = _newState;
        //进入该状态
        currentState.Enter();
    }
}
