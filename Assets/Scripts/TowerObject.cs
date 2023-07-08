using System;
using UnityEngine;

public class TowerObject : MonoBehaviour
{
    [SerializeField] private Transform TowerBase;
    [SerializeField] private Transform[] shootPosition;
    [SerializeField] private Animator animator;
    [SerializeField] private BoundsDetector boundsDetector;
    private IMonster currentTarget = null;
    private Transform targetPos;
    private GameObject boundsObject = null;
    private UITowerState uITowerState = null;
    private float power;

    private bool isinit = false;

    private void Update()
    {
        if (currentTarget != null)
        {
            TowerBase.LookAt(targetPos);
        }
    }

    public void SetTowerData(TowerData towerdata)
    {
        power = towerdata.Power;
        InstantiateBounds(towerdata.AttackArea);
        InvokeRepeating("Shoot", 0f, towerdata.Speed);
    }
    
    private void Shoot()
    {
        if (currentTarget == null)
            return;
        if (!currentTarget.GetMonster().activeSelf)
            return;

        foreach (Transform ShootPos in shootPosition)
        {
            Bullet bullet = BulletManager.Instance.GetBullet();

            bullet.transform.position = ShootPos.position;
        
            bullet.SetBullet(targetPos, OnHitAction);
        
            animator.Play("Shoot");
        }
    }

    private void OnHitAction()
    {
        currentTarget.TakeDamage(power);
    }
    
    private void UpdataTarget(GameObject target)
    {
        target.TryGetComponent<IMonster>(out currentTarget);
        targetPos = target.transform;
    }

    private void InstantiateBounds(int area)
    {
        boundsDetector.SetBoundsSize(area, UpdataTarget);
    }
}
