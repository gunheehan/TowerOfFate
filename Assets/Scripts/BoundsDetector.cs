using UnityEngine;

public class BoundsDetector : MonoBehaviour
{
    public Bounds detectionBounds;
    [SerializeField] private GameObject Obj_Area;
    
    private GameObject targetMonster = null;

    private bool isInit = false;
    private void FixedUpdate()
    {
        if (targetMonster != null)
        {
            CheckObjectExit();
            return;
        }
        
        CheckObjectEntrance();
    }

    public void SetBoundsSize(float radius)
    {
        radius = 1;
        detectionBounds.size = new Vector3(radius, radius, radius) * 2f;
        detectionBounds.center = Vector3.zero;

        Obj_Area.transform.localScale = detectionBounds.size;

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
                targetMonster = enteredObject;
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
