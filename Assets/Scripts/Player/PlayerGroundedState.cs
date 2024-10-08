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

        //投掷短剑,仅能有一把短剑，不能无限投掷
        if(Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword())
            stateMachine.ChangeState(player.aimSword);

        //弹反
        if(Input.GetKeyDown(KeyCode.Q))
        {
            stateMachine.ChangeState(player.counterAttackState);
        }

        //三段攻击
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.primaryAttack);
        }

        //地面检测
        if (!player.IsGroundedDetected())
        {
           stateMachine.ChangeState(player.airState);
        }
       
        //按下空格键并且检测到是地面进入跳跃状态
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundedDetected())
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }

    //判断是否存在剑
    private bool HasNoSword()
    {
        if (!player.sword)
        {
            return true;
        }

        //调用返回时设置参数的函数
        player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }
}
