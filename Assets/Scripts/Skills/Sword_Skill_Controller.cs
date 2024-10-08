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

    //由于引擎问题，刚体这些东西的声明需要放在Awake中，放在Start中会有问题（无法获取到）
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
        //播放动画
        anim.SetBool("Rotation", true);
    }

    //返回短剑时设置参数的函数
    public void ReturnSword()
    {
        rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;
    }

    private void Update()
    {
        //将刚体速度赋值给短剑物体的右方向
        //这一行代码可以用于实现一个角色或物体始终朝向其移动方向，比如一辆车沿着它的运动方向旋转，或一个飞行的箭头始终指向其前进的方向。
        //其结果就是短剑朝向始终是朝向移动方向
        if(canRotate)
            transform.right = rb.velocity;

        if (isReturning)
        {
            //返回短剑
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);

            //小于某个距离销毁短剑
            if(Vector2.Distance(transform.position, player.transform.position) < 1)
                player.ClearTheSword();
        }
    }

    //进入触发器时这些下面函数
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //取消动画
        anim.SetBool("Rotation", false);

        canRotate = false;
        //关闭碰撞体
        cd.enabled = false;

        //改变刚体的物理行为,设置为具有动态性运动的刚体
        rb.isKinematic = true;
        //改变刚体的约束：冻结位置和旋转
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        //改变短剑的父级，接触到谁就作为其子级
        transform.parent = collision.transform;
    }
}
