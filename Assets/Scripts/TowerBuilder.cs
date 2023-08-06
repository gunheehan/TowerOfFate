using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour, ILayerInteraction
{
    private TowerObject currentTower = null;
    public TowerData CurrentTowerDB => currentTowerDB;
    private TowerData currentTowerDB;
    private TowerData nextTowerDB;
    private UITowerState uITowerState = null;
    private bool isMaxLv = false;

    public void InitBuild(TowerData initTowerDB)
    {
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        collider.size = Vector3.one;
        nextTowerDB = initTowerDB;
        UpdateTower();
        gameObject.SetActive(true);
    }

    private void InstantiateTower(TowerData towerDB)
    {
        if(currentTower != null)
            DestroyImmediate(currentTower.gameObject);
        GameObject obj = ObjectManager.Instance.GetObject(towerDB.name, this.transform);
        currentTower = obj.GetComponent<TowerObject>();
        currentTower.SetTowerData(towerDB);
    }

    public TowerData UpdateTower()
    {
        if (isMaxLv)
            return default;
        if (CoinWatcher.money < nextTowerDB.Price)
        {
            Debug.Log("We Don't Have a Money.\nPlease Show Me The Money");
            return default;
        }
        
        CoinWatcher.UpdateWallet(-nextTowerDB.Price);
        
        InstantiateTower(nextTowerDB);
        currentTowerDB = nextTowerDB;
        int nextLv = currentTowerDB.Level + 1;
        nextTowerDB = CsvTableManager.Instance.GetTowerData(nextLv.ToString(),(int)currentTowerDB.TowerType);
        if (nextTowerDB.Equals(default(TowerData)))
            isMaxLv = true;

        return currentTowerDB;
    }

    public void ProcessLayerCollision()
    {
        if(uITowerState == null) 
            uITowerState = UIManager.Instance.GetUI<UITowerState>() as UITowerState;
        
        uITowerState.SetTower(this);
    }
}
