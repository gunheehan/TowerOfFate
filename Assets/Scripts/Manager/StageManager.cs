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
    private const int comparisonValue = 5;
    private PlayType currentPlayType = PlayType.Ready;
    
    private void Awake()
    {
        stageLevel = 0;
        floorLoader = new FloorLoader();
        Camera.main.AddComponent<CameraController>();
    }

    public void OnLoadNextStage()
    {
        currentPlayType = PlayType.Ready;
        stageLevel++;
        if(stageLevel % comparisonValue == 0 || stageLevel <= 1)
            SetFloor(stageLevel);
        ReadyStage();
    }

    private void SetFloor(int Lv)
    {
        int floorSize = 2 * Lv + 1;
        floorLoader.CreateFloor(floorSize);
        Camera.main.GetComponent<CameraController>().SetCameraPosition(floorSize);
    }

    private void ReadyStage()
    {
        UITimer UITimer = UIManager.Instance.GetUI<UITimer>() as UITimer;
        float timertime = GetPlayStateTiem();
        UITimer.SetTimer(timertime, PlayStage);
        UITimer.gameObject.SetActive(true);
    }

    private void PlayStage()
    {
        UITimer UITimer = UIManager.Instance.GetUI<UITimer>() as UITimer;
        float timertime = GetPlayStateTiem();
        UITimer.SetTimer(timertime, NextStage);
        //InvokeRepeating("CreateMonster",1f,3f);
        CreateMonster();
        currentPlayType = PlayType.Play;
    }

    private float GetPlayStateTiem()
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
