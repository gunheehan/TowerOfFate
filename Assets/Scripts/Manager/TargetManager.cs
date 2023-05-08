using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : Singleton<TargetManager>
{
    private Queue<FireMonster> monsterQueue = new Queue<FireMonster>();

    private static FireMonster currentTarget = null;
                      
    public delegate void TargetEventHandler(FireMonster target);
    
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

    public void EnQueueTarget(FireMonster target)
    {
        monsterQueue.Enqueue(target);
    }

    private void UpdataTarget(FireMonster target)
    {
        currentTarget = target;
        targetUpdateReceived?.Invoke(target);
    }
}
