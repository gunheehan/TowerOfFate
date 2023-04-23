using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    Dictionary<string, IUIInterface> UIDictionary = new Dictionary<string, IUIInterface>();

    public IUIInterface GetUI<T>() where T : IUIInterface
    {
        IUIInterface ui = FindUI<T>();

        if (ui != null)
            return ui;

        string path = "Assets/UI/" + typeof(T).Name + ".prefab";  // UI 프리팹 경로
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        GameObject uiObject = Instantiate(prefab);

        ui = uiObject.GetComponent<T>();
        UIDictionary.Add(typeof(T).Name, ui);

        return ui;
    }

    private IUIInterface FindUI<T>()
    {
        if (UIDictionary.ContainsKey(typeof(T).Name))
            return UIDictionary[typeof(T).Name];

        return null;
    }
}
