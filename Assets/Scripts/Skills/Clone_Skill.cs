using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill : Skill
{
    [Header("Clone info")]
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    [Space]
    [SerializeField] private bool canAttack;

    public void CreateClone(Transform _clonePosition)
    {
        //ͨ��Ԥ���崴����ʵ��
        GameObject newClone = Instantiate(clonePrefab);
        //ͨ��ʵ���µ�Clone_Skill_Controller�ű��������ʼλ��
        newClone.GetComponent<Clone_Skill_Controller>().SetupClone(_clonePosition, cloneDuration, canAttack);
    }
}
