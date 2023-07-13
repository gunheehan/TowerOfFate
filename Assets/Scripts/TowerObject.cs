using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerObject : MonoBehaviour
{
    [SerializeField] private Transform TowerBase;
    [SerializeField] private Transform[] shootPosition;
    [SerializeField] private Animator animator;
    [SerializeField] private BoundsDetector boundsDetector;
    private List<GameObject> effectList = new List<GameObject>();
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

    private void FixedUpdate()
    {
        if (currentTarget != null)
        {
            Shoot();
        }
    }

    public void SetTowerData(TowerData towerdata)
    {
        power = towerdata.Power;
        InstantiateBounds(towerdata.AttackArea);
        SetEffectPrefab();
        //InvokeRepeating("Shoot", 0f, towerdata.Speed);
    }

    private void SetEffectPrefab()
    {
        foreach (Transform ShootPos in shootPosition)
        {
            GameObject particle = ParticlePool.instance.GetImpactObject(particleType.muzzzle,ShootPos);
            particle.transform.SetParent(ShootPos.transform);
            particle.SetActive(false);
            effectList.Add(particle);
        }
    }

    private void SetParticleActive(bool isActive)
    {
        foreach (GameObject obj in effectList)
        {
            obj.SetActive(isActive);
        }
    }
    
    private void Shoot()
    {
        if (currentTarget.GetMonster().activeSelf)
        {
            SetParticleActive(true);
        }
        else
        {
            SetParticleActive(false);
            return;
        }

        OnHitAction();
        
        // foreach (Transform ShootPos in shootPosition)
        // {
        //     Bullet bullet = BulletManager.Instance.GetBullet();
        //
        //     bullet.transform.position = ShootPos.position;
        //
        //     bullet.SetBullet(targetPos, OnHitAction);
        //
        //     animator.Play("Shoot");
        //     SetParticleActive(true);
        // }
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
