using System.Collections.Generic;
using UnityEngine;

public enum MonsterPropertyType
{
    None,
    Fire
}
public class TargetManager : Singleton<TargetManager>
{
    //private Queue<IMonster> monsterQueue = new Queue<IMonster>();

    private Dictionary<MonsterPropertyType, List<GameObject>> MonsterPooldic =
        new Dictionary<MonsterPropertyType, List<GameObject>>();
    

    private static List<GameObject> targetList = new List<GameObject>();
                      
    public delegate void TargetEventHandler(List<GameObject> targetList);
    
    private static TargetEventHandler targetUpdateReceived = null;
    public event TargetEventHandler TargetReceived
    {
        add
        {
            targetUpdateReceived += value;
            if(targetList != null)
                value?.Invoke(targetList);
        }
        remove { targetUpdateReceived -= value; }
    }

    public void EnQueueTarget(GameObject target)
    {
        targetList.Add(target);
        targetUpdateReceived?.Invoke(targetList);
    }

    public int GetMonsterCount()
    {
        return targetList.Count;
    }

    public void PushTargetDictionary(MonsterPropertyType monsterType, GameObject monster)
    {
        if(!MonsterPooldic.ContainsKey(monsterType))
            MonsterPooldic.Add(monsterType, new List<GameObject>());
        
        MonsterPooldic[monsterType].Add(monster);
        targetList.Remove(monster);

        if (invokeCount >= totalTargetCount && targetList.Count <= 0)
        {
            StageManager.Instance.NextStage();
        }
    }

    public void InstantiateTarget(MonsterPropertyType monsterType)
    {
        FireMonster FireMonster;
        switch (monsterType)
        {
            case MonsterPropertyType.None:
                FireMonster = ObjectManager.Instance.GetObject<FireMonster>().GetComponent<FireMonster>();
                FireMonster.SetMonsterProperty(1f);
                break;
            case MonsterPropertyType.Fire:
                FireMonster = ObjectManager.Instance.GetObject<FireMonster>().GetComponent<FireMonster>();
                FireMonster.SetMonsterProperty(1f);
                break;
        }
    }

    private int invokeCount;
    private int totalTargetCount;
    private MonsterPropertyType currentType;
    public void SetStageTarget(MonsterPropertyType monsterType, int monsterAmount)
    {
        invokeCount = 0;
        totalTargetCount = monsterAmount;
        currentType = monsterType;
        InvokeRepeating("InvokeAction", 0f, 1f);
    }

    private void InvokeAction()
    {
        // Invoke 실행할 동작

        invokeCount++;
        InstantiateTarget(currentType);
        if (invokeCount >= totalTargetCount)
        {
            CancelInvoke("InvokeAction");
        }
    }
}
