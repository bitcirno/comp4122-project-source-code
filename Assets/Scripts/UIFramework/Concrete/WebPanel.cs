using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// The main panel of the main scene
/// </summary>
public class WebPanel : BasePanel
{
    static readonly string path = "Prefabs/UI/Panel/WebPanel";

    public WebPanel() : base(new UIType(path)) { }

    private WebManager webManager;

    private TMP_Text output;

    public override void OnEnter()
    {
        webManager = GameObject.Find("WebManager").GetComponent<WebManager>();
        output = UITool.GetOrAddComponentInChildren<TMP_Text>("TextOutput");


        UITool.GetOrAddComponentInChildren<Button>("BtnBack").onClick.AddListener(() =>
        {
            GameRoot.Instance.SceneSystem.SetScene(new StartScene());
            // Pop();
        });

        UITool.GetOrAddComponentInChildren<Button>("BtnSetting").onClick.AddListener(() =>
        {
            Push(new SettingPanel());
        });

        UITool.GetOrAddComponentInChildren<Button>("BtnGet").onClick.AddListener(() =>
        {
            string uri = UITool.GetOrAddComponentInChildren<TMP_InputField>("GetURI").text;
            webManager.GetRequest(uri);
            Debug.Log(uri);
        });

        UITool.GetOrAddComponentInChildren<Button>("BtnPut").onClick.AddListener(() =>
        {
            string uri = UITool.GetOrAddComponentInChildren<TMP_InputField>("PutURI").text;
            string data = UITool.GetOrAddComponentInChildren<TMP_InputField>("PutData").text;
            webManager.PutRequest(uri, data);
        });

        UITool.GetOrAddComponentInChildren<Button>("BtnPost").onClick.AddListener(() =>
        {
            string uri = UITool.GetOrAddComponentInChildren<TMP_InputField>("PostURI").text;
            string field = UITool.GetOrAddComponentInChildren<TMP_InputField>("PostField").text;
            string data = UITool.GetOrAddComponentInChildren<TMP_InputField>("PostData").text;
            webManager.PostRequest(uri, field, data);
        });
    }
}
