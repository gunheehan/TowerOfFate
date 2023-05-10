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
}
