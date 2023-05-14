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
    

    private static IMonster currentTarget = null;
                      
    public delegate void TargetEventHandler(IMonster target);
    
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

    public void EnQueueTarget(IMonster target)
    {
        monsterQueue.Enqueue(target);
    }

    private void UpdataTarget(IMonster target)
    {
        currentTarget = target;
        targetUpdateReceived?.Invoke(target);
    }

    public void PushTargetDictionary(MonsterPropertyType monsterType, IMonster monster)
    {
        if(!MonsterPooldic.ContainsKey(monsterType))
            MonsterPooldic.Add(monsterType, new List<IMonster>());
        
        MonsterPooldic[monsterType].Add(monster);

        currentTarget = null;
    }

    public void InstantiateTarget(MonsterPropertyType monsterType)
    {
        switch (monsterType)
        {
            case MonsterPropertyType.None:
                break;
            case MonsterPropertyType.Fire:
                FireMonster FireMonster = ObjectManager.Instance.GetObject<FireMonster>().GetComponent<FireMonster>();
                FireMonster.SetMove(1f);
                break;
        }
    }
}
