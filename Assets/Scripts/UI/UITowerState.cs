using System;
using UnityEngine;
using UnityEngine.UI;

public class UITowerState : MonoBehaviour, IUIInterface
{
    [SerializeField] private Text Text_name;
    [SerializeField] private Text Text_level;
    [SerializeField] private Text Text_power;

    [SerializeField] private Button Btn_Upgrade;
    [SerializeField] private Button Btn_Close;

    private TowerObject towerModel;

    private void Start()
    {
        Btn_Upgrade.onClick.AddListener(OnClickUpgrade);
        Btn_Close.onClick.AddListener(() => gameObject.SetActive(false));
    }

    public void SetTower(TowerObject towerObject)
    {
        towerModel = towerObject;
        SetData();
        gameObject.SetActive(true);
    }

    private void SetData()
    {
        Text_name.text = towerModel.name;
        Text_level.text = towerModel.CurrentTowerData.Level.ToString();
        Text_power.text = towerModel.CurrentTowerData.Power.ToString();
    }

    private void OnClickUpgrade()
    {
        towerModel.UpgradeTower();
        SetData();
    }
}
