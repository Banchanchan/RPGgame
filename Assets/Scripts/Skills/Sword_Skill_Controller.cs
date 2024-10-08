using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill_Controller : MonoBehaviour
{
    [SerializeField] private float returnSpeed = 12;
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;

    private bool canRotate = true;
    private bool isReturning;

    //�����������⣬������Щ������������Ҫ����Awake�У�����Start�л������⣨�޷���ȡ����
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }

    public void SetupSword(Vector2 _dir, float _gravityScale, Player _player)
    {
        player = _player;
        if(rb == null)
        {
            Debug.Log("Rigidbody2D is null...");
        }
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
        //���Ŷ���
        anim.SetBool("Rotation", true);
    }

    //���ض̽�ʱ���ò����ĺ���
    public void ReturnSword()
    {
        rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;
    }

    private void Update()
    {
        //�������ٶȸ�ֵ���̽�������ҷ���
        //��һ�д����������ʵ��һ����ɫ������ʼ�ճ������ƶ����򣬱���һ�������������˶�������ת����һ�����еļ�ͷʼ��ָ����ǰ���ķ���
        //�������Ƕ̽�����ʼ���ǳ����ƶ�����
        if(canRotate)
            transform.right = rb.velocity;

        if (isReturning)
        {
            //���ض̽�
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);

            //С��ĳ���������ٶ̽�
            if(Vector2.Distance(transform.position, player.transform.position) < 1)
                player.ClearTheSword();
        }
    }

    //���봥����ʱ��Щ���溯��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ȡ������
        anim.SetBool("Rotation", false);

        canRotate = false;
        //�ر���ײ��
        cd.enabled = false;

        //�ı�����������Ϊ,����Ϊ���ж�̬���˶��ĸ���
        rb.isKinematic = true;
        //�ı�����Լ��������λ�ú���ת
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        //�ı�̽��ĸ������Ӵ���˭����Ϊ���Ӽ�
        transform.parent = collision.transform;
    }
}
