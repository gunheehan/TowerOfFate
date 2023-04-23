using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLoader : MonoBehaviour
{
    private GameObject floorPrefab; // 1x1 크기의 바닥 오브젝트 프리팹
    private int floorCount = 7; // 7x7 크기의 바닥 오브젝트를 생성
    
    // Start is called before the first frame update
    void Start()
    {
        floorPrefab = ObjectManager.Instance.GetObject<FloorObject>();
        floorPrefab.SetActive(false);
    }

    public void CreateFloor()
    {
        // 시작 좌표 (0, 0)
        Vector3 startPosition = Vector3.zero;

        // 7x7 바닥 오브젝트 생성
        for (int x = 0; x < floorCount; x++)
        {
            for (int z = 0; z < floorCount; z++)
            {
                Vector3 position = startPosition + new Vector3(x, 0, z);
                GameObject floorObject = Instantiate(floorPrefab, position, Quaternion.identity);
                floorObject.SetActive(true);
            }
        }
    }
}
