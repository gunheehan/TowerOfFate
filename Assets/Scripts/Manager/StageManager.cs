using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    private void Awake()
    {
        stageLevel = 0;
        floorLoader = new FloorLoader();
        Camera.main.AddComponent<CameraController>();
    }

    public void OnLoadNextStage()
    {
        stageLevel++;
        SetFloor(stageLevel);
    }

    private void SetFloor(int Lv)
    {
        int floorSize = 2 * Lv + 1;
        floorLoader.CreateFloor(floorSize);
        Camera.main.GetComponent<CameraController>().SetCameraPosition(floorSize);
    }

    public void OnPlayStage()
    {
        UITimer UITimer = UIManager.Instance.GetUI<UITimer>() as UITimer;
        UITimer.SetTimer(10f, NextStage);
        UITimer.gameObject.SetActive(true);
        //InvokeRepeating("CreateMonster",1f,3f);
        CreateMonster();
    }

    private void CreateMonster()
    {
        TargetManager.Instance.InstantiateTarget(MonsterPropertyType.Fire);
    }

    private void NextStage()
    {
        bool isComplete = CheckCompleteStage();
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
