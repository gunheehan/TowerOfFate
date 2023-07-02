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
    private StageDate currentData;
    private CameraController cameraController;
    private PlayType currentPlayType = PlayType.Ready;
    public PlayType CurrentPlayType => currentPlayType;
    
    private void Awake()
    {
        stageLevel = 0;
        floorLoader = new FloorLoader();
        cameraController = Camera.main.AddComponent<CameraController>();
    }

    public void NextStage()
    {
        StageDate stagedata = CsvTableManager.Instance.GetData<StageDate>(TableType.Stage, stageLevel.ToString());
        currentData = stagedata;
        OnLoadNextStage();
        ReadyStage();
    }

    private void OnLoadNextStage()
    {
        if (currentData.FloorLevel != stageLevel)
        {
            roadtarget = null;
            stageLevel = currentData.FloorLevel;
            SetFloor(stageLevel);
        }
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
        float timertime = GetPlayStateTime(PlayType.Ready);
        UITimer.SetTimer(timertime, PlayStage);
    }

    private void PlayStage()
    {
        PlaySetting(false);
        UITimer UITimer = UIManager.Instance.GetUI<UITimer>() as UITimer;
        float timertime = GetPlayStateTime(PlayType.Play);
        UITimer.SetTimer(timertime, CheckCompleteStage);
       
        TargetManager.Instance.SetStageTarget(currentData.MonsterType, currentData.MonsterAmount);
    }
    
    private float GetPlayStateTime(PlayType currentPlayType)
    {
        float time = 0;
        switch (currentPlayType)
        {
            case PlayType.Ready:
                time = currentData.ReLoadTime;
                break;
            case PlayType.Play:
                time = currentData.PlayTime;
                break;
        }

        return time;
    }
    
    public void PlaySetting(bool isReady)
    {
        if (isReady)
            currentPlayType = PlayType.Ready;
        else
            currentPlayType = PlayType.Play;
        
        cameraController.SetLayerMask(CurrentPlayType);
        
        if (isReady)
            UIManager.Instance.GetUI<UITowerList>().OpenUI();
        else
            UIManager.Instance.GetUI<UITowerList>().ClostUI();
    }
    
    private void CheckCompleteStage()
    {
        if (TargetManager.Instance.GetMonsterCount() > 0)
        {
            Debug.Log("클리어 실패");
            return;
        }

        NextStage();
    }
}