using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private float speed = 5f;
    private float arrivalDistance = 0.1f;
    private Action onTargetReached;
    private bool isSet = false;
    
    private void Update()
    {
        if(isSet)
           Move(); 
    }

    private void OnDisable()
    {
        isSet = false;
    }

    private void Move()
    {
        if (target == null)
        {
            BulletManager.Instance.SpawnBullet(this);
            return;
        }            
            
        Vector3 direction = (target.position - transform.position).normalized;

        float distance = speed * Time.deltaTime;

        transform.Translate(direction * distance, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= arrivalDistance)
        {
            onTargetReached?.Invoke();
            BulletManager.Instance.SpawnBullet(this);
        }
    }

    public void SetBullet(Transform targetobj, Action arriveAction)
    {
        target = targetobj;
        onTargetReached = arriveAction;
        gameObject.SetActive(true);
        isSet = true;
    }
}
