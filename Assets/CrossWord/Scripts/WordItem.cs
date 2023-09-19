using System;
using UnityEngine;

public class WordItem : MonoBehaviour
{
    public WordItemType ItemType = WordItemType.NONE;


    private void OnDisable()
    {
        ItemType = WordItemType.NONE;
    }
}
