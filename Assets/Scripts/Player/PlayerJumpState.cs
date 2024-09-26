using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //设置一个向上的速度实现跳跃
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //当达到最高点时，下落时（y方向变为负数）则进入Air状态，通过Air状态检测是否需要进入Idle状态
        if(rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}
