using System;
using System.Collections.Generic;
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
    private TowerShootController towerShootController;

    private bool isDelayShoot = false;
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
        towerShootController = gameObject.AddComponent<TowerShootController>();
        towerShootController.SetShootData(towerdata.Power,towerdata.AttackType,towerdata.DelaySpeed,shootPosition);
        InstantiateBounds(towerdata.AttackArea);
    }

    private void UpdataTarget(GameObject target)
    {
        Debug.Log("FindMonster");
        target.TryGetComponent<IMonster>(out currentTarget);
        targetPos = target.transform;
        towerShootController.SetTarget(currentTarget);
    }

    private void OnLostTarget()
    {
        Debug.Log("LostMonster");
        towerShootController.SetTarget(null);
    }

    private void InstantiateBounds(int area)
    {
        boundsDetector.SetBoundsSize(area, UpdataTarget, OnLostTarget);
    }
}
