using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    [SerializeField] private FloorLoader floorLoader;
    void Start()
    {
        UIManager.Instance.GetUI<UIMenu>().OpenUI();
    }
}
