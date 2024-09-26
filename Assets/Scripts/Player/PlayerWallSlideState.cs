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
    }

    public override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJump);
            //��ִ��wallJumpʱ���Ͳ����������Щ�����ˣ�֮���˳�����Ȼ�ᷢ����ͻ��������ٶȻ��ͻ
            return;
        }

        //��������ʱ����Ҫ�뿪wallslide״̬����������ķ�����泯����һ��ʱ������ǽ�ķ����뿪��
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

        //Ŀǰ���ֵ��Ӵ�ǽ��һ˲�䰴�·��������䶯���ķ���Ҳ����
    }
}
