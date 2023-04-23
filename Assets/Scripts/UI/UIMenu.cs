using System;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour, IUIInterface
{
    [SerializeField] private Button Btn_Start;
    [SerializeField] private Button Btn_Preference;
    [SerializeField] private Button Btn_Logout;

    private Action playAction = null;

    private void Start()
    {
        Btn_Start.onClick.AddListener(()=>
        {
            playAction.Invoke();
            gameObject.SetActive(false);
        });
        Btn_Preference.onClick.AddListener(()=>Debug.Log("설정"));
        Btn_Logout.onClick.AddListener(()=>Debug.Log("종료"));
    }

    public void SetPlayAction(Action play)
    {
        playAction = play;
    }

    public void OpenUI()
    {
        Debug.Log("UI열림");
    }
}
