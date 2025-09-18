using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The Start scene
/// </summary>
public class StartScene : SceneState
{
    /// <summary>
    /// The scene name
    /// </summary>
    readonly string sceneName = "Start";
    PanelManager panelManager;

    public override void OnEnter()
    {
        panelManager = new PanelManager();
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            SceneManager.LoadScene(sceneName);
            SceneManager.sceneLoaded += SceneLoaded;
            Debug.Log($"{sceneName} scene change!");
        }
        else
        {
            panelManager.Push(new StartPanel());
            GameRoot.Instance.SetAction(panelManager.Push);
            Debug.Log($"{sceneName} scene enter!");
        }
    }

    public override void OnExit()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
        panelManager.PopAll();
        Debug.Log($"{sceneName} scene exit!");
    }

    /// <summary>
    /// Method to execute after the scene is loaded
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="load"></param>
    private void SceneLoaded(Scene scene, LoadSceneMode load)
    {
        panelManager.Push(new StartPanel());
        GameRoot.Instance.SetAction(panelManager.Push);
        Debug.Log($"{sceneName} scene loaded!");
    }
}
