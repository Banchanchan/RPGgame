using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    //����
    protected Rigidbody2D rb;
    //x������
    protected float xInput;
    protected float yInput;
    private string animBoolName;

    //��ʱ��
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
        //����ú����򲥷Ŷ�Ӧ����
        player.anim.SetBool(animBoolName , true);
        rb = player.rb;

        triggerCalled = false;
    }

    public virtual void Update()
    {
        //��ʱ���仯
        stateTimer -= Time.deltaTime;
        //ͨ��ˮƽ��õ�����
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        //������Ծ�����������������ת��
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void Exit()
    {
        //�˳���ֹͣ���Ŷ���
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
