using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    //刚体
    protected Rigidbody2D rb;
    //x轴输入
    protected float xInput;
    protected float yInput;
    private string animBoolName;

    //计时器
    protected float stateTimer;
    protected bool triggerCalled;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        //进入该函数则播放对应动画
        player.anim.SetBool(animBoolName , true);
        rb = player.rb;

        triggerCalled = false;
    }

    public virtual void Update()
    {
        //计时器变化
        stateTimer -= Time.deltaTime;
        //通过水平轴得到输入
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        //设置跳跃动画的上跳和下落的转换
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void Exit()
    {
        //退出则停止播放动画
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
