using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TowerListItem : MonoBehaviour
{
    [SerializeField] private Button Btn_PlacedTower;
    [SerializeField] private Image Thumnail;

    private Action towerPlacedAction = null;

    void Start()
    {
        Btn_PlacedTower.onClick.AddListener(OnClickPlacedTower);
    }

    private void OnClickPlacedTower()
    {
        towerPlacedAction?.Invoke();
    }

    public void SetItem(Sprite spritethumnail, Action TowerPlacedAcion)
    {
        Thumnail.sprite = spritethumnail;
        towerPlacedAction = TowerPlacedAcion;
    }
}
