using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class UITowerList : MonoBehaviour, IUIInterface
{
    [SerializeField] private TowerListItem towerItem;
    [SerializeField] private Transform contents;

    private List<TowerListItem> toweritemList = new List<TowerListItem>();
    private Stack<TowerListItem> toweritemPool = new Stack<TowerListItem>();
    private Sprite sprite_thumbnail;

    private FloorController currentfloor = null;

    private bool isinit = false;

    private void Start()
    {
        SetTowerList();
    }

    private void OnEnable()
    {
        SetTowerList();
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
    }

    private void AllPushPoolItem()
    {
        foreach (TowerListItem item in toweritemList)
        {
            toweritemPool.Push(item);
            item.gameObject.SetActive(false);
        }
        toweritemList.Clear();
    }

    private void SetTowerList()
    {
        AllPushPoolItem();
        
        List<TowerData> towerData = CsvTableManager.Instance.GetData<List<TowerData>>(TableType.Tower,null);

        foreach (TowerData data in towerData)
        {
            Debug.Log(data.name);
            //GameObject tower = ObjectManager.Instance.GetObject(data.name);

            TowerListItem towerpreitem;
            if (toweritemPool.Count > 0)
                towerpreitem = toweritemPool.Pop();
            else
                towerpreitem = Instantiate(towerItem,contents);
            
            toweritemList.Add(towerpreitem);
            towerpreitem.SetItem(data,InstantiateTower);
            towerpreitem.transform.SetAsLastSibling();
            towerpreitem.gameObject.SetActive(true);
        }
        //GameObject tower = ObjectManager.Instance.GetObject("NT_1");
        
        //Texture2D thumbnailTexture = AssetPreview.GetAssetPreview(tower);
        //sprite_thumbnail = Sprite.Create(thumbnailTexture, new Rect(0, 0, thumbnailTexture.width, thumbnailTexture.height), Vector2.zero);
        //tower.SetActive(false);
        
        //SetTowerList();
    }
    // private void SetTowerList()
    // {
    //     // 타워리스트를 어디선가 받아오던지 읽어오던지 동작 필요
    //     towerItem.SetItem(sprite_thumbnail, InstantiateTower);
    // }

    private void InstantiateTower(TowerData tower)
    {
        bool setState = currentfloor.IsCanPlaced;
        if (setState)
        {
            GameObject obj = ObjectManager.Instance.GetObject(tower.name);
            TowerObject objectTower = obj.GetComponent<TowerObject>();
            objectTower.Init();
            objectTower.SetTowerLevel(tower.Level);
            currentfloor.SetTower(objectTower);
            objectTower.gameObject.SetActive(true);
        }
    }

    public void SetCurrentFloor(FloorController floor)
    {
        if(currentfloor != null)
            currentfloor.OnDeselected();
        currentfloor = floor;
    }
}
