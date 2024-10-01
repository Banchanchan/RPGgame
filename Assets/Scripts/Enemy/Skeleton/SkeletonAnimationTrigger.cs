using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationTrigger : MonoBehaviour
{
    private Enemy_Skeleton enemy => GetComponentInParent<Enemy_Skeleton>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
    private void AttackTrigger()
    {
        //���������ΪattackCheck���뾶ΪattackCheckRadius��Բ�η�Χ�ڵ�������ײ��
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);
        //����������ײ��
        foreach (var hit in colliders)
        {
            //���Player����Ϊ��
            if (hit.GetComponent<Player>() != null)
            {
                //ִ�иú���
                hit.GetComponent<Player>().Damage();
            }
        }
    }

    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();

    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
