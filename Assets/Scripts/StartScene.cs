using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.OpenUI<UIMenu>().SetUI();
    }
}
