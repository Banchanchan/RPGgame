using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        //改变刚体速度实现移动效果
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        //x方向变为0表示停下来，进入Idle状态
        if (xInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
