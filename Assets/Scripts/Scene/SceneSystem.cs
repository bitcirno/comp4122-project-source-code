using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The state management system for the scenario
/// </summary>
public class SceneSystem
{
    /// <summary>
    /// Scene state class
    /// </summary>
    SceneState sceneState;

    /// <summary>
    /// Sets the current scene and enters the current scene
    /// </summary>
    /// <param name="state"></param>
    public void SetScene(SceneState state)
    {
        // if (sceneState != null)
        //     sceneState.OnExit();
        // sceneState = state;
        // if (sceneState != null)
        //     sceneState.OnEnter();
        sceneState?.OnExit();
        sceneState = state;
        sceneState?.OnEnter();
    }
}
