using System;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour, IUIInterface
{
    [SerializeField] private Button Btn_Start;
    [SerializeField] private Button Btn_Preference;
    [SerializeField] private Button Btn_Logout;
    
    private void Start()
    {
        Btn_Start.onClick.AddListener(Onplay);
        Btn_Preference.onClick.AddListener(()=>Debug.Log("설정"));
        Btn_Logout.onClick.AddListener(()=>Debug.Log("종료"));
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }

    public void Onplay()
    {
        StageManager.Instance.OnLoadNextStage();
        UITimer UITimer = UIManager.Instance.GetUI<UITimer>() as UITimer;
        UITimer.SetTimer(61f);
        CloseUI();
    }
}
