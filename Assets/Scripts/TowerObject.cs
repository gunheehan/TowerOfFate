using System;
using UnityEngine;

public class TowerObject : MonoBehaviour, ILayerInteraction
{
    private TowerData currentTowerData;
    public TowerData CurrentTowerData => currentTowerData;
    private TowerData NextTowerData;
    [SerializeField] private Transform TowerBase;
    [SerializeField] private Transform[] shootPosition;
    [SerializeField] private Animator animator;
    [SerializeField] private BoundsDetector boundsDetector;
    private IMonster currentTarget = null;
    private Transform targetPos;
    private GameObject boundsObject = null;
    private UITowerState uITowerState = null;

    private bool isinit = false;

    private void Update()
    {
        if (currentTarget != null)
        {
            TowerBase.LookAt(targetPos);
        }
    }

    public void SetTowerLevel(int level = 0)
    {
        currentTowerData = CsvTableManager.Instance.GetData<TowerData>(TableType.Tower,(level - 1).ToString());
        NextTowerData = CsvTableManager.Instance.GetData<TowerData>(TableType.Tower,level.ToString());
        
        InstantiateBounds();

        InvokeRepeating("Shoot", 0f, currentTowerData.Speed);
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
        currentTarget.TakeDamage(currentTowerData.Power);
    }
    
    private void UpdataTarget(GameObject target)
    {
        target.TryGetComponent<IMonster>(out currentTarget);
        targetPos = target.transform;
    }

    private void InstantiateBounds()
    {
        boundsDetector.SetBoundsSize(currentTowerData.AttackArea,UpdataTarget);
    }

    public void UpgradeTower()
    {
        if (CoinWatcher.money < NextTowerData.Price)
            return;
        
        CoinWatcher.UpdateWallet(-NextTowerData.Price);
        UpdateTower();
    }

    private void UpdateTower()
    {
        currentTowerData = NextTowerData;
        NextTowerData = CsvTableManager.Instance.GetData<TowerData>(TableType.Tower,currentTowerData.Level.ToString());
    }

    public void ProcessLayerCollision()
    {
        if(uITowerState == null) 
            uITowerState = UIManager.Instance.GetUI<UITowerState>() as UITowerState;
        
        uITowerState.SetTower(this);
    }
}
