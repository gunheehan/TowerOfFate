using UnityEngine;

public class BoundsController : MonoBehaviour
{
    private Vector3 boundsSize;
    private GameObject movingObject;
    private LayerMask layerMask;

    private void Start()
    {
        layerMask = LayerMask.GetMask("Monster");
    }

    private void OnEnable()
    {
        gameObject.transform.localPosition = Vector3.zero;
    }

    public void SetBounds()
    {
        SphereCollider collider = gameObject.AddComponent<SphereCollider>();
        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        collider.isTrigger = true;
        collider.center = transform.position;
        collider.radius = StageManager.Instance.floorSize / 2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (layerMask == 1 << other.gameObject.layer)
        {
            Debug.Log("Monster In My Area!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (layerMask == 1 << other.gameObject.layer)
        {
            Debug.Log("Monster Exit My Area!");
        }
    }
}
