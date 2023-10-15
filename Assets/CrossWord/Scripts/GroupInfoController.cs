using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroupInfoController : MonoBehaviour
{
    [SerializeField] private List<WordGroupItem> groupItem = null;

    private void Start()
    {
        DisAbleItem();
    }

    public void SetItemQuestionInfo(WordItem selectItem)
    {
        DisAbleItem();
        if (selectItem.WordData.ColGroup != null)
        {
            groupItem[0].SetItem(selectItem.WordData.ColGroup);
        }
        if (selectItem.WordData.RowGroup != null)
        {
            groupItem[1].SetItem(selectItem.WordData.RowGroup);
        }
    }

    private void DisAbleItem()
    {
        foreach (WordGroupItem item in groupItem)
        {
            item.gameObject.SetActive(false);
        } 
    }
}
