using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxBackground : MonoBehaviour
{
    //����һ���������Ϸ����
    private GameObject cam;

    [SerializeField]
    private float parallaxEffect;

    private float xPosition;
    private float length;

    void Start()
    {
        //��ȡ��ǰ�����е�Main Camera����ͨ��Find������������
        cam = GameObject.Find("Main Camera");

        //��ȡ��ǰ�������ĳ���,bounds �� SpriteRenderer �����һ�����ԣ�����һ�� Bounds �ṹ�壬��ʾ�þ���������ռ��еı߽��
        //size �� Bounds �ṹ���һ�����ԣ�����һ�� Vector3����ʾ�߽��Ĵ�С������������£�������������������ȡ��߶Ⱥ���ȡ�
        length = GetComponent<SpriteRenderer>().bounds.size.x;

        xPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //�ƶ�����Ϊ�������x��λ�ó���parallaxEffect
        float distanceToMove = cam.transform.position.x * parallaxEffect;
        //
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

        if(distanceMoved > xPosition + length)
        {
            xPosition = xPosition + length;
        }
        else if(distanceMoved < xPosition - length)
        {
            xPosition = xPosition - length;
        }
    }
}
