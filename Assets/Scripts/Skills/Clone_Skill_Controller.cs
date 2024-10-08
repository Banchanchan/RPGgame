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

    //动画退出条件
    private void AnimationTrigger()
    {
        cloneTimer = -1;
    }

    private void AttackTrigger()
    {
        //检测在中心的为attackCheck，半径为attackCheckRadius的圆形范围内的所有碰撞体
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        //遍历所有碰撞体
        foreach (var hit in colliders)
        {
            //如果Enemy对象不为空
            if (hit.GetComponent<Enemy>() != null)
            {
                //执行该函数
                hit.GetComponent<Enemy>().Damage();
            }
        }
    }

    //设置克隆体的朝向函数
    private void FaceClosestTarget()
    {
        //在一个范围内检测
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);
        //最近的距离,等于数学函数的正无穷大
        float closestDistance = Mathf.Infinity;

        //遍历
        foreach(var hit in colliders)
        {
            //非空
            if(hit.GetComponent<Enemy>() != null)
            {
                //clone和Enemy的距离
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                //如果两者之间的距离小于最近距离,将其指定为最近的Enemy变量
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }

        if(closestEnemy != null)
        {
            //Clone的位置在Enemy的右边, 翻转
            if (transform.position.x > closestEnemy.position.x)
                transform.Rotate(0, 180, 0);
        }
    }
}
