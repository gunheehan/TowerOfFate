using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayType
{
    Ready,
    Play
}

public class StageManager : Singleton<StageManager>
{
    private FloorLoader floorLoader = null;
    private List<Vector3> roadtarget = null;

    public List<Vector3> Roadtarget
    {
        get
        {
            if (roadtarget == null)
                roadtarget = floorLoader.GetRoadEdgeList();
            return roadtarget;
        }
        set
        {
            roadtarget = value;
        }
    }

    public int stageLevel { get; private set; }
    public int floorSize { get; private set; }
    private CameraController cameraController;
    private const int comparisonValue = 5;
    private PlayType currentPlayType = PlayType.Ready;
    public PlayType CurrentPlayType => currentPlayType;
    
    private void Awake()
    {
        stageLevel = 0;
        floorLoader = new FloorLoader();
        cameraController = Camera.main.AddComponent<CameraController>();
    }

    public void OnLoadNextStage()
    {
        stageLevel++;
        if(stageLevel % comparisonValue == 0 || stageLevel <= 1)
            SetFloor(stageLevel);
        ReadyStage();
    }

    private void SetFloor(int Lv)
    {
        floorSize = 2 * Lv + 1;
        floorLoader.CreateFloor(floorSize);
        cameraController.SetCameraPosition(floorSize);
    }

    private void ReadyStage()
    {
        PlaySetting(true);
        UITimer UITimer = UIManager.Instance.GetUI<UITimer>() as UITimer;
        float timertime = GetPlayStateTime();
        UITimer.SetTimer(timertime, PlayStage);
    }

    private void PlayStage()
    {
        PlaySetting(false);
        UITimer UITimer = UIManager.Instance.GetUI<UITimer>() as UITimer;
        float timertime = GetPlayStateTime();
        UITimer.SetTimer(timertime, NextStage);
        //InvokeRepeating("CreateMonster",1f,3f);
        CreateMonster();
    }

    private float GetPlayStateTime()
    {
        float time = 0;
        switch (currentPlayType)
        {
            case PlayType.Ready:
                time = 5f + 5f * stageLevel;
                break;
            case PlayType.Play:
                time = 10f + 10f * stageLevel;
                break;
        }

        return time;
    }

    private void PlaySetting(bool isReady)
    {
        if (isReady)
            currentPlayType = PlayType.Ready;
        else
            currentPlayType = PlayType.Play;
        
        cameraController.SetLayerMask(currentPlayType);

        UITowerList uiTowerList = UIManager.Instance.GetUI<UITowerList>() as UITowerList;
        uiTowerList.gameObject.SetActive(isReady);
    }

    private void CreateMonster()
    {
        TargetManager.Instance.InstantiateTarget(MonsterPropertyType.Fire);
    }

    private void NextStage()
    {
        if(CheckCompleteStage())
            OnLoadNextStage();
    }

    private bool CheckCompleteStage()
    {
        if (TargetManager.Instance.GetMonsterCount() > 0)
        {
            Debug.Log("클리어 실패");
            return false;
        }

        return true;
    }
}