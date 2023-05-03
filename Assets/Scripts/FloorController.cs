using System.Collections;
using System.Collections.Generic;
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
        
        isCanPlaced = true;
        return isCanPlaced;
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
