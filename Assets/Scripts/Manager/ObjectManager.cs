using System;
using UnityEditor;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    public GameObject GetObject<T>(Action callback = null)
    {
        string path = "Assets/Prefabs/" + typeof(T).Name + ".prefab"; // UI 프리팹 경로
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        GameObject Obj = Instantiate(prefab);

        callback?.Invoke();
        return Obj;
    }
}
