using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{

    #region States
    public SkeletonIdleState idleState {  get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
        moveState = new SkeletonMoveState(this, stateMachine, "Move", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initiailze(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    //等待一段时间之后再翻转
    public IEnumerator ChangeToIdleAndFlip(float _seconds)
    {
        stateMachine.ChangeState(idleState);

        yield return new WaitForSeconds(_seconds);

        Flip();
    }
}
