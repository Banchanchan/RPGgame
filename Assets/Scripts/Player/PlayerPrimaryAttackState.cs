using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    //�����л������Ĳ���
    private int comboCounter;
    //��󹥻���ʱ��
    private float lastTimeAttacked;
    //�������ʱ��
    private float comboWindow = 2;

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //������һ�£���ֹ�����ط����г�ͻ����ȻĿǰû�з��ִ��󣬿γ����д���
        xInput = 0;
        //������2ʱ���ã����ߵ�ʱ��Ƚϳ�ʱ���ã���Ϊ�����һ�ι���ʱ��̶�ʱ��Time.time���ڱ�󣬶��������lastTimeAttacked + comboWindow��ʾ���ʱ��Ƚϳ��ˣ�
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
        {
            //���ò���
            comboCounter = 0;
        }

        player.anim.SetInteger("ComboCounter", comboCounter);

        //�ý�ɫ�����������統��һ������ʱ���ڶ��ι��������������󹥻���������ת�泯����֮�乥���÷���ĵ��ˣ��������
        float attackDir = player.facingDir;
        if (xInput != 0)
        {
            attackDir = xInput;
        }

        //�ý�ɫվ�������ǹ���Ҳ����һС��λ��
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);

        //����ʱ�������һС��λ�ƣ���Ϊ��Update�У�����ʱ��С��0ʱ�����ٶ�ҲΪ�㣬
        //�����Ļ�0.1��Ϊ0����һС��ʱ������ƶ�
        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();

        //����һ��������ǣ�����ͣ�ĵ������������һֱ��ס�ƶ�ʱ������״̬���ǻ����ƶ���������Ϊ�����������֮����ȫ��ʱ������ƶ�״̬���˿첻��������
        //�����������Э�̷���������һ�����ڱ�ʾ�Ƿ�æµ�Ĳ�����æ�Ļ���һ��ʱ��֮��ͱ�Ϊ��æ
        //��ʱֻ�����ε�����С��0.1�ͱ�ʾæ��StartCoroutine������Э�̵ĺ���
        player.StartCoroutine("BusyFor", .15f);

        // �����仯
        comboCounter++;
        //��¼���һ�ι�����ʱ��
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        //stateTimer��PlayerState�ж��壬����stateTimer -= Time.deltaTime;
        //����һֱС���㣬���Ը�����ٶ�һֱ����ֵΪ��
        //��ˣ��ڹ���ʱ���������Updateʱ�������ٶ�Ϊ0���Ͳ������������Ч������������û��Ӱ�죨�������������ࣩ
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
