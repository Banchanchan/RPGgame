using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash FX")]
    [SerializeField] private float flashDuration;               //持续时间
    [SerializeField] private Material hitMat;                   //受击材质
    private Material originalMat;                               //原始材质

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    //协程--用于表示受击状态，就是在flashDuration秒后改变图片是Material
    private IEnumerator FlashFX()
    {
        sr.material = hitMat;

        yield return new WaitForSeconds(flashDuration);

        sr.material = originalMat;
    }

    //受击后退闪烁效果,在该状态中，效果为红白闪烁
    private void RedColorBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }
    //取消受击后退闪烁效果
    private void CancelRedBlink()
    {
        //取消该 MonoBehaviour 上的所有 Invoke 调用。
        CancelInvoke();
        //颜色变为原来的白色
        sr.color = Color.white;
    }
}
