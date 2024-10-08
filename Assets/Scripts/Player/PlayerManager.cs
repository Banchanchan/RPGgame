using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //这段代码通常用于实现单例模式。单例模式确保一个类只有一个实例，并提供一个全局访问点。
    //通过将 instance 声明为 static，你可以在程序的任何地方通过 PlayerManager.instance 进行访问，而不需要创建新的 PlayerManager 实例。
    public static PlayerManager instance;
    public Player player;

    private void Awake()
    {
        //只能有一个PlayerManager实例,即单例模式
        if(instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
        
    }
}
