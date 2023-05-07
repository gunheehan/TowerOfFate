using UnityEngine;

public class FloorController : MonoBehaviour
{
    private TowerObject playObject = null;

    private bool isCanPlaced = true;
    public bool IsCanPlaced => isCanPlaced;

    public bool SetTower(TowerObject towerObject)
    {
        if (!isCanPlaced)
            return isCanPlaced;

        playObject = towerObject;
        playObject.transform.position = this.transform.position;
        
        isCanPlaced = false;
        return isCanPlaced;
    }

    public void SetTowerList()
    {
        UITowerList uiTowerList = UIManager.Instance.GetUI<UITowerList>() as UITowerList;
        uiTowerList.SetCurrentFloor(this);
        uiTowerList.OpenUI();
    }

    public void CanPlaceOnFloor()
    {
        if(playObject == null)
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        else
            gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void FinishPlaceFloor()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }
}
