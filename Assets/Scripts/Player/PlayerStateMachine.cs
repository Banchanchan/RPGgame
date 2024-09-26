using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    //�ɶ�����д
    public PlayerState currentState {  get; private set; }

    //��ʼ��״̬
    public void Initialize(PlayerState _startState)
    {
        //��ֵ״̬
        currentState = _startState;
        //����״̬
        currentState.Enter();
    }

    //�ı�״̬
    public void ChangeState(PlayerState _newState)
    {
        //���˳���һ��״̬
        currentState.Exit();
        //�ٸ�ֵΪ��״̬
        currentState = _newState;
        //�����״̬
        currentState.Enter();
    }
}
