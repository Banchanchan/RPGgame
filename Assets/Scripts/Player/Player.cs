using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Attack details")]
    public Vector2[] attackMovement;

    public bool isBusy {  get; private set; }

    [Header("Move info")]
    public float moveSpeed = 8;
    public float jumpForce = 12;

    [Header("Dash info")]
    [SerializeField]
    private float dashCooldown;
    private float dashUsageTimer;
    public float dashSpeed = 25;
    public float dashDuration = 0.2f;
    public float dashDir { get; private set; }

    #region States
    public PlayerStateMachine stateMachine {  get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }
    public PlayerDashState dashState { get; private set; }

    public PlayerPrimaryAttackState primaryAttack { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState  = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
    }

    protected override void Start()
    {
        base.Start();

        //初始化状态
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        //改变状态
        stateMachine.currentState.Update();
        CheckDashInput();
    }

    //协程概念，是一个协程（Coroutine），它的作用是让一个对象在指定的时间内处于“忙碌”状态。
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        //等到_seconds秒后，赋值为false
        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }

    //本来该功能是在PlayerGroundState脚本中的，移动到这里是为了实现跳跃时也能冲刺
    private void CheckDashInput()
    {
        if(IsWallDetected())
        {
            return;
        }

        //冲刺冷却计时器
        dashUsageTimer -= Time.deltaTime;

        //按下左Shift进入冲刺状态
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
        {
            //冲刺冷却时间
            dashUsageTimer = dashCooldown;
            //加个dashDir是为了解决当在攻击时，不用打断攻击也能向反方向冲刺
            dashDir = Input.GetAxisRaw("Horizontal");
            //如果为零则将面朝向赋值给冲刺朝向
            if(dashDir == 0)
            {
                dashDir = facingDir;
            }

            stateMachine.ChangeState(dashState);
        }
    }

    public virtual void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
}
