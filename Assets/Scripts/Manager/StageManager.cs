using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    private FloorLoader floorLoader = null;
    
    public int stageLevel { get; private set; }

    private void Awake()
    {
        stageLevel = 0;
        floorLoader = new FloorLoader();
    }

    public void OnLoadStage(int stageLv)
    {
        SetFloor(stageLv);
        stageLevel = stageLv;
    }

    private void SetFloor(int Lv)
    {
        int floorSize = 2 * Lv + 1;
        floorLoader.CreateFloor(floorSize);
        SetCameraPosition(floorSize);
    }
    
    public void SetCameraPosition(int level)
    {
        // 바닥의 가운데 포지션을 구합니다.
        Vector3 floorCenter = new Vector3((level - 1) / 2f, 0f, (level - 1) / 2f);

        // 카메라가 바닥 위에 위치할 거리(distance)를 결정합니다.
        float cameraDistance = level;

        // 바닥의 크기를 기준으로 카메라가 바닥 위에서 얼마나 떨어져있어야 하는지 구합니다.
        float cameraHeight = Mathf.Max(level, 5f) * 0.5f / Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);

        // 카메라 위치와 각도를 설정합니다.
        Vector3 cameraPosition = new Vector3(floorCenter.x, cameraHeight * 2, floorCenter.z - cameraDistance);
        Quaternion cameraRotation = Quaternion.Euler(60f, 0f, 0f);

        // 카메라 위치 변경
        Camera.main.transform.position = cameraPosition;
        Camera.main.transform.rotation = cameraRotation;
    }
}
