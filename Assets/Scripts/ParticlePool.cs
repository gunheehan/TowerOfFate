using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum particleType
{
    muzzzle
}
public class ParticlePool : MonoBehaviour
{
    private Dictionary<particleType, Stack<ParticleSystem>> particleDic = new Dictionary<particleType, Stack<ParticleSystem>>();
    private const string ParticlePath = "EffectTexturesAndPrefabs/Prefabs/";
    //Assets/EffectTexturesAndPrefabs/Prefabs/MuzzleFlash.prefab
    private static ParticlePool _instance = null;

    public static ParticlePool instance
    {
        get
        {
            if (_instance == null)
                _instance = new ParticlePool();
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public ParticleSystem GetParticle(particleType type)
    {
        ParticleSystem particleSystem;

        if (!particleDic.ContainsKey(type))
        {
            string prefabName = GetparticleName(type);

            if (string.IsNullOrEmpty(prefabName))
                return null;

            string prefabPath = ParticlePath + prefabName + ".prefab";
            
            GameObject particlePrefab = Resources.Load<GameObject>(prefabPath);
            particleSystem = particlePrefab.GetComponent<ParticleSystem>();
        }
        else
        {
            particleSystem = particleDic[type].Pop();
        }

        return particleSystem;
    }

    public GameObject GetImpactObject(particleType type, Transform parents)
    {
        string prefabName = GetparticleName(type);

        if (string.IsNullOrEmpty(prefabName))
            return null;

        string prefabPath = ParticlePath + prefabName;
        
        GameObject particlePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/EffectTexturesAndPrefabs/Prefabs/MuzzleFlash.prefab");
        GameObject Obj = Instantiate(particlePrefab,parents.position,Quaternion.identity);
        return Obj;
    }

    private string GetparticleName(particleType type)
    {
        string prefabname = string.Empty;

        switch (type)
        {
            case particleType.muzzzle:
                prefabname = "MuzzleFlash";
                break;
        }

        return prefabname;
    }

}
