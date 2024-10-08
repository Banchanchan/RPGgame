using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;
    [SerializeField] private float colorLoosingSpeed;

    private float cloneTimer;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = .8f;

    private Transform closestEnemy;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if(cloneTimer < 0)
        {
            sr.color = new Color(1,1,1, sr.color.a - (Time.deltaTime * colorLoosingSpeed));

            if (sr.color.a <= 0)
                Destroy(gameObject);
        }
    }

    public void SetupClone(Transform _newTransform, float _cloneDuration, bool _canAttack)
    {
        if (_canAttack)
            anim.SetInteger("AttackNumber", Random.Range(1, 4));

        transform.position = _newTransform.position;
        cloneTimer = _cloneDuration;

        FaceClosestTarget();
    }

    //�����˳�����
    private void AnimationTrigger()
    {
        cloneTimer = -1;
    }

    private void AttackTrigger()
    {
        //��������ĵ�ΪattackCheck���뾶ΪattackCheckRadius��Բ�η�Χ�ڵ�������ײ��
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        //����������ײ��
        foreach (var hit in colliders)
        {
            //���Enemy����Ϊ��
            if (hit.GetComponent<Enemy>() != null)
            {
                //ִ�иú���
                hit.GetComponent<Enemy>().Damage();
            }
        }
    }

    //���ÿ�¡��ĳ�����
    private void FaceClosestTarget()
    {
        //��һ����Χ�ڼ��
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);
        //����ľ���,������ѧ�������������
        float closestDistance = Mathf.Infinity;

        //����
        foreach(var hit in colliders)
        {
            //�ǿ�
            if(hit.GetComponent<Enemy>() != null)
            {
                //clone��Enemy�ľ���
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                //�������֮��ľ���С���������,����ָ��Ϊ�����Enemy����
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }

        if(closestEnemy != null)
        {
            //Clone��λ����Enemy���ұ�, ��ת
            if (transform.position.x > closestEnemy.position.x)
                transform.Rotate(0, 180, 0);
        }
    }
}
