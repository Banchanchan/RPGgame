using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundedState
{
    public SkeletonMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, enemy.rb.velocity.y);

        if(enemy.IsWallDetected() || !enemy.IsGroundedDetected())
        {
            //这里我想让其先播放Idle状态一段时间之后在翻转，采用的还是协程方法
            /*stateMachine.ChangeState(enemy.idleState);
            enemy.Flip();*/
            enemy.StartCoroutine("ChangeToIdleAndFlip", .25f);
        }
    }

}
