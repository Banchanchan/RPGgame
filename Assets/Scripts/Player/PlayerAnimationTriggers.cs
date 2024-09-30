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
        //��������ĵ�ΪattackCheck���뾶ΪattackCheckRadius��Բ�η�Χ�ڵ�������ײ��
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        //����������ײ��
        foreach(var hit in colliders)
        {
            //���Enemy����Ϊ��
            if(hit.GetComponent<Enemy>() != null)
            {
                //ִ�иú���
                hit.GetComponent<Enemy>().Damage();
            }
        }
    }
}
