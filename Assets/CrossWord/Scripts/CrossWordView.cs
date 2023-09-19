using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossWordView : MonoBehaviour
{
    [SerializeField] private RectTransform RootRect = null;
    [SerializeField] private GridLayoutGroup LayoutGroup = null;

    [SerializeField] private WordItem wordItemPrefab;
    [SerializeField] private Transform itemContentsTr;

    private WordItem[,] itemMatrix = null;
    private Stack<WordItem> itemPool;

    private void Start()
    {
        itemPool = new Stack<WordItem>();
    }

    public void SetGridCellSize(int DPcount)
    {
        LayoutGroup.constraintCount = DPcount;

        int padding = ((int)LayoutGroup.spacing.x * (DPcount - 1)) + LayoutGroup.padding.left +
                      LayoutGroup.padding.right;
        int cellsize = ((int)RootRect.rect.width - padding) / DPcount;
        LayoutGroup.cellSize = new Vector2(cellsize, cellsize);
        
    }

    private void PushPoolItem()
    {
        foreach (WordItem item in itemMatrix)
        {
            itemPool.Push(item);
            item.gameObject.SetActive(false);
        }

        itemMatrix = null;
    }

    public void SetCellItem(int DPcount)
    {
        if (itemMatrix != null)
            PushPoolItem();
        
        itemMatrix = new WordItem[DPcount, DPcount];

        for (int row = 0; row < DPcount; row++)
        {
            for (int col = 0; col < DPcount; col++)
            {
                itemMatrix[row, col] = GetItemByPool();
                itemMatrix[row, col].SetItem($"{row}/{col}");
                itemMatrix[row, col].gameObject.SetActive(true);
            }
        }
    }

    private WordItem GetItemByPool()
    {
        WordItem newItem;
        if (itemPool.Count > 0)
        {
            newItem = itemPool.Pop();
            newItem.transform.SetAsLastSibling();
            return newItem;
        }

        newItem = Instantiate(wordItemPrefab, itemContentsTr);
        return newItem;
    }
}
