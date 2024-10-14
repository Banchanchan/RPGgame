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

    [Header("Pierce info")]
    private int pierceAmount;

    [Header("Bounce info")]
    [SerializeField] private float bounceSpeed = 20;                  //弹跳速度
    private bool isBouncing;                 //是否可以在Enemy之间弹跳
    private int bounceAmount;                 //弹跳次数
    private List<Transform> enemyTarget;            //用于存储Enemy位置的列表,如果是public类型，unity会自动创建，而如果是private则要之间初始化
    private int targetIndex;                        //列表索引

    [Header("Spin info")]
    private float maxTravelDistance;                    //离玩家最大距离，也就是投掷距离
    private float spinDuration;                         //旋转持续时间
    private float spinTimer;                            //计时器
    private bool wasStopped;                            //控制停止的参数，当投掷出某个位置时固定在那
    private bool isSpinning;                            //是否在旋转

    private float hitTimer;                             //攻击Enemy的计时器
    private float hitCooldown;                          //冷却时间

    private float spinDirection;                        //旋转剑的移动距离

    //由于引擎问题，刚体这些东西的声明需要放在Awake中，放在Start中会有问题（无法获取到）
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }

    public void SetupBounce(bool _isBouncing, int _amountOfBounce)
    {
        isBouncing = _isBouncing;
        bounceAmount = _amountOfBounce;
        //初始化为空列表
        enemyTarget = new List<Transform>();
    }

    public void SetupPierce(int _pierceAmount)
    {
        pierceAmount = _pierceAmount;
    }

    public void SetupSpin(bool _isSpinning, float _maxTravelDistance, float _spinDuration, float _hitCooldown)
    {
        isSpinning = _isSpinning;
        maxTravelDistance = _maxTravelDistance;
        spinDuration = _spinDuration;
        hitCooldown = _hitCooldown;
    }

    public void SetupSword(Vector2 _dir, float _gravityScale, Player _player)
    {
        player = _player;
        if (rb == null)
        {
            Debug.Log("Rigidbody2D is null...");
        }
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
        //播放动画
        if(pierceAmount <= 0)
            anim.SetBool("Rotation", true);

        //将其限制在-1到1之间，低于就返回-1高于就返回1
        spinDirection = Mathf.Clamp(rb.velocity.x, -1,1);
    }

    //返回短剑时设置参数的函数
    public void ReturnSword()
    {
        //修复当没有接触地面或者Enemy就返回时出现是Bug，动力学不应该取消，冻结位置和旋转
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //rb.iskinematic = false;
        transform.parent = null;
        isReturning = true;
    }

    private void Update()
    {
        //将刚体速度赋值给短剑物体的右方向
        //这一行代码可以用于实现一个角色或物体始终朝向其移动方向，比如一辆车沿着它的运动方向旋转，或一个飞行的箭头始终指向其前进的方向。
        //其结果就是短剑朝向始终是朝向移动方向
        if (canRotate)
            transform.right = rb.velocity;

        if (isReturning)
        {
            //返回短剑
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);

            //小于某个距离销毁短剑
            if (Vector2.Distance(transform.position, player.transform.position) < 1)
                player.CatchTheSword();
        }

        BounceLogic();
        SpinLogic();
    }

    private void SpinLogic()
    {
        //可以旋转
        if (isSpinning)
        {
            //如果旋转的剑和玩家位置距离大于maxTravelDistance并且没有固定时
            if (Vector2.Distance(player.transform.position, transform.position) > maxTravelDistance && !wasStopped)
            {
                StopWhenSpinning();
            }
            //如果可以固定
            if (wasStopped)
            {
                //计时器变化
                spinTimer -= Time.deltaTime;

                //移动
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + spinDirection, transform.position.y), 1.5f * Time.deltaTime);

                //计时器倒计时为零
                if (spinTimer < 0)
                {
                    isReturning = true;
                    isSpinning = false;
                }

                //持续伤害时间
                hitTimer -= Time.deltaTime;
                if (hitTimer < 0)
                {
                    //重置计时器
                    hitTimer = hitCooldown;
                    //碰撞检测,造成持续伤害
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);
                    foreach (var hit in colliders)
                    {
                        if (hit.GetComponent<Enemy>() != null)
                            hit.GetComponent<Enemy>().Damage();

                    }
                }
            }
        }
    }

    private void StopWhenSpinning()
    {
        //可以固定
        wasStopped = true;
        //冻结位置，固定在那
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        //计时器赋值
        spinTimer = spinDuration;
    }

    private void BounceLogic()
    {
        //可以弹跳并且周围有Enemy
        if (isBouncing && enemyTarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bounceSpeed * Time.deltaTime);
            //小于一定距离时移动到下一个Enemy
            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < .1f)
            {
                //受击视觉效果
                enemyTarget[targetIndex].GetComponent<Enemy>().Damage();
                targetIndex++;
                //弹跳次数减一，不能无限弹跳
                bounceAmount--;

                //弹跳次数变为0，就不能弹跳了，并且变为可回收
                if (bounceAmount <= 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }

                if (targetIndex >= enemyTarget.Count)
                    targetIndex = 0;
            }

        }
    }

    //进入触发器时这些下面函数
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //当没有接触到地面或者Enemy就返回时，此时应该不能再有触发器了
        if (isReturning)
        {
            return;
        }

        //调用受击函数,语法糖，相当于if语句，如果不为空则执行Damage函数
        //有个问题是，第二次攻击时Enemy就会翻转，但是应该不需要翻转才对（没有这个需求）
        //原因可能在SkeletonBattleState中的调用SetVelocity函数中，从而调用了Flip函数
        collision.GetComponent<Enemy>()?.Damage();
        SetupTargetsForBounce(collision);
        StuckInTo(collision);
    }

    private void SetupTargetsForBounce(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            //如果可以弹跳并且列表为空
            if (isBouncing && enemyTarget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                        enemyTarget.Add(hit.transform);

                }
            }
        }
    }

    private void StuckInTo(Collider2D collision)
    {
        if (pierceAmount > 0 && collision.GetComponent<Enemy>() != null)
        {
            pierceAmount--;
            return;
        }

        if (isSpinning)
        {
            //攻击到Enemy停下
            StopWhenSpinning();
            return;
        }

        canRotate = false;
        //关闭碰撞体
        cd.enabled = false;

        //改变刚体的物理行为,设置为具有动态性运动的刚体
        rb.isKinematic = true;
        //改变刚体的约束：冻结位置和旋转
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        //可以弹跳并且周围有Enemy时，旋转动画和设置父级不需要执行
        if (isBouncing && enemyTarget.Count > 0)
            return;

        //取消动画
        anim.SetBool("Rotation", false);
        //改变短剑的父级，接触到谁就作为其子级
        transform.parent = collision.transform;
    }
}
