using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxBackground : MonoBehaviour
{
    //声明一个摄像机游戏对象
    private GameObject cam;

    [SerializeField]
    private float parallaxEffect;

    private float xPosition;
    private float length;

    void Start()
    {
        //获取当前场景中的Main Camera对象，通过Find（）函数查找
        cam = GameObject.Find("Main Camera");

        //获取当前精灵对象的长度,bounds 是 SpriteRenderer 组件的一个属性，返回一个 Bounds 结构体，表示该精灵在世界空间中的边界框。
        //size 是 Bounds 结构体的一个属性，返回一个 Vector3，表示边界框的大小。在这种情况下，它包含三个分量：宽度、高度和深度。
        length = GetComponent<SpriteRenderer>().bounds.size.x;

        xPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //移动距离为摄像机的x轴位置乘上parallaxEffect
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
