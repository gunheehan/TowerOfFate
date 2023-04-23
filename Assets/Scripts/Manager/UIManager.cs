using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    Dictionary<string, IUIInterface> UIDictionary = new Dictionary<string, IUIInterface>();

    public IUIInterface OpenUI<T>() where T : IUIInterface
    {
        IUIInterface ui = GetUI<T>();

        if (ui != null)
            return ui;

        string path = "Assets/UI/" + typeof(T).Name + ".prefab";  // UI 프리팹 경로
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        GameObject uiObject = Instantiate(prefab);

        ui = uiObject.GetComponent<T>();

        return ui;
    }

    private IUIInterface GetUI<T>()
    {
        if (UIDictionary.ContainsKey(typeof(T).Name))
            return UIDictionary[typeof(T).Name];

        return null;
    }

}
