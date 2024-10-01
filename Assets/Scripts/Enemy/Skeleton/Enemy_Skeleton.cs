using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{

    #region States
    public SkeletonIdleState idleState {  get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletoBattleState battleState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonStunnedState stunnedState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
        moveState = new SkeletonMoveState(this, stateMachine, "Move", this);
        //��Move��������Ϊ������Ҫ�ƶ������λ�ã����Ծ�ʹ���������������Ӳ���
        battleState = new SkeletoBattleState(this, stateMachine, "Move", this);
        attackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
        stunnedState = new SkeletonStunnedState(this, stateMachine, "Stunned", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initiailze(idleState);
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.U))
            stateMachine.ChangeState(stunnedState);
    }

    //�ȴ�һ��ʱ��֮���ٷ�ת
    public IEnumerator ChangeToIdleAndFlip(float _seconds)
    {
        stateMachine.ChangeState(idleState);

        yield return new WaitForSeconds(_seconds);

        Flip();
    }

    public override bool CanBeStunned()
    {
        if(base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }

}
