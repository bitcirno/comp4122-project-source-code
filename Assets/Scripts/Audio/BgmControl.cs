using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class BgmControl : MonoBehaviour
{
    // Start is called before the first frame update
    private static AudioSource BGM;
    public AudioClip[] Source;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        BGM = GetComponent<AudioSource>();
        ChangeVolume(0.3f);
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public static float GetVolume()
    {
        return BGM.volume;
    }

    public static void ChangeVolume(float value)
    {
        BGM.volume = value;
    }

    void ChangeBGM(AudioClip bgm)
    {
        BGM.PlayOneShot(bgm);
    }

    void Start()
    {

        BGM.PlayOneShot(Source[0]);
    }

    // Update is called once per frame
    void Update()
    {
        // if (!GlobalManager.Instance.IsPlayerAlive())
        // {
        //     BGM.PlayOneShot(Source[2]);
        // }
    }
}
