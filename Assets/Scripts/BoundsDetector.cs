using System;
using UnityEngine;

public class BoundsDetector : MonoBehaviour
{
    private Bounds detectionBounds;
    [SerializeField] private GameObject Obj_Area;
    
    private GameObject targetMonster = null;
    private Action<IMonster> targetUpdate;
    private bool isInit = false;
    
    private void FixedUpdate()
    {
        if (targetMonster != null && targetMonster.activeSelf)
        {
            CheckObjectExit();
            return;
        }
        
        CheckObjectEntrance();
    }

    public void SetBoundsSize(float radius, Action<IMonster> updateTarget)
    {
        radius = 1;
        detectionBounds.size = new Vector3(radius, radius, radius) * 2f;
        detectionBounds.center = Vector3.zero;

        Obj_Area.transform.localScale = detectionBounds.size;
        targetUpdate = updateTarget;
        isInit = true;
    }

    private void CheckObjectEntrance()
    {
        Collider[] colliders = Physics.OverlapSphere(detectionBounds.center, detectionBounds.extents.x);
        foreach (Collider collider in colliders)
        {
            GameObject enteredObject = collider.gameObject;
            IMonster monster = enteredObject.GetComponent<IMonster>();

            if (monster != null)
            {
                targetMonster = enteredObject;
                targetUpdate?.Invoke(monster);
            }

            Debug.Log(enteredObject.name + " Entered the bounds.");
        }
    }

    private void CheckObjectExit()
    {
        if (!detectionBounds.Contains(targetMonster.transform.position))
        {
            Debug.Log(targetMonster.name + " Exited the bounds.");
            targetMonster = null;
        }
    }
}
