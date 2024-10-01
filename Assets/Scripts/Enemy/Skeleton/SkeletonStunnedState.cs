using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunnedState : EnemyState
{
    private Enemy_Skeleton enemy;
    public SkeletonStunnedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.stunnedDuration;

        //����.1���0�ӳٵĵ���RedColorBlink�������ظ����ã��γ���˸Ч��
        enemy.fx.InvokeRepeating("RedColorBlink", 0, .1f);

        enemy.rb.velocity = new Vector2(enemy.stunnedDirection.x * -enemy.facingDir, enemy.stunnedDirection.y);
    }

    public override void Exit()
    {
        base.Exit();

        //�˳������˸Ч��
        enemy.fx.Invoke("CancelRedBlink", 0);
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
