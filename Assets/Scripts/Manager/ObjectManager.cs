using System;
using UnityEditor;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    public GameObject GetObject<T>(Action callback = null)
    {
        string path = "Assets/Prefabs/" + typeof(T).Name + ".prefab";
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        GameObject Obj = Instantiate(prefab);

        callback?.Invoke();
        return Obj;
    }

    public GameObject GetObject(string prefabName,Transform parents = null)
    {
        string path = "Assets/Prefabs/" + prefabName + ".prefab";
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        GameObject Obj = Instantiate(prefab, parents);

        return Obj;
    }
}
