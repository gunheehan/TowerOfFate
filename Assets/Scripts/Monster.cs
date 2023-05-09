using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [HideInInspector] public Animation anim;

    [HideInInspector] public List<Vector3> roadTarget;

    [HideInInspector] public float moveSpeed = 1f;

    [HideInInspector] public int currentMoveIndex = 0;

    [HideInInspector] public float HP = 100f;

    void Start()
    {
        anim = GetComponent<Animation>();
    }

    private void Update()
    {
        Move();
    }

    public abstract void Move();
    public abstract void TakeDamage(int damage);
    public abstract void SetPropertice(int HP);
}
