using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash FX")]
    [SerializeField] private float flashDuration;               //����ʱ��
    [SerializeField] private Material hitMat;                   //�ܻ�����
    private Material originalMat;                               //ԭʼ����

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    //Э��--���ڱ�ʾ�ܻ�״̬��������flashDuration���ı�ͼƬ��Material
    private IEnumerator FlashFX()
    {
        sr.material = hitMat;

        yield return new WaitForSeconds(flashDuration);

        sr.material = originalMat;
    }

    //�ܻ�������˸Ч��,�ڸ�״̬�У�Ч��Ϊ�����˸
    private void RedColorBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }
    //ȡ���ܻ�������˸Ч��
    private void CancelRedBlink()
    {
        //ȡ���� MonoBehaviour �ϵ����� Invoke ���á�
        CancelInvoke();
        //��ɫ��Ϊԭ���İ�ɫ
        sr.color = Color.white;
    }
}
