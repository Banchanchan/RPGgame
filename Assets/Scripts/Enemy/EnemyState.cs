using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;

    private string animName;

    //��ʱ��
    protected float stateTimer;
    //�����˳��Ŀ�������
    protected bool triggerCalled;

    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animName)
    {
        this.stateMachine = _stateMachine;
        this.enemyBase = _enemyBase;
        this.animName = _animName;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Enter()
    {
        //��ʼΪfalse����ʾ���������˳�
        triggerCalled = false;

        enemyBase.anim.SetBool(animName, true);
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animName, false);
    }
}
