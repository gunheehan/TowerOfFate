using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterPropertyType
{
    None,
    Fire
}
public class TargetManager : Singleton<TargetManager>
{
    private Queue<Monster> monsterQueue = new Queue<Monster>();

    private Dictionary<MonsterPropertyType, List<Monster>> MonsterPooldic =
        new Dictionary<MonsterPropertyType, List<Monster>>();
    

    private static Monster currentTarget = null;
                      
    public delegate void TargetEventHandler(Monster target);
    
    private static TargetEventHandler targetUpdateReceived = null;
    public event TargetEventHandler TargetReceived
    {
        add
        {
            targetUpdateReceived += value;
            if(currentTarget != null)
                value?.Invoke(currentTarget);
        }
        remove { targetUpdateReceived -= value; }
    }

    void Update()
    {
        if (monsterQueue.Count > 0 && currentTarget == null)
        {
            UpdataTarget(monsterQueue.Dequeue());
        }
    }

    public void EnQueueTarget(Monster target)
    {
        monsterQueue.Enqueue(target);
    }

    private void UpdataTarget(Monster target)
    {
        currentTarget = target;
        targetUpdateReceived?.Invoke(target);
    }

    public void PushTargetDictionary(MonsterPropertyType monsterType, Monster monster)
    {
        if(!MonsterPooldic.ContainsKey(monsterType))
            MonsterPooldic.Add(monsterType, new List<Monster>());
        
        monster.gameObject.SetActive(false);
        MonsterPooldic[monsterType].Add(monster);

        currentTarget = null;
    }
}
