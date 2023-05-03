using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerItem : MonoBehaviour
{
    [SerializeField] private Button Btn_PlacedTower;
    [SerializeField] private Image Thumnail;
    private FloorController floorController = null;

    private TowerObject towerObject;
    // Start is called before the first frame update
    void Start()
    {
        Btn_PlacedTower.onClick.AddListener(OnClickPlacedTower);
    }

    private void OnClickPlacedTower()
    {
        bool setState = floorController.IsCanPlaced;
        if (setState)
        {
            Object obj = Instantiate(towerObject);
            TowerObject objectTower = obj as TowerObject;
            floorController.SetTower(objectTower);
        }

        towerObject.SetUsedState(setState);
    }

    public void SetItem(TowerObject towerPrefab, Sprite spritethumnail)
    {
        towerObject = towerPrefab;
        Thumnail.sprite = spritethumnail;
    }
}
