using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletoBattleState : EnemyState
{
    private Transform player;
    private Enemy_Skeleton enemy;
    private int moveDir;
    public SkeletoBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        //player = GameObject.Find("Player").transform;
        //��ʹ��GameObject.Find()���Խ�ʡ����ʱ�Ŀ���
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;

            if(enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if(CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }
        }
        else
        {
            //��������ʱ����ߺ�player�ľ��������˳�����״̬����Ϊidle״̬
            if(stateTimer < 0 || Vector2.Distance(enemy.transform.position, player.transform.position) > 10)
            {
                stateMachine.ChangeState(enemy.idleState);
            }

            //�����ٶȵĴ����������else����ͻ��ǹ���һ���ƶ�һ�µ����󣬶����ǹ�����վ�Ų�������
            if (player.position.x > enemy.transform.position.x)
            {
                moveDir = 1;
            }
            else if (player.position.x < enemy.transform.position.x)
            {
                moveDir = -1;
            }

            enemy.SetVelocity(enemy.moveSpeed * moveDir, enemy.rb.velocity.y);
        }

        
    }

    private bool CanAttack()
    {
        if(Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
