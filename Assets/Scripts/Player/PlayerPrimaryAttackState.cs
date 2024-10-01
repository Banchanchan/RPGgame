using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    //控制切换攻击的参数
    private int comboCounter;
    //最后攻击的时间
    private float lastTimeAttacked;
    //连击间隔时间
    private float comboWindow = 2;

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //先清理一下，防止其他地方会有冲突，当然目前没有发现错误，课程上有错误
        xInput = 0;
        //当大于2时重置，或者当时间比较长时重置（因为当最后一次攻击时间固定时，Time.time还在变大，而当其大于lastTimeAttacked + comboWindow表示间隔时间比较长了）
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
        {
            //重置参数
            comboCounter = 0;
        }

        player.anim.SetInteger("ComboCounter", comboCounter);

        //让角色攻击更灵活，比如当第一段向右时，第二段攻击可以马上向左攻击，不用先转面朝向，是之间攻击该方向的敌人，更灵活了
        float attackDir = player.facingDir;
        if (xInput != 0)
        {
            attackDir = xInput;
        }

        //让角色站立不动是攻击也会有一小段位移
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);

        //攻击时让其产生一小段位移，因为在Update中，当计时器小于0时刚体速度也为零，
        //这样的话0.1变为0还有一小段时间可以移动
        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();

        //还有一个问题就是，当不停的点击鼠标左键并且一直按住移动时，攻击状态还是会有移动，这是因为在两次鼠标点击之间完全够时间进入移动状态（人快不过机器）
        //解决方法是用协程方法，设置一个用于表示是否忙碌的参数，忙的话，一定时间之后就变为不忙
        //此时只有两次点击间隔小于0.1就表示忙，StartCoroutine是启动协程的函数
        player.StartCoroutine("BusyFor", .15f);

        // 参数变化
        comboCounter++;
        //记录最后一次攻击的时间
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        //stateTimer在PlayerState中定义，并且stateTimer -= Time.deltaTime;
        //所以一直小于零，所以刚体的速度一直都赋值为零
        //如此，在攻击时，进入这个Update时，刚体速度为0，就不会产生滑步的效果，不攻击则没有影响（都不会进入这个类）
        if (stateTimer < 0)
        {
            player.SetZeroVelocity();
        }

        if(triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
