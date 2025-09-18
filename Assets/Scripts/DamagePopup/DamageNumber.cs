using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 控制伤害显示
/// </summary>
public class DamageNumber : MonoBehaviour
{
    /// <summary>
    /// 滚动速度
    /// </summary>
    private float speed = 1.5f;

    /// <summary>
    /// 计时器
    /// </summary>
    private float timer = 0f;

    /// <summary>
    /// 销毁时间
    /// </summary>
    private float time = 0.8f;

    private void Update()
    {
        Scroll();
    }

    /// <summary>
    /// 冒泡效果
    /// </summary>
    private void Scroll()
    {
        // 字体滚动
        this.transform.Translate(Vector3.up * speed * Time.deltaTime);
        timer += Time.deltaTime;
        // 字体缩小
        this.GetComponent<TMP_Text>().fontSize -= 0.01f;
        // 字体渐变透明
        this.GetComponent<TMP_Text>().color = new Color(1, 0, 0, 1 - timer);
        Destroy(gameObject, time);
    }
}
