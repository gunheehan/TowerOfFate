using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLoader : MonoBehaviour
{
    private GameObject floorContents = null;
    private GameObject floorPrefab = null; // 1x1 크기의 바닥 오브젝트 프리팹

    public void CreateFloor(int floorLv)
    {
        Vector3 startPosition = Vector3.zero;

        int floorSize = floorLv - 2;

        for (int x = 0; x < floorLv; x++)
        {
            for (int z = 0; z < floorLv; z++)
            {
                Vector3 position = startPosition + new Vector3(x, 0, z);
                GameObject floorObject = Instantiate(GetFloorPrefab(), position, Quaternion.identity, floorContents.transform);
                
                if (x >= 1 && x <= floorSize && z >= 1 && z <= floorSize)
                    floorObject.GetComponent<Renderer>().material.color = Color.blue;
                else 
                    floorObject.GetComponent<Renderer>().material.color = Color.white;
                                        
                floorObject.SetActive(true);
            }
        }
    }

    private GameObject GetFloorPrefab()
    {
        if (floorPrefab != null)
            return floorPrefab;

        floorContents = new GameObject();
        floorContents.name = "FloorContents";
        
        floorPrefab = ObjectManager.Instance.GetObject<FloorObject>();
        floorPrefab.SetActive(false);

        return floorPrefab;
    }
}
