using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// The main panel of the main scene
/// </summary>
public class EndPanel : BasePanel
{
    static readonly string path = "Prefabs/UI/Panel/EndPanel";
    private string output;

    public EndPanel(string gameResult) : base(new UIType(path))
    {
        output = gameResult;
    }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<TMP_Text>("TextOutput").text = output;

        UITool.GetOrAddComponentInChildren<Button>("BtnBack").onClick.AddListener(() =>
        {
            GameRoot.Instance.SceneSystem.SetScene(new StartScene());
            // Pop();
        });

        UITool.GetOrAddComponentInChildren<Button>("BtnSetting").onClick.AddListener(() =>
        {
            Push(new SettingPanel());
        });

        UITool.GetOrAddComponentInChildren<Button>("BtnMainMenu").onClick.AddListener(() =>
        {
            GameRoot.Instance.SceneSystem.SetScene(new StartScene());
        });
    }
}
