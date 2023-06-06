using UnityEngine;

public class TowerObject : MonoBehaviour
{
    private float power = 0f;
    public float Power => power;
    private int level = 1;
    public float Level => level;
    [SerializeField] private Transform TowerBase;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private Animator animator;
    private float shootSpeed = 3f;
    private FireMonster currentTarget = null;
    private GameObject boundsObject = null;

    private bool isinit = false;

    private void Awake()
    {
        power = 30f;
    }

    private void Start()
    {
        InvokeRepeating("Shoot", 0f, shootSpeed);
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            TowerBase.LookAt(currentTarget.transform);
        }
    }
    
    private void OnEnable()
    {
        TargetManager.Instance.TargetReceived += UpdataTarget;
    }

    private void OnDisable()
    {
        TargetManager.Instance.TargetReceived -= UpdataTarget;
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
        if (!currentTarget.gameObject.activeSelf)
            return;

        Bullet bullet = BulletManager.Instance.GetBullet();

        bullet.transform.position = shootPosition.position;
        
        bullet.SetBullet(currentTarget.transform,OnHitAction);
        
        animator.Play("Shoot");
    }

    private void OnHitAction()
    {
        currentTarget.TakeDamage(power);
    }
    
    private void UpdataTarget(IMonster target)
    {
        currentTarget = target.GetMonster();
    }

    private void InstantiateBounds()
    {
        boundsObject = new GameObject("BoundsObject");
        boundsObject.transform.parent = transform;
        
        boundsObject.AddComponent<BoundsController>().SetBounds();
    }

    public void UpgradeTower()
    {
        level++;
        power += power * level;
    }
}
