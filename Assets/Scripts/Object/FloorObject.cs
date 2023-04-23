using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorObject : MonoBehaviour
{

    private Tower currentTower = null;

    public void SetTower(Tower tower)
    {
        currentTower = tower;
    }
    
    public void OpenStateUI()
    {
        if (currentTower == null)
            return;
        
        UIManager.Instance.GetUI<UITowerState>();

    }
}
