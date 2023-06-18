using System.Collections.Generic;

public enum MonsterPropertyType
{
    None,
    Fire
}
public class TargetManager : Singleton<TargetManager>
{
    private Queue<IMonster> monsterQueue = new Queue<IMonster>();

    private Dictionary<MonsterPropertyType, List<IMonster>> MonsterPooldic =
        new Dictionary<MonsterPropertyType, List<IMonster>>();
    

    private static List<IMonster> targetList = new List<IMonster>();
                      
    public delegate void TargetEventHandler(List<IMonster> targetList);
    
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

    public void EnQueueTarget(IMonster target)
    {
        monsterQueue.Enqueue(target);
        targetList.Add(target);
        targetUpdateReceived?.Invoke(targetList);
    }

    public int GetMonsterCount()
    {
        return monsterQueue.Count;
    }

    public void PushTargetDictionary(MonsterPropertyType monsterType, IMonster monster)
    {
        if(!MonsterPooldic.ContainsKey(monsterType))
            MonsterPooldic.Add(monsterType, new List<IMonster>());
        
        MonsterPooldic[monsterType].Add(monster);
        targetList.Remove(monster);
    }

    public void InstantiateTarget(MonsterPropertyType monsterType)
    {
        switch (monsterType)
        {
            case MonsterPropertyType.None:
                break;
            case MonsterPropertyType.Fire:
                FireMonster FireMonster = ObjectManager.Instance.GetObject<FireMonster>().GetComponent<FireMonster>();
                FireMonster.SetMonsterProperty(1f);
                break;
        }
    }
}
