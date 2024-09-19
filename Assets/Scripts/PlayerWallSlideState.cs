using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        //防止惯性滑动
        player.SetVelocity(0,rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        //当有输入时（想要离开wallslide状态）并且输入的方向和面朝方向不一致时（向不是墙的方向离开）
        if(xInput != 0 && xInput != player.facingDir)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if(yInput < 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y * 0.7f);
        }
        
        if (player.IsGroundedDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }

        //目前发现当接触墙的一瞬间按下反方向则其动画的方向也反了
    }
}
