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
        //��ֹ�ߴ�����ʱ��Ϊ���Լ����ƶ�������
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        //��������⵽�����򲥷�Idle״̬
        if(player.IsGroundedDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
        //��⵽��ǽ�岥�Ż�ǽ����
        if(player.IsWallDetected())
        {
            stateMachine.ChangeState(player.wallSlide);
        }

        //����ǽ��ʱ����x�����ٶȱ�С
        if(xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.velocity.y);
        }
    }
}
