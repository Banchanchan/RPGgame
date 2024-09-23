using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    //�����л������Ĳ���
    private int comboCounter;
    //��󹥻���ʱ��
    private float lastTimeAttacked;
    //�������ʱ��
    private float comboWindow = 2;

    public PlayerPrimaryAttack(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //������2ʱ���ã����ߵ�ʱ��Ƚϳ�ʱ���ã���Ϊ�����һ�ι���ʱ��̶�ʱ��Time.time���ڱ�󣬶��������lastTimeAttacked + comboWindow��ʾ���ʱ��Ƚϳ��ˣ�
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
        {
            //���ò���
            comboCounter = 0;
        }

        player.anim.SetInteger("ComboCounter", comboCounter);
    }

    public override void Exit()
    {
        base.Exit();
        // �����仯
        comboCounter++;
        //��¼���һ�ι�����ʱ��
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if(triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
