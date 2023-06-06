using UnityEngine;

public class LayerInteractionController
{
    private UITowerList uiTowerList = null;
    private GameObject curSelectFloor = null;
    
    public void SetController()
    {
        uiTowerList = UIManager.Instance.GetUI<UITowerList>() as UITowerList;
    }

    public void ProcessLayerCollision(GameObject hitObj)
    {
        int hitLayer = hitObj.layer;

        if (LayerMask.NameToLayer("Floor") == hitLayer)
        {
            CheckFloorItem(hitObj.transform.gameObject);
        }
            
        else if (LayerMask.NameToLayer("Tower") == hitLayer)
        {
            CheckTowerItem(hitObj.transform.gameObject);
            Debug.Log("타워 오브젝트 충돌처리");
        }
    }
    
    private void CheckFloorItem(GameObject floor)
    {
        if(curSelectFloor != null)
            curSelectFloor.GetComponent<Renderer>().material.color = Color.blue;
        curSelectFloor = floor;
        floor.GetComponent<Renderer>().material.color = Color.red;
        
        FloorController floorController = floor.GetComponent<FloorController>();
        uiTowerList.SetCurrentFloor(floorController);
    }

    private void CheckTowerItem(GameObject tower)
    {
        UITowerState uITowerState = UIManager.Instance.GetUI<UITowerState>() as UITowerState;
        TowerObject towerObject = tower.GetComponent<TowerObject>();
        uITowerState.SetTower(towerObject);
    }
}
