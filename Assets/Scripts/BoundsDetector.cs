using System;
using System.Collections.Generic;
using UnityEngine;

public class BoundsDetector : MonoBehaviour
{
    private Bounds detectionBounds;
    [SerializeField] private GameObject Obj_Area;
    
    private GameObject targetMonster = null;
    private Action<IMonster> targetUpdate;
    private bool isInit = false;
    private List<IMonster> targetList;

    private void OnEnable()
    {
        TargetManager.Instance.TargetReceived += (value) => targetList = value;
    }

    private void OnDisable()
    {
        TargetManager.Instance.TargetReceived -= (value) => targetList = value;
    }

    private void FixedUpdate()
    {
        if (targetMonster != null && targetMonster.activeSelf)
        {
            TargetExitcheck();
            return;
        }
        
        TargetUpdate();
    }

    public void SetBoundsSize(float radius, Action<IMonster> updateTarget)
    {
        detectionBounds.size = new Vector3(radius, radius, radius);
        detectionBounds.center = Vector3.zero;

        Obj_Area.transform.localScale = detectionBounds.size;
        targetUpdate = updateTarget;
        isInit = true;
    }

    private void TargetUpdate()
    {
        if (targetList == null)
            return;
        
        foreach (var monster in targetList)
        {
            GameObject target = monster.GetMonster();
            if (detectionBounds.Contains(target.transform.position))
            {
                targetMonster = target;
                targetUpdate?.Invoke(monster);
            }
        }
    }

    private void TargetExitcheck()
    {
        if (!detectionBounds.Contains(targetMonster.transform.position))
        {
            targetMonster = null;
        }
    }
}
