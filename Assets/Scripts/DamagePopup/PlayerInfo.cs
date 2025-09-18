using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    private Slider hpSlider;
    private int hp;
    private float timer = 0f;
    private static bool paused = false;

    public TMP_Text postText;
    public int maxHp = 100;
    public GameObject hpColor;
    public TMP_Text hpText;
    public TMP_Text ammunitionText;
    public BasePanel ingameMenu;

    public static PlayerInfo Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        hpSlider = transform.GetComponentInChildren<Slider>();
        // hpText = transform.GetComponentInChildren<TMP_Text>();
        // init();
    }

    public void init()
    {
        init(maxHp);
    }

    public void init(int curHp)
    {
        init(curHp, maxHp);
    }

    public void init(int curHp, int max)
    {
        hp = curHp;
        maxHp = max;
        draw();
    }

    void draw()
    {
        hpSlider.value = (float)hp / (float)maxHp;

        if (hpSlider.value > 0.5)
        {
            hpColor.GetComponent<Image>().color = new Color32(150, 215, 34, 255);
        }
        else
        {
            hpColor.GetComponent<Image>().color = new Color32(255, 90, 90, 255);
        }

        hpText.text = hp.ToString() + " / " + maxHp.ToString();
    }

    public void lossHp(int loss)
    {
        hp -= loss;

        if (hp < 0)
        {
            hp = 0;
        }
        draw();
    }

    public void increaseHp(int increase)
    {
        hp += increase;

        if (hp > maxHp)
        {
            hp = maxHp;
        }
        draw();
    }

    public void setAmmunition(string weaponName, int curAmmunition, int maxAmmunition)
    {
        ammunitionText.text = weaponName + "\n" + curAmmunition.ToString() + " / " + maxAmmunition.ToString();

        if ((float)curAmmunition / (float)maxAmmunition > 0.3)
        {
            ammunitionText.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            ammunitionText.color = new Color32(255, 0, 0, 255);
        }
    }

    public void post(String post)
    {
        // Debug.Log("post something");
        postText.text = post;
        timer = 0f;
        postText.color = new Color(1, 1, 1, 1);
    }

    void Update()
    {
        timer += Time.deltaTime / 2;
        postText.color = new Color(1, 1, 1, 1 - timer);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                OnPause();
            }
            else
            {
                OnResume();
            }
        }
    }

    public void OnPause()
    {
        paused = true;
        ingameMenu = new SettingPanel();
        GameRoot.Instance.Push(ingameMenu);
    }

    public void OnResume()
    {
        paused = false;
        ingameMenu.Pop();
    }

    public static void resetPause()
    {
        paused = false;
    }
}
