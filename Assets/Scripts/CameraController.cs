using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private RaycastHit rayHit;
    private Ray ray;
    
    private int floorLayerMask;
    private int towerLayerMask;

    private int layerMask;

    private LayerInteractionController layerInteractionController;

    private void Start()
    {
        layerInteractionController = new LayerInteractionController();
        layerInteractionController.SetController();
        floorLayerMask = 1 << LayerMask.NameToLayer("Floor");
        towerLayerMask = 1 << LayerMask.NameToLayer("Tower");
        SetLayerMask(PlayType.Ready);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ProcessLayerRaycast();
        }
    }

    public void SetLayerMask(PlayType type)
    {
        Debug.Log(type.ToString());
        switch (type)
        {
            case PlayType.Ready:
                layerMask = floorLayerMask | towerLayerMask;
                break;
            case PlayType.Play:
                layerMask = towerLayerMask;
                break;
        }
    }

    private void ProcessLayerRaycast()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out rayHit, 50, layerMask))
        {
            ILayerInteraction LayerInteraction = rayHit.collider.gameObject.GetComponent<ILayerInteraction>();
            if(LayerInteraction != null)
                LayerInteraction.ProcessLayerCollision();
            //layerInteractionController.ProcessLayerCollision(rayHit.collider.gameObject);
            Debug.DrawLine(ray.origin, rayHit.point, Color.magenta);
        }
        else
        {
            Debug.DrawLine(ray.origin,rayHit.point,Color.cyan);
        }
    }

    
    
    public void SetCameraPosition(int floorSise)
    {
        // 바닥의 가운데 포지션을 구합니다.
        Vector3 floorCenter = new Vector3((floorSise - 1) / 2f, 0f, (floorSise - 1) / 2f);

        // 카메라가 바닥 위에 위치할 거리(distance)를 결정합니다.
        float cameraDistance = floorSise;

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
