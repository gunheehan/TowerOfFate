using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    Dictionary<string, IUIInterface> UIDictionary = new Dictionary<string, IUIInterface>();

    private Dictionary<string, Stack<IUIInterface>> UIMultiDictionary =
        new FlexibleDictionary<string, Stack<IUIInterface>>();

    private GameObject multUIContents;

    private void Start()
    {
        multUIContents = new GameObject();
        multUIContents.name = "MultiUIPool";
    }

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
    
    public IUIInterface GetMultiUI<T>() where T : IUIInterface
    {
        if (UIMultiDictionary.ContainsKey(typeof(T).Name))
        {
            if (UIMultiDictionary[typeof(T).Name].Count > 0)
                return UIMultiDictionary[typeof(T).Name].Pop();
        }
        IUIInterface ui;

        string path = "Assets/UI/" + typeof(T).Name + ".prefab";  // UI 프리팹 경로
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        GameObject uiObject = Instantiate(prefab,multUIContents.transform);
        ui = uiObject.GetComponent<T>();

        ui.OpenUI();

        return ui;
    }

    public void PushMultiUI<T>(IUIInterface ui)
    {
        if (!UIMultiDictionary.ContainsKey(typeof(T).Name))
            UIMultiDictionary.Add(typeof(T).Name,new Stack<IUIInterface>());
        
        ui.CloseUI();
        
        UIMultiDictionary[typeof(T).Name].Push(ui);
    }
    private IUIInterface FindUI<T>()
    {
        if (UIDictionary.ContainsKey(typeof(T).Name))
            return UIDictionary[typeof(T).Name];

        return null;
    }
}
