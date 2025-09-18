using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Characters;
using Characters.MovingController;

public class SettingPanel : BasePanel
{
    static readonly string path = "Prefabs/UI/Panel/SettingPanel";

    public SettingPanel() : base(new UIType(path)) { }

    private static bool isFirstLook = false;

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("BtnBack").onClick.AddListener(() =>
        {
            PlayerInfo.resetPause();
            PanelManager.Pop();
        });

        UITool.GetOrAddComponentInChildren<Button>("BtnMainMenu").onClick.AddListener(() =>
        {
            GameRoot.Instance.SceneSystem.SetScene(new StartScene());
        });

        UITool.GetOrAddComponentInChildren<Slider>("MusicSlider").value = BgmControl.GetVolume();
        UITool.GetOrAddComponentInChildren<Slider>("MusicSlider").onValueChanged.AddListener((float value) =>
        {
            BgmControl.ChangeVolume(value);
            Debug.Log(value);
        });

        UITool.GetOrAddComponentInChildren<Slider>("SoundSlider").value = PlayerMovingController.getVolume();
        UITool.GetOrAddComponentInChildren<Slider>("SoundSlider").onValueChanged.AddListener((float value) =>
        {
            PlayerMovingController.changeVolume(value);
            Debug.Log(value);
        });

        UITool.GetOrAddComponentInChildren<Toggle>("Toggle_1").isOn = isFirstLook;
        UITool.GetOrAddComponentInChildren<Toggle>("Toggle_1").onValueChanged.AddListener((bool value) =>
        {
            if (value)
            {
                GameObject.Find("Hero1(Clone)")?.GetComponent<Player>().FirstPerson();
                isFirstLook = true;
            }
        });

        UITool.GetOrAddComponentInChildren<Toggle>("Toggle_2").isOn = !isFirstLook;
        UITool.GetOrAddComponentInChildren<Toggle>("Toggle_2").onValueChanged.AddListener((bool value) =>
        {
            if (value)
            {
                GameObject.Find("Hero1(Clone)")?.GetComponent<Player>().ThirdPerson();
                isFirstLook = false;
            }
        });
    }
}
