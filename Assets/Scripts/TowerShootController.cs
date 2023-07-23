using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class TowerShootController : MonoBehaviour
{
    private IMonster currentTarget;
    private Transform targetPos;

    private float power;
    private float attackSpeed;
    private Transform[] shootPos;
    private List<GameObject> effectList = new List<GameObject>();

    private bool isinit = false;
    private AttackType attackMode = AttackType.None;

    private void FixedUpdate()
    {
        if(attackMode == AttackType.Hit)
            Shoot();
    }

    public void SetShootData(float attackPower, AttackType attackType, float attackSpeed,Transform[] ShootPosition)
    {
        power = attackPower;
        this.attackSpeed = attackSpeed;
        shootPos = ShootPosition;
        attackMode = attackType;
        SetInit();
    }

    public void SetTarget([CanBeNull] IMonster monster)
    {
        currentTarget = monster;

        if (monster != null)
        {
            targetPos = currentTarget.GetMonster().transform;
            SetAttackMode();
        }
    }

    private void SetInit()
    {
        if(!isinit)
            SetEffectPrefab();

        isinit = true;
    }
    private void SetEffectPrefab()
    {
        if (shootPos == null)
            return;
        
        foreach (Transform ShootPos in shootPos)
        {
            GameObject particle = ParticlePool.instance.GetImpactObject(particleType.muzzzle,ShootPos);
            particle.transform.SetParent(ShootPos.transform);
            particle.SetActive(false);
            effectList.Add(particle);
        }
    }

    private void SetAttackMode()
    {
        if(attackSpeed > 0)
            InvokeRepeating("Shoot", 0f, attackSpeed);
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
        if (currentTarget == null || !currentTarget.GetMonster().activeSelf)
        {
            SetParticleActive(false);
            return;
        }
        switch (attackMode)
        {
            case AttackType.Hit:
                SetParticleActive(true);
                OnHitAction();
                break;
            case AttackType.Slow:
                SlowHitAction();
                break;
            case AttackType.Fire:
                break;
        }
    }
    private void OnHitAction()
    {
        currentTarget.TakeDamage(power, AttackType.Hit);
    }

    private void SlowHitAction()
    {
        foreach (Transform ShootPos in shootPos)
        {
            Bullet bullet = BulletManager.Instance.GetBullet();
            
            bullet.transform.position = ShootPos.position;
            
            bullet.SetBullet(targetPos, ()=>currentTarget.TakeDamage(power, AttackType.Slow));
        }
    }
}
