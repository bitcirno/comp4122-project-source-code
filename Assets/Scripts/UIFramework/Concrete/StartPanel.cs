using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Start main panel
/// </summary>
public class StartPanel : BasePanel
{
    static readonly string path = "Prefabs/UI/Panel/StartPanel";

    public StartPanel() : base(new UIType(path)) { }

    public override void OnEnter()
    {
        // Main Menu
        UITool.GetOrAddComponentInChildren<Button>("BtnNewGame").onClick.AddListener(() =>
        {
            SaveLoadManager.generateDefultJson();
            // Click events can be written in here
            GameRoot.Instance.SceneSystem.SetScene(new LevelScene("Level1"));
        });

        UITool.GetOrAddComponentInChildren<Button>("BtnContinue").onClick.AddListener(() =>
        {
            if (SaveLoadManager.haveSaveData())
            {
                string levelName = SaveLoadManager.getLevelName();
                // Click events can be written in here
                GameRoot.Instance.SceneSystem.SetScene(new LevelScene(levelName));
            }

        });

        UITool.GetOrAddComponentInChildren<Button>("BtnMore").onClick.AddListener(() =>
        {
            // Click events can be written in here
            Debug.Log("The More button was clicked");
            UITool.FindChildGameObject("Main Menu").SetActive(false);
            UITool.FindChildGameObject("More").SetActive(true);
        });

        UITool.GetOrAddComponentInChildren<Button>("BtnQuit").onClick.AddListener(() =>
        {
            // Click events can be written in here
            Application.Quit();
        });

        // More
        UITool.GetOrAddComponentInChildren<Button>("BtnMainMenu").onClick.AddListener(() =>
        {
            // Click events can be written in here
            UITool.FindChildGameObject("More").SetActive(false);
            UITool.FindChildGameObject("Main Menu").SetActive(true);
        });

        UITool.GetOrAddComponentInChildren<Button>("BtnWeb").onClick.AddListener(() =>
        {
            // Click events can be written in here
            GameRoot.Instance.SceneSystem.SetScene(new WebScene());
        });

        UITool.GetOrAddComponentInChildren<Button>("BtnSetting").onClick.AddListener(() =>
        {
            // Click events can be written in here
            Debug.Log("The Setting button was clicked");
            PanelManager.Push(new SettingPanel());
        });

        UITool.GetOrAddComponentInChildren<Button>("BtnRules").onClick.AddListener(() =>
        {
            // Click events can be written in here
            Debug.Log("The Rules button was clicked");
            PanelManager.Push(new SettingPanel());
        });


        UITool.FindChildGameObject("More").SetActive(false);
    }
}
