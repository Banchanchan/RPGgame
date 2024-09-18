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
        //��ȡ���
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //��ʼ��״̬
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        //�ı�״̬
        stateMachine.currentState.Update();
        CheckDashInput();

    }

    //�����ù�������PlayerGroundState�ű��еģ��ƶ���������Ϊ��ʵ����ԾʱҲ�ܳ��
    private void CheckDashInput()
    {
        //�����ȴ��ʱ��
        dashUsageTimer -= Time.deltaTime;

        //������Shift������״̬
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
        {
            //�����ȴʱ��
            dashUsageTimer = dashCooldown;
            //�Ӹ�dashDir��Ϊ�˽�����ڹ���ʱ�����ô�Ϲ���Ҳ���򷴷�����
            dashDir = Input.GetAxisRaw("Horizontal");
            //���Ϊ�����泯��ֵ����̳���
            if(dashDir == 0)
            {
                dashDir = facingDir;
            }

            stateMachine.ChangeState(dashState);
        }
    }

    //���ø����ٶȵĺ���
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        //�ı��泯��ĺ���
        FlipController(_xVelocity);
    }

    //����Ƿ�Ϊ����ĺ���
    public bool IsGroundedDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    //���Ƹ����ߵĺ���
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

    //��ת����
    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        //��ת����
        transform.Rotate(0, 180, 0);
    }

    //���Ʒ�ת�ĺ���
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
