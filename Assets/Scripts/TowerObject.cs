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
    [SerializeField] private Transform shootPosition;
    [SerializeField] private Animator animator;
    [SerializeField] private BoundsDetector boundsDetector;
    private FireMonster currentTarget = null;
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
            TowerBase.LookAt(currentTarget.transform);
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
        if (!currentTarget.gameObject.activeSelf)
            return;

        Bullet bullet = BulletManager.Instance.GetBullet();

        bullet.transform.position = shootPosition.position;
        
        bullet.SetBullet(currentTarget.transform,OnHitAction);
        
        animator.Play("Shoot");
    }

    private void OnHitAction()
    {
        currentTarget.TakeDamage(TowerData.power);
    }
    
    private void UpdataTarget(IMonster target)
    {
        currentTarget = target.GetMonster();
    }

    private void InstantiateBounds()
    {
        boundsDetector.SetBoundsSize(StageManager.Instance.floorSize,UpdataTarget);
    }

    public void UpgradeTower()
    {
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
