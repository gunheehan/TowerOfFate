using System;
using UnityEngine;
using UnityEngine.Events;

public class BoundsController : MonoBehaviour
{
    private Bounds bounds;
    private Vector3 boundsSize = Vector3.one; // 반지름이 10인 크기로 변경
    private GameObject movingObject;
    private const string targetTag = "Monster"; // 감지할 대상 오브젝트의 태그

    private void OnEnable()
    {
        gameObject.transform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        if (CheckCollision())
        {
            // 대상 오브젝트가 bounds 내에 들어왔을 때
            Debug.Log("대상 오브젝트가 bounds 내에 들어왔습니다.");
        }
        else
        {
            Debug.Log("대상 오브젝트가 bounds 내에 벗어났습니다.");

        }
    }

    private bool CheckCollision()
    {
        Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.extents, Quaternion.identity);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(targetTag) && collider.gameObject == movingObject)
            {
                return true;
            }
        }

        return false;
    }

    public void SetBounds()
    {
        Debug.Log(gameObject.transform.position);
        bounds = new Bounds(Vector3.zero, boundsSize);
        SphereCollider collider = gameObject.AddComponent<SphereCollider>(); // SphereCollider 추가
        collider.center = gameObject.transform.position; // Bounds 중심을 Collider의 중심으로 설정
        collider.radius = boundsSize.magnitude / 2f; // Bounds 크기의 절반을 Collider 반지름으로 설정
        Debug.Log(gameObject.transform.position);
    }
}
