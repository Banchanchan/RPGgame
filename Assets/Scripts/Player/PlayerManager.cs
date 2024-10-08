using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //��δ���ͨ������ʵ�ֵ���ģʽ������ģʽȷ��һ����ֻ��һ��ʵ�������ṩһ��ȫ�ַ��ʵ㡣
    //ͨ���� instance ����Ϊ static��������ڳ�����κεط�ͨ�� PlayerManager.instance ���з��ʣ�������Ҫ�����µ� PlayerManager ʵ����
    public static PlayerManager instance;
    public Player player;

    private void Awake()
    {
        //ֻ����һ��PlayerManagerʵ��,������ģʽ
        if(instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
        
    }
}
