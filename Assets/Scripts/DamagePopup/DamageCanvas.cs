using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 控制伤害效果的生成，附在Canvas上
/// </summary>
public class DamageCanvas : MonoBehaviour
{
    /// <summary>
    /// 文字预制体
    /// </summary>
    public GameObject hudText;

    private void Update()
    {
        Rotation();
    }

    /// <summary>
    /// 生成伤害文字
    /// </summary>
    public void show(int damage)
    {
        GameObject hud = Instantiate(hudText, transform) as GameObject;
        hud.GetComponent<TMP_Text>().text = "-" + damage.ToString();
    }

    /// <summary>
    /// 画布始终朝向摄像机
    /// </summary>
    void Rotation()
    {
        // this.transform.LookAt(Camera.main.transform);
        this.transform.rotation = Camera.main.transform.rotation;
    }
}
