using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill : Skill
{
    [Header("Sword info")]
    [SerializeField] private GameObject swordPrefab;            //短剑预制体
    [SerializeField] private Vector2 launchForce;               //发射方向
    [SerializeField] private float swordGravity;                //控制短剑下落的重力



    private Vector2 finalDir;                                   //最终方向

    [Header("Aim dots")]
    [SerializeField] private int numberOfDots;                  //点的数量
    [SerializeField] private float spaceBewteenDots;            //点于点之间的距离
    [SerializeField] private GameObject dotPrefab;              //用于表示点的预制体
    [SerializeField] private Transform dotsParent;              //点的父级

    private GameObject[] dots;                                  //存储这些点的数组

    protected override void Start()
    {
        base.Start();

        //开始时先创建这些点，并且取消可见性，只有当进入AimSword状态时才能打开可见性
        GenerateDots();
    }

    protected override void Update()
    {
        base.Update();

        //当松开鼠标右键时就确定了最终发射方向
        //通过计算发射方向函数得到方向，而normalized表示归一化，限制在0到1之间，这样是为了让其仅表示方向
        if(Input.GetKeyUp(KeyCode.Mouse1))
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);

        //按住鼠标右键时查看点的位置
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
        //实例Sword对象，在玩家的位置上，并且保持玩家的旋转
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, player.transform.rotation);
        //调用挂载在实例下的脚本Sword_Skill_Controller
        newSword.GetComponent<Sword_Skill_Controller>().SetupSword(finalDir, swordGravity, player);

        //设置短剑对象
        player.AssignNewSword(newSword);

        //抛出短剑之后取消可见性
        DotsActive(false);
    }

    //计算发射方向的函数，用于投掷剑和设置辅助点
    public Vector2 AimDirection()
    {
        //当前玩家位置
        Vector2 playerPosition = player.transform.position;
        //当前鼠标位置
        //ScreenToWorldPoint函数是将屏幕空间位置转换为世界空间,通过该函数将鼠标在屏幕上的位置转换成世界空间位置
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //得到发射的方向
        //通过向量的减法
        Vector2 direction = mousePosition - playerPosition;             //也就是从玩家位置指向鼠标位置
        return direction;
    }

    //控制可见性的函数
    public void DotsActive(bool _isActive)
    {
        for(int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }

    //生成点对象的函数
    private void GenerateDots()
    {
        //确定数量
        dots = new GameObject[numberOfDots];
        //通过for循环生成
        for(int i = 0; i < numberOfDots; i++)
        {
            //实例化
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            //设置可见性
            dots[i].SetActive(false);
        }
    }

    //确定点的坐标位置
    private Vector2 DotsPosition(float t)
    {
        //以当前玩家角色为原点，通过AimDirection和launchForce力再借助传入的参数t（t表示在以玩家为坐标中心的坐标系的横轴坐标，
        //不同点乘上点间距表示当前点的横坐标）确定横坐标，
        //而后面的就是计算纵坐标的方法，计算高度公式： h = 1/2 * g *t*t
        Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) * t + .5f * (Physics2D.gravity * swordGravity) * (t * t);
        return position;
    }
}
