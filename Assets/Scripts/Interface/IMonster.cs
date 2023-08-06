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
    List<Vector3> roadPoint { get; set; }
    MonsterData monsterproperty { get; set; }

    void TakeDamage(float damage, AttackType type);
    void SetMonsterData(MonsterData monsterdata);
    GameObject GetMonster();
}