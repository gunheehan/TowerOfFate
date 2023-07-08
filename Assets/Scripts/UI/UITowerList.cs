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

    private FloorObject currentfloor = null;

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

    public void CloseUI()
    {
        gameObject.SetActive(false);
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
    }

    private void InstantiateTower(TowerData tower)
    {
        bool setState = currentfloor.IsCanPlaced;
        if (setState)
        {
            GameObject newTower = new GameObject("TowerBuilder");
            newTower.transform.position = transform.position;
            newTower.layer = LayerMask.NameToLayer("Tower");
            TowerBuilder towerBuilder = newTower.AddComponent<TowerBuilder>();
            towerBuilder.InitBuild(tower);
            //objectTower.SetTowerLevel(tower.Level);
            currentfloor.SetTowerBuilder(towerBuilder);
        }
    }

    public void SetCurrentFloor(FloorObject floor)
    {
        if(currentfloor != null)
            currentfloor.OnDeselected();
        currentfloor = floor;
    }
}
