using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        //防止高处跳下时因为惯性继续移动的现象
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        //跳下来检测到地面则播放Idle状态
        if(player.IsGroundedDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
        //检测到是墙体播放滑墙动作
        if(player.IsWallDetected())
        {
            stateMachine.ChangeState(player.wallSlide);
        }

        //贴在墙上时让其x方向速度变小
        if(xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.velocity.y);
        }
    }
}
