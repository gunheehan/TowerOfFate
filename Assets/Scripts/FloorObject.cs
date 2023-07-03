using System;
using UnityEngine;

public class FloorObject : MonoBehaviour, ILayerInteraction
{
    private TowerObject playObject = null;

    private bool isCanPlaced = true;
    public bool IsCanPlaced => isCanPlaced;

    private UITowerList uiTowerList = null;
    private Material material;

    private void Start()
    {
        material = gameObject.GetComponent<Renderer>().material;
    }

    public bool SetTower(TowerObject towerObject)
    {
        if (!isCanPlaced)
            return isCanPlaced;

        playObject = towerObject;
        playObject.transform.position = this.transform.position;
        
        isCanPlaced = false;
        return isCanPlaced;
    }

    public void OnDeselected()
    {
        material.color = Color.blue;
    }

    public void ProcessLayerCollision()
    {
        if(uiTowerList == null)
            uiTowerList = UIManager.Instance.GetUI<UITowerList>() as UITowerList;
        uiTowerList.SetCurrentFloor(this);
        material.color = Color.red;
    }
}
