using UnityEngine;

public struct TowerData
{
    public int level;
    public float power;
    public float shootspeed;
}

public class TowerObject : MonoBehaviour, ILayerInteraction
{
    [HideInInspector] public TowerData TowerData;
    [SerializeField] private Transform TowerBase;
    [SerializeField] private Transform[] shootPosition;
    [SerializeField] private Animator animator;
    [SerializeField] private BoundsDetector boundsDetector;
    private IMonster currentTarget = null;
    private Transform targetPos;
    private GameObject boundsObject = null;
    private UITowerState uITowerState = null;

    private bool isinit = false;

    private void Awake()
    {
        TowerData = new TowerData()
        {
            level = 1,
            power = 30f,
            shootspeed = 3f
        };
    }

    private void Start()
    {
        InvokeRepeating("Shoot", 0f, TowerData.shootspeed);
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            TowerBase.LookAt(targetPos);
        }
    }

    public void Init()
    {
        if (isinit)
            return;

        InstantiateBounds();
        isinit = true;
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
        currentTarget.TakeDamage(TowerData.power);
    }
    
    private void UpdataTarget(IMonster target)
    {
        currentTarget = target;
        targetPos = currentTarget.GetMonster().transform;
    }

    private void InstantiateBounds()
    {
        boundsDetector.SetBoundsSize(StageManager.Instance.floorSize,UpdataTarget);
    }

    public void UpgradeTower()
    {
        if (CoinWatcher.money < 300)
            return;
        
        CoinWatcher.UpdateWallet(-300);
        TowerData.level++;
        TowerData.power += TowerData.power * TowerData.level;
    }

    public void ProcessLayerCollision()
    {
        if(uITowerState == null) 
            uITowerState = UIManager.Instance.GetUI<UITowerState>() as UITowerState;
        
        uITowerState.SetTower(this);
    }
}
