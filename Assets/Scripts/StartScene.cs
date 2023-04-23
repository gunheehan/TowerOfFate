using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    [SerializeField] private FloorLoader floorLoader;
    void Start()
    {
        UIMenu uiMenu = UIManager.Instance.GetUI<UIMenu>() as UIMenu;
        uiMenu.SetPlayAction(() =>
        {
            floorLoader.CreateFloor();
        });
        uiMenu.OpenUI();
    }
}
