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
        //����һ�����ϵ��ٶ�ʵ����Ծ
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //���ﵽ��ߵ�ʱ������ʱ��y�����Ϊ�����������Air״̬��ͨ��Air״̬����Ƿ���Ҫ����Idle״̬
        if(rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}
