using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.counterAttackDuration;

        //ȷ��successfulCounterAttackΪfalse
        player.anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();

        //��������ĵ�ΪattackCheck���뾶ΪattackCheckRadius��Բ�η�Χ�ڵ�������ײ��
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        //����������ײ��
        foreach (var hit in colliders)
        {
            //���Enemy����Ϊ��
            if (hit.GetComponent<Enemy>() != null)
            {
                //Enemy�Ƿ��ܱ����Σ����Ծͽ��뵯��״̬
                if (hit.GetComponent<Enemy>().CanBeStunned())
                {
                    //���¼�ʱ��
                    stateTimer = 10;
                    //���ŵ�������
                    player.anim.SetBool("SuccessfulCounterAttack", true);
                }
            }
        }

        //����ʧ�ܽ���idle״̬
        if(stateTimer < 0 || triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
