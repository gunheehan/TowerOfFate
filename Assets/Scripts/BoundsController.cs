using System;
using UnityEngine;
using UnityEngine.Events;

public class BoundsController : MonoBehaviour
{
    private Bounds bounds;
    private Vector3 boundsSize = Vector3.one;
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
        bounds = new Bounds(Vector3.zero, boundsSize);
        SphereCollider collider = gameObject.AddComponent<SphereCollider>();
        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        collider.isTrigger = true;
        collider.center = transform.position;
        collider.radius = boundsSize.magnitude / 2f;
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
