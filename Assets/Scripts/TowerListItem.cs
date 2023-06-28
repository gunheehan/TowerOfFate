using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TowerListItem : MonoBehaviour
{
    [SerializeField] private Button Btn_PlacedTower;
    [SerializeField] private Text Text_name;
    [SerializeField] private Text Text_price;

    private TowerData data;
    private Action<TowerData> towerPlacedAction = null;

    void Start()
    {
        Btn_PlacedTower.onClick.AddListener(OnClickPlacedTower);
    }

    private void OnClickPlacedTower()
    {
        towerPlacedAction?.Invoke(data);
    }

    public void SetItem(TowerData towerdata, Action<TowerData> TowerPlacedAcion)
    {
        data = towerdata;
        towerPlacedAction = TowerPlacedAcion;
        Text_name.text = data.name;
        Text_price.text = data.Price.ToString();
    }
}
