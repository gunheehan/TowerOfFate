using System.Collections.Generic;
using UnityEngine;

public struct Monsterproperty
{
    public List<Vector3> roadPoint;
    public float speed;
}

public interface IMonster
{
    int currentMoveIndex { get; set; }
    Monsterproperty monsterproperty { get; set; }

    void TakeDamage(float damage, AttackType type);
    void SetMonsterProperty(float speed);
    GameObject GetMonster();
}