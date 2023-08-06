using System.Collections.Generic;
using UnityEngine;

public class TargetManager : Singleton<TargetManager>
{
    //private Queue<IMonster> monsterQueue = new Queue<IMonster>();

    private Dictionary<MonsterPropertyType, Stack<GameObject>> MonsterPooldic =
        new Dictionary<MonsterPropertyType, Stack<GameObject>>();
    

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
            MonsterPooldic.Add(monsterType, new Stack<GameObject>());
        
        MonsterPooldic[monsterType].Push(monster);
        targetList.Remove(monster);

        if (invokeCount >= totalTargetCount && targetList.Count <= 0)
        {
            StageManager.Instance.NextStage();
        }
    }

    private GameObject GetMonsterObject(MonsterPropertyType monsterType, string prefabName)
    {
        GameObject monsterObj;

        if (MonsterPooldic.ContainsKey(monsterType))
        {
            if (MonsterPooldic[monsterType].Count > 0)
            {
                monsterObj = MonsterPooldic[monsterType].Pop();
                targetList.Add(monsterObj);
                return monsterObj;
            }
        }

        monsterObj = ObjectManager.Instance.GetObject(prefabName);

        EnQueueTarget(monsterObj);
        return monsterObj;
    }

    public void InstantiateTarget(MonsterPropertyType monsterType)
    {
        IMonster Imonster;

        MonsterData monsterDB;

        monsterDB = CsvTableManager.Instance.GetMonsterDB(monsterType);

        GameObject monsterObj = GetMonsterObject(monsterType, monsterDB.prefabName);
        monsterObj.TryGetComponent<IMonster>(out Imonster);
        
        if(Imonster != null)
            Imonster.SetMonsterData(monsterDB);
        else
            Debug.Log("Monster Type Setting Error");
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
