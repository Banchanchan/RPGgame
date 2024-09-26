using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;

    private string animName;

    //计时器
    protected float stateTimer;
    //动画退出的控制条件
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
        //初始为false，表示动画不用退出
        triggerCalled = false;

        enemyBase.anim.SetBool(animName, true);
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animName, false);
    }
}
