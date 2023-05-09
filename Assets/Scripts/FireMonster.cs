using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMonster : Monster
{
    private void OnEnable()
    {
        roadTarget = StageManager.Instance.Roadtarget;
        transform.position = roadTarget[0];
        TargetManager.Instance.EnQueueTarget(this);
    }

    public override void Move()
    {
        if (currentMoveIndex < roadTarget.Count)
        {
            Vector3 targetPosition = roadTarget[currentMoveIndex];

            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(moveDirection);

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                currentMoveIndex++;
            }
        }
        else
        {
            currentMoveIndex = 0;
        }
    }

    public override void TakeDamage(int damage)
    {
        //데미지 부분
        HP -= damage;
        if (HP <= 0)
            TargetManager.Instance.PushTargetDictionary(MonsterPropertyType.Fire, this);
    }

    public override void SetPropertice(int HP)
    {
        base.HP = HP;
    }
}
