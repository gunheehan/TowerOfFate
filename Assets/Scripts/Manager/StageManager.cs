using System.Collections;
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
    }

    public int stageLevel { get; private set; }

    private void Awake()
    {
        stageLevel = 0;
        floorLoader = new FloorLoader();
    }

    public void OnLoadStage(int stageLv)
    {
        SetFloor(3);
        stageLevel = stageLv;
    }

    private void SetFloor(int Lv)
    {
        int floorSize = 2 * Lv + 1;
        floorLoader.CreateFloor(floorSize);
        Camera.main.AddComponent<CameraController>().SetCameraPosition(floorSize);
    }
}
