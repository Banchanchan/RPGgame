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
        //�ı�����ٶ�ʵ���ƶ�Ч��
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        //x�����Ϊ0��ʾͣ����������Idle״̬
        if (xInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
