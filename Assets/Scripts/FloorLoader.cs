using System.Collections.Generic;
using UnityEngine;

public enum FloorType
{
    Road,
    Placement
}
public class FloorLoader : MonoBehaviour
{
    private GameObject floorContents = null;
    private GameObject floorPrefab = null; // 1x1 크기의 바닥 오브젝트 프리팹

    private List<FloorObject> floorControllerList = new List<FloorObject>();
    private List<Vector3> roadEdgeTransform = new List<Vector3>();
    private List<GameObject> floorList = new List<GameObject>();

    private Stack<GameObject> floorPool = new Stack<GameObject>();

    public List<FloorObject> GetFloorList()
    {
        return floorControllerList;
    }

    public List<Vector3> GetRoadEdgeList()
    {
        return roadEdgeTransform;
    }

    private void AllFloorPushPool()
    {
        foreach (GameObject floor in floorList)
        {
            floor.layer = 0;
            floor.name = "NotUsed";
            floor.GetComponent<Renderer>().material.color = Color.white;
            floor.SetActive(false);
            floorPool.Push(floor);
        }
        floorList.Clear();
        floorControllerList.Clear();
        roadEdgeTransform.Clear();
    }

    public void CreateFloor(int floorLv)
    {
        AllFloorPushPool();
        
        Vector3 startPosition = Vector3.zero;

        int floorSize = floorLv - 2;

        for (int x = 0; x < floorLv; x++)
        {
            for (int z = 0; z < floorLv; z++)
            {
                Vector3 position = startPosition + new Vector3(x, 0, z);

                if (x >= 1 && x <= floorSize && z >= 1 && z <= floorSize)
                    SetFloor(FloorType.Placement, position);
                else
                {
                    SetFloor(FloorType.Road, position);
                }
            }
        } 
        SetMonsterPath(floorLv);
    }

    private void SetFloor(FloorType floortype, Vector3 position)
    {
        GameObject floorObject = GetFloor(position);
        floorObject.name = "Floor " + position.x + " / " + position.z;

        if (floortype == FloorType.Placement)
        {
            floorObject.GetComponent<Renderer>().material.color = Color.blue;
            floorObject.layer = LayerMask.NameToLayer("Floor");
            floorControllerList.Add(floorObject.GetComponent<FloorObject>());
        }

        floorList.Add(floorObject);
        floorObject.transform.SetAsLastSibling();
        floorObject.SetActive(true);
    }

    private void SetMonsterPath(int floorSize)
    {
        int floorEndSize = floorSize - 1;
        
        roadEdgeTransform = new List<Vector3>
        {
            new Vector3(0,0,0),
            new Vector3(floorEndSize,0,0),
            new Vector3(floorEndSize,0,floorEndSize),
            new Vector3(0,0,floorEndSize)
        };
    }

    private GameObject GetFloor(Vector3 position)
    {
        GameObject floorObject;
        
        if (floorPrefab == null)
        {
            floorContents = new GameObject();
            floorContents.name = "FloorContents";
        
            floorPrefab = ObjectManager.Instance.GetObject("FloorObject");
            floorPrefab.SetActive(false);
        }

        if (floorPool.Count > 0)
        {
            floorObject = floorPool.Pop();
            floorObject.transform.position = position;
        }
        else
        {
            floorObject = Instantiate(floorPrefab, position, Quaternion.identity, floorContents.transform);
            floorObject.AddComponent<FloorObject>();
        }

        return floorObject;
    }
}
