using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UITowerState : MonoBehaviour, IUIInterface
{
    [SerializeField] private Button Btn_CheckPlaced;
    [SerializeField] private Button Btn_FinishPlaced;
    [SerializeField] private Button Btn_Monster;

    private List<FloorController> floorControllerList;

    private void Start()
    {
        Btn_CheckPlaced.onClick.AddListener(OnClickCheckPlaced);
        Btn_FinishPlaced.onClick.AddListener(OnClickFinishPlaced);
        Btn_Monster.onClick.AddListener(OnClickMonster);
    }

    public void SetFloorController(List<FloorController> floorControllers)
    {
        floorControllerList = floorControllers;
    }

    private void OnClickCheckPlaced()
    {
        foreach (FloorController floorController in floorControllerList)
        {
            floorController.CanPlaceOnFloor();
        }
    }

    private void OnClickFinishPlaced()
    {
        foreach (FloorController floorController in floorControllerList)
        {
            floorController.FinishPlaceFloor();
        }
    }

    private void OnClickMonster()
    {
        ObjectManager.Instance.GetObject<FireMonster>();
    }
}
