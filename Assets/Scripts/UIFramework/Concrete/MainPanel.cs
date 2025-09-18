using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The main panel of the main scene
/// </summary>
public class MainPanel : BasePanel
{
    static readonly string path = "Prefabs/UI/Panel/MainPanel";

    public MainPanel() : base(new UIType(path)) { }

    public override void OnEnter()
    {
        // UITool.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(() =>
        // {
        //     GameRoot.Instance.SceneSystem.SetScene(new StartScene());
        //     Pop();
        // });
        // UITool.GetOrAddComponentInChildren<Button>("BtnMsg").onClick.AddListener(() =>
        // {
        //     Push(new TaskPanel());
        // });
        UITool.GetOrAddComponentInChildren<Button>("BtnSetting").onClick.AddListener(() =>
        {
            Push(new SettingPanel());
        });
    }

    public override void OnPause()
    {
        base.OnPause();
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public override void OnResume()
    {
        base.OnResume();
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void OnExit()
    {
        base.OnExit();
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
