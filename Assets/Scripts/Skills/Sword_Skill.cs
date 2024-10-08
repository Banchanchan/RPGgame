using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill : Skill
{
    [Header("Sword info")]
    [SerializeField] private GameObject swordPrefab;            //�̽�Ԥ����
    [SerializeField] private Vector2 launchForce;               //���䷽��
    [SerializeField] private float swordGravity;                //���ƶ̽����������



    private Vector2 finalDir;                                   //���շ���

    [Header("Aim dots")]
    [SerializeField] private int numberOfDots;                  //�������
    [SerializeField] private float spaceBewteenDots;            //���ڵ�֮��ľ���
    [SerializeField] private GameObject dotPrefab;              //���ڱ�ʾ���Ԥ����
    [SerializeField] private Transform dotsParent;              //��ĸ���

    private GameObject[] dots;                                  //�洢��Щ�������

    protected override void Start()
    {
        base.Start();

        //��ʼʱ�ȴ�����Щ�㣬����ȡ���ɼ��ԣ�ֻ�е�����AimSword״̬ʱ���ܴ򿪿ɼ���
        GenerateDots();
    }

    protected override void Update()
    {
        base.Update();

        //���ɿ�����Ҽ�ʱ��ȷ�������շ��䷽��
        //ͨ�����㷢�䷽�����õ����򣬶�normalized��ʾ��һ����������0��1֮�䣬������Ϊ���������ʾ����
        if(Input.GetKeyUp(KeyCode.Mouse1))
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);

        //��ס����Ҽ�ʱ�鿴���λ��
        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBewteenDots);
            }
        }
    }

    public void CteateSword()
    {
        //ʵ��Sword��������ҵ�λ���ϣ����ұ�����ҵ���ת
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, player.transform.rotation);
        //���ù�����ʵ���µĽű�Sword_Skill_Controller
        newSword.GetComponent<Sword_Skill_Controller>().SetupSword(finalDir, swordGravity, player);

        //���ö̽�����
        player.AssignNewSword(newSword);

        //�׳��̽�֮��ȡ���ɼ���
        DotsActive(false);
    }

    //���㷢�䷽��ĺ���������Ͷ���������ø�����
    public Vector2 AimDirection()
    {
        //��ǰ���λ��
        Vector2 playerPosition = player.transform.position;
        //��ǰ���λ��
        //ScreenToWorldPoint�����ǽ���Ļ�ռ�λ��ת��Ϊ����ռ�,ͨ���ú������������Ļ�ϵ�λ��ת��������ռ�λ��
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //�õ�����ķ���
        //ͨ�������ļ���
        Vector2 direction = mousePosition - playerPosition;             //Ҳ���Ǵ����λ��ָ�����λ��
        return direction;
    }

    //���ƿɼ��Եĺ���
    public void DotsActive(bool _isActive)
    {
        for(int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }

    //���ɵ����ĺ���
    private void GenerateDots()
    {
        //ȷ������
        dots = new GameObject[numberOfDots];
        //ͨ��forѭ������
        for(int i = 0; i < numberOfDots; i++)
        {
            //ʵ����
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            //���ÿɼ���
            dots[i].SetActive(false);
        }
    }

    //ȷ���������λ��
    private Vector2 DotsPosition(float t)
    {
        //�Ե�ǰ��ҽ�ɫΪԭ�㣬ͨ��AimDirection��launchForce���ٽ�������Ĳ���t��t��ʾ�������Ϊ�������ĵ�����ϵ�ĺ������꣬
        //��ͬ����ϵ����ʾ��ǰ��ĺ����꣩ȷ�������꣬
        //������ľ��Ǽ���������ķ���������߶ȹ�ʽ�� h = 1/2 * g *t*t
        Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) * t + .5f * (Physics2D.gravity * swordGravity) * (t * t);
        return position;
    }
}
