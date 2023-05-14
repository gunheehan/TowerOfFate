using System.Collections.Generic;
using UnityEngine;

public struct Monsterproperty
{
    public List<Vector3> roadPoint;
    public float speed;
}

public interface IMonster
{
    Animation anim { get; }
    int currentMoveIndex { get; set; }
    Monsterproperty monsterproperty { get; set; }

    void Move();
    void TakeDamage(int damage);
    void SetMove(float speed);
    GameObject GetMonster();
}