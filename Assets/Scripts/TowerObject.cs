using UnityEngine;

public class TowerObject : MonoBehaviour
{
    private float power = 0f;

    private GameObject currentTarget = null;

    private void OnEnable()
    {
        TargetManager.Instance.TargetReceived += UpdataTarget;
    }

    private void OnDisable()
    {
        TargetManager.Instance.TargetReceived -= UpdataTarget;
    }

    private void Awake()
    {
        power = 10f;
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            transform.LookAt(currentTarget.transform);
        }
    }

    private void UpdataTarget(IMonster target)
    {
        currentTarget = target.GetMonster();
    }
}
