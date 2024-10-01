using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.counterAttackDuration;

        //确保successfulCounterAttack为false
        player.anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();

        //检测在中心的为attackCheck，半径为attackCheckRadius的圆形范围内的所有碰撞体
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        //遍历所有碰撞体
        foreach (var hit in colliders)
        {
            //如果Enemy对象不为空
            if (hit.GetComponent<Enemy>() != null)
            {
                //Enemy是否能被击晕，可以就进入弹反状态
                if (hit.GetComponent<Enemy>().CanBeStunned())
                {
                    //更新计时器
                    stateTimer = 10;
                    //播放弹反动画
                    player.anim.SetBool("SuccessfulCounterAttack", true);
                }
            }
        }

        //弹反失败进入idle状态
        if(stateTimer < 0 || triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
