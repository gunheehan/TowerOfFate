using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UITowerList : MonoBehaviour, IUIInterface
{
    private TowerObject TowerObjectPrefab = null;
    [SerializeField] private TowerItem towerItem;
    private Sprite sprite_thumbnail;

    private FloorController currentfloor = null;

    private bool isinit = false;
    public void OpenUI()
    {
        if (isinit)
            return;
        
        if (TowerObjectPrefab == null)
            SetTowerPrefab();
        else
            SetTowerList();
        
        isinit = false;
    }

    private void SetTowerPrefab()
    {
        GameObject tower = ObjectManager.Instance.GetObject<TowerObject>();
        TowerObjectPrefab = tower.GetComponent<TowerObject>();
        
        Texture2D thumbnailTexture = AssetPreview.GetAssetPreview(tower);
        sprite_thumbnail = Sprite.Create(thumbnailTexture, new Rect(0, 0, thumbnailTexture.width, thumbnailTexture.height), Vector2.zero);
        
        SetTowerList();
    }
    private void SetTowerList()
    {
        // 타워리스트를 어디선가 받아오던지 읽어오던지 동작 필요
        towerItem.SetItem(TowerObjectPrefab, sprite_thumbnail);
    }

    public void SetCurrentFloor(FloorController floor)
    {
        currentfloor = floor;
    }
}
