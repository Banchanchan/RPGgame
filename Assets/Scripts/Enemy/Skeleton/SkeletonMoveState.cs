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
            //�������������Ȳ���Idle״̬һ��ʱ��֮���ڷ�ת�����õĻ���Э�̷���
            /*stateMachine.ChangeState(enemy.idleState);
            enemy.Flip();*/
            enemy.StartCoroutine("ChangeToIdleAndFlip", .25f);
        }
    }

}
