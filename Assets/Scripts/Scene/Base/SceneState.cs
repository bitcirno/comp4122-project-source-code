using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scene: the state
/// </summary>
public abstract class SceneState
{
    /// <summary>
    /// Actions performed when the scene enters
    /// </summary>
    public abstract void OnEnter();

    /// <summary>
    /// Operation performed when the scenario exits
    /// </summary>
    public abstract void OnExit();
}
