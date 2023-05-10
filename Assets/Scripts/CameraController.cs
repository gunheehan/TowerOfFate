using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private RaycastHit rayHit;
    private Ray ray;
    UITowerList uiTowerList = null;

    private GameObject curSelectFloor = null;

    private void Start()
    {
        uiTowerList = UIManager.Instance.GetUI<UITowerList>() as UITowerList;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out rayHit, 50, 1<<LayerMask.NameToLayer("Floor")))
            {
                {
                    CheckPlaceItem(rayHit.transform.gameObject);
                    Debug.Log(rayHit.transform.name);
                }
                Debug.DrawLine(ray.origin,rayHit.point,Color.magenta);
            }
            else
            {
                Debug.DrawLine(ray.origin,rayHit.point,Color.cyan);
            }
        }
    }

    private void CheckPlaceItem(GameObject floor)
    {
        if(curSelectFloor != null)
            curSelectFloor.GetComponent<Renderer>().material.color = Color.blue;
        curSelectFloor = floor;
        floor.GetComponent<Renderer>().material.color = Color.red;
        
        FloorController floorController = floor.GetComponent<FloorController>();
        uiTowerList.SetCurrentFloor(floorController);
    }
    
    public void SetCameraPosition(int level)
    {
        // 바닥의 가운데 포지션을 구합니다.
        Vector3 floorCenter = new Vector3((level - 1) / 2f, 0f, (level - 1) / 2f);

        // 카메라가 바닥 위에 위치할 거리(distance)를 결정합니다.
        float cameraDistance = level;

        // 바닥의 크기를 기준으로 카메라가 바닥 위에서 얼마나 떨어져있어야 하는지 구합니다.
        float cameraHeight = cameraDistance / Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        
        // 카메라 위치와 각도를 설정합니다.
        Vector3 cameraPosition = new Vector3(floorCenter.x, cameraHeight, floorCenter.z - cameraDistance);
        Quaternion cameraRotation = Quaternion.Euler(60f, 0f, 0f);

        // 카메라 위치 변경
        Camera.main.transform.position = cameraPosition;
        Camera.main.transform.rotation = cameraRotation;
    }
}
