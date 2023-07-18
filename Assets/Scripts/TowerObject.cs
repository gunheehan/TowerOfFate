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

    private bool isDelayShoot = false;
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
        if (currentTarget != null && !isDelayShoot)
        {
            Shoot();
        }
    }

    public void SetTowerData(TowerData towerdata)
    {
        isDelayShoot = false;
        power = towerdata.Power;
        InstantiateBounds(towerdata.AttackArea);
        SetEffectPrefab();
        if (towerdata.DaleySpeed > 0)
        {
            InvokeRepeating("Shoot", 0f, towerdata.DaleySpeed);
            isDelayShoot = true;
        }
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
        if (!currentTarget.GetMonster().activeSelf)
        {
            return;
        }
        if (!isDelayShoot)
        {
            OnHitAction();
        }
        else
        {
            foreach (Transform ShootPos in shootPosition)
            {
                Bullet bullet = BulletManager.Instance.GetBullet();
            
                bullet.transform.position = ShootPos.position;
            
                bullet.SetBullet(targetPos, OnHitAction);
            
                animator.Play("Shoot");
            }
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
        if(!isDelayShoot)
            SetParticleActive(true);
    }

    private void OnLostTarget()
    {
        SetParticleActive(false);
    }

    private void InstantiateBounds(int area)
    {
        boundsDetector.SetBoundsSize(area, UpdataTarget, OnLostTarget);
    }
}
