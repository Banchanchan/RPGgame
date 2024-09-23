using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    //控制切换攻击的参数
    private int comboCounter;
    //最后攻击的时间
    private float lastTimeAttacked;
    //连击间隔时间
    private float comboWindow = 2;

    public PlayerPrimaryAttack(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //当大于2时重置，或者当时间比较长时重置（因为当最后一次攻击时间固定时，Time.time还在变大，而当其大于lastTimeAttacked + comboWindow表示间隔时间比较长了）
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
        {
            //重置参数
            comboCounter = 0;
        }

        player.anim.SetInteger("ComboCounter", comboCounter);
    }

    public override void Exit()
    {
        base.Exit();
        // 参数变化
        comboCounter++;
        //记录最后一次攻击的时间
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
