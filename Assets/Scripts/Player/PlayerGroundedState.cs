using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        //Ͷ���̽�,������һ�Ѷ̽�����������Ͷ��
        if(Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword())
            stateMachine.ChangeState(player.aimSword);

        //����
        if(Input.GetKeyDown(KeyCode.Q))
        {
            stateMachine.ChangeState(player.counterAttackState);
        }

        //���ι���
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.primaryAttack);
        }

        //������
        if (!player.IsGroundedDetected())
        {
           stateMachine.ChangeState(player.airState);
        }
       
        //���¿ո�����Ҽ�⵽�ǵ��������Ծ״̬
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundedDetected())
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }

    //�ж��Ƿ���ڽ�
    private bool HasNoSword()
    {
        if (!player.sword)
        {
            return true;
        }

        //���÷���ʱ���ò����ĺ���
        player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }
}
