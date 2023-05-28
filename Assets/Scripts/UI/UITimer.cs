using System;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour, IUIInterface
{
    [SerializeField] private Text Text_Timer;
    public float timeInSeconds = 0f;

    private bool isSetting = false;
    private Action endAction = null;

    private void OnDisable()
    {
        timeInSeconds = 0f;
        isSetting = false;
    }

    public void SetTimer(float time, Action EndStageAction = null)
    {
        timeInSeconds = time;
        endAction = EndStageAction;
        this.gameObject.SetActive(true);
        
        isSetting = true;
    }

    void Update()
    {
        if (isSetting)
            PlayTimer();
    }

    private void PlayTimer()
    {
        timeInSeconds -= Time.deltaTime; // 경과 시간 업데이트

        int minutes = Mathf.FloorToInt(timeInSeconds / 60); // 분 계산
        int seconds = Mathf.FloorToInt(timeInSeconds % 60); // 초 계산

        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds); // 형식 지정

        Text_Timer.text = timeString;

        if (timeInSeconds <= 0)
        {
            isSetting = false;
            endAction?.Invoke();
        }
    }
}
