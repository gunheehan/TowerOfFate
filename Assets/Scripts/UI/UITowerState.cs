using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITowerState : MonoBehaviour, IUIInterface
{
    [SerializeField] private Button Btn_CheckPlaced;
    [SerializeField] private Button Btn_FinishPlaced;
    [SerializeField] private Button Btn_Monster;
    
    private Monster target = null;

    private void Start()
    {
        Btn_CheckPlaced.onClick.AddListener(OnClickCheckPlaced);
        Btn_FinishPlaced.onClick.AddListener(OnClickFinishPlaced);
        Btn_Monster.onClick.AddListener(OnClickMonster);
    }

    private void OnEnable()
    {
        TargetManager.Instance.TargetReceived += (value) => target = value;
    }

    private void OnClickCheckPlaced()
    {
        target.TakeDamage(100);
    }

    private void OnClickFinishPlaced()
    {
        StageManager.Instance.Roadtarget = null;
        StageManager.Instance.OnLoadNextStage();
    }

    private void OnClickMonster()
    {
        TargetManager.Instance.InstantiateTarget(MonsterPropertyType.Fire);
    }
}
