using System;
using System.Collections;
using UnityEngine;

public class FireMonster : MonoBehaviour, IMonster
{
    public int currentMoveIndex { get; set; }
    public Monsterproperty monsterproperty { get; set; }
    [SerializeField] private UIHpbar uihpbar;
    private float HP = 100f;
    private float moveSpeed;

    private bool isinit = false;
    private bool isMoveDebuff = false;

    private void OnEnable()
    {
        uihpbar = UIManager.Instance.GetMultiUI<UIHpbar>() as UIHpbar;
        uihpbar.SetMonsterTransform(transform);
        uihpbar.SetHpbar(HP);
        uihpbar.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        isinit = false;
        HP = 100;
        UIManager.Instance.PushMultiUI<UIHpbar>(uihpbar);
    }

    private void Update()
    {
        if(isinit)
            Move();
    }

    public void Move()
    {
        if (currentMoveIndex < monsterproperty.roadPoint.Count)
        {
            Vector3 targetPosition = monsterproperty.roadPoint[currentMoveIndex];

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

    public void TakeDamage(float damage, AttackType type)
    {
        HP -= damage;
        UpdateMonsterState();

        switch (type)
        {
            case AttackType.Slow:
                DebufferMove();
                break;
            case AttackType.Fire:
                invokeDamage = damage;
                ContinueDamaged(damage);
                break;
        }
    }
    
    private void DebufferMove()
    {
        if (isMoveDebuff)
            return;
        SetMoveSpeed(true);
        StartCoroutine(CourutineMoveSlow());
    }

    private IEnumerator CourutineMoveSlow()
    {
        yield return new WaitForSeconds(2f);
        SetMoveSpeed(false);
    }

    private void SetMoveSpeed(bool isSlow)
    {
        if (isSlow)
            moveSpeed *= .5f;
        else
            moveSpeed = monsterproperty.speed;

        isMoveDebuff = isSlow;
        
        Debug.Log("MoveSet : " + moveSpeed);
    }

    private float invokeDamage;
    private void ContinueDamaged(float damage)
    {
        CancelInvoke("DamageCourutine");
        InvokeRepeating("DamageCourutine", 1f, 5f);
    }

    private void DamageCourutine()
    {
        HP -= invokeDamage;
        UpdateMonsterState();
    }

    private void UpdateMonsterState()
    {
        uihpbar.UpdateHP(HP);
        if (HP <= 0)
        {
            TargetManager.Instance.PushTargetDictionary(MonsterPropertyType.Fire, this.gameObject);
            gameObject.SetActive(false);
            CoinWatcher.UpdateWallet(200);
        }
    }

    public void SetMonsterProperty(float speed)
    {
        monsterproperty = new Monsterproperty()
        {
            roadPoint = StageManager.Instance.Roadtarget,
            speed = speed
        };
        gameObject.SetActive(true);
        transform.position = monsterproperty.roadPoint[0];
        moveSpeed = monsterproperty.speed;
        TargetManager.Instance.EnQueueTarget(this.gameObject);

        isinit = true;
    }

    public GameObject GetMonster()
    {
        return gameObject;
    }
}
