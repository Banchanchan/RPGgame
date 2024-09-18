using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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

    [Header("Collision Info")]
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundCheckDistance;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private float wallCheckDistance;
    [SerializeField]
    private LayerMask whatIsWall;

    #region Components
    public Animator anim {  get; private set; }
    public Rigidbody2D rb { get; private set; }

    #endregion
    #region States
    public PlayerStateMachine stateMachine {  get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }

    #endregion

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;


    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState  = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
    }

    private void Start()
    {
        //获取组件
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //初始化状态
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        //改变状态
        stateMachine.currentState.Update();
        CheckDashInput();

    }

    //本来该功能是在PlayerGroundState脚本中的，移动到这里是为了实现跳跃时也能冲刺
    private void CheckDashInput()
    {
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

    //设置刚体速度的函数
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        //改变面朝向的函数
        FlipController(_xVelocity);
    }

    //检测是否为地面的函数
    public bool IsGroundedDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    //绘制辅助线的函数
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

    //翻转函数
    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        //翻转朝向
        transform.Rotate(0, 180, 0);
    }

    //控制翻转的函数
    public void FlipController(float _x)
    {
        if(_x > 0 && !facingRight)
        {
            Flip();
        }
        else if(_x < 0 && facingRight)
        {
            Flip();
        }
    }
}
