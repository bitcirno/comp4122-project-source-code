using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI management tools, including operations to get a child object component
/// </summary>
public class UITool
{
    /// <summary>
    /// Current active panel
    /// </summary>
    GameObject activePanel;

    public UITool(GameObject panel)
    {
        activePanel = panel;
    }

    /// <summary>
    /// Gets or adds a component to the current active panel
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    /// <returns>component</returns>
    public T GetOrAddComponent<T>() where T : Component
    {
        if (activePanel.GetComponent<T>() == null)
            activePanel.AddComponent<T>();

        return activePanel.GetComponent<T>();
    }

    /// <summary>
    /// Finds a child object by name
    /// </summary>
    /// <param name="name">Child object name</param>
    /// <returns></returns>
    public GameObject FindChildGameObject(string name)
    {
        Transform[] trans = activePanel.GetComponentsInChildren<Transform>(true);

        foreach (Transform item in trans)
        {
            if (item.name == name)
            {
                return item.gameObject;
            }
        }

        Debug.LogWarning($"{activePanel.name} could not find child object named {name}");
        return null;
    }

    /// <summary>
    /// Component that gets a child object by name
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    /// <param name="name">Child object name</param>
    /// <returns></returns>
    public T GetOrAddComponentInChildren<T>(string name) where T : Component
    {
        GameObject child = FindChildGameObject(name);
        if (child)
        {
            if (child.GetComponent<T>() == null)
                child.AddComponent<T>();

            return child.GetComponent<T>();
        }
        return null;
    }
}
