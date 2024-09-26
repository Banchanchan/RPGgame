using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    [Header("Collision Info")]
    [SerializeField]
    protected Transform groundCheck;
    [SerializeField]
    protected float groundCheckDistance;
    [SerializeField]
    protected LayerMask whatIsGround;
    [SerializeField]
    protected Transform wallCheck;
    [SerializeField]
    protected float wallCheckDistance;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        //��ȡ���
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {

    }
    #region Collision
    //����Ƿ�Ϊ����ĺ���
    public virtual bool IsGroundedDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    //���Ƹ����ߵĺ���
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
    }
    #endregion

    #region Velocity
    public virtual void ZeroVelocity() => rb.velocity = new Vector2(0, 0);
    //���ø����ٶȵĺ���
    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        //�ı��泯��ĺ���
        FlipController(_xVelocity);
    }
    #endregion

    #region Flip
    //��ת����
    public virtual void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        //��ת����
        transform.Rotate(0, 180, 0);
    }

    //���Ʒ�ת�ĺ���
    protected virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }
    #endregion
}
