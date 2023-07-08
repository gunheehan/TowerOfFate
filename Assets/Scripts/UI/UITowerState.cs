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

    private TowerBuilder towerBuilder;

    private void Start()
    {
        Btn_Upgrade.onClick.AddListener(OnClickUpgrade);
        Btn_Close.onClick.AddListener(() => gameObject.SetActive(false));
    }

    public void SetTower(TowerBuilder towerObject)
    {
        towerBuilder = towerObject;
        SetData(towerObject.CurrentTowerDB);
        gameObject.SetActive(true);
    }

    private void SetData(TowerData towerdb)
    {
        if (towerdb.Equals(default(TowerData)))
            return;
        
        Text_name.text = towerdb.name;
        Text_level.text = towerdb.Level.ToString();
        Text_power.text = towerdb.Power.ToString();
    }

    private void OnClickUpgrade()
    {
        TowerData upGrageTowerDB = towerBuilder.UpdateTower();
        SetData(upGrageTowerDB);
    }
}
