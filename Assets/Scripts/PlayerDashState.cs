using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //将持续时间赋值给计时器
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        //退出时速度要变为0
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        //设置冲刺的速度
        player.SetVelocity(player.dashSpeed * player.dashDir, 0);

        //如果计时器小于0，则状态变为Idle状态
        if(stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
