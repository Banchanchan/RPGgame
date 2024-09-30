using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        //检测在中心的为attackCheck，半径为attackCheckRadius的圆形范围内的所有碰撞体
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        //遍历所有碰撞体
        foreach(var hit in colliders)
        {
            //如果Enemy对象不为空
            if(hit.GetComponent<Enemy>() != null)
            {
                //执行该函数
                hit.GetComponent<Enemy>().Damage();
            }
        }
    }
}
