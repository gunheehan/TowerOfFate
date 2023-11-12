using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public event Action<WordItem[,]> OnItemSetting = null;
    public event Action<WordItem> OnClickItem = null;
    
    [SerializeField] private RectTransform RootRect = null;
    [SerializeField] private GridLayoutGroup gridLayoutGroup = null;
    [SerializeField] private WordItem wordItemPrefab;
    [SerializeField] private Transform itemContentsTr;
    
    private WordItem[,] itemMatrix = null;
    private Stack<WordItem> itemPool;
    
    private WordItem currentSeletItem = null;

    private void Start()
    {
        itemPool = new Stack<WordItem>();
    }
    
    public void SetGridCellSize(int DPcount)
    {
        gridLayoutGroup.constraintCount = DPcount;

        int padding = ((int)gridLayoutGroup.spacing.x * (DPcount - 1)) + gridLayoutGroup.padding.left +
                      gridLayoutGroup.padding.right;
        int cellsize = ((int)RootRect.rect.width - padding) / DPcount;
        gridLayoutGroup.cellSize = new Vector2(cellsize, cellsize);
    }

    public void InputNewItemData(CrossWordInfo.GroupWord itemData)
    {
        List<WordItem> itemList = null;
        if (itemData.wordItemType == WordItemType.ROW)
        {
            Debug.Log("가로 데이터 넣자");
            itemList = GetInputAreaWord(true, itemData.startCol, itemData.answer.Length + itemData.startCol, itemData.startRow);
        }
        else if (itemData.wordItemType == WordItemType.COL)
        {
            Debug.Log("세로 데이터 넣자");
            itemList = GetInputAreaWord(false, itemData.startRow, itemData.answer.Length + itemData.startRow, itemData.startCol);
        }
        
        for (int index = 0; index < itemList.Count; index++)
        {
            WordItem checkItem = itemList[index];
            checkItem.SetItemData(itemData.answer[index], itemData);
        }
    }

    public void DeleteNewItemData(CrossWordInfo.GroupWord itemData)
    {
        if (itemData == null)
            return;
        
        List<WordItem> itemList = null;
        if (itemData.wordItemType == WordItemType.ROW)
        {
            itemList = GetInputAreaWord(true, itemData.startCol, itemData.answer.Length + itemData.startCol, itemData.startRow);
        }
        else if (itemData.wordItemType == WordItemType.COL)
        {
            itemList = GetInputAreaWord(false, itemData.startRow, itemData.answer.Length + itemData.startRow, itemData.startCol);
        }
        
        for (int index = 0; index < itemList.Count; index++)
        {
            WordItem checkItem = itemList[index];
            checkItem.DeleteData(itemData);
        }
    }
    
    private List<WordItem> GetInputAreaWord(bool isrow, int startIndex, int endIndex, int fixIndex)
    {
        List<WordItem> currentSelectItemList = new List<WordItem>();
        
        for (int index = startIndex; index < endIndex; index++)
        {
            if (isrow)
                currentSelectItemList.Add(itemMatrix[fixIndex,index]);
            else
                currentSelectItemList.Add(itemMatrix[index,fixIndex]);
        }

        return currentSelectItemList;
    }
    
    #region MatrixItemSetting

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
                itemMatrix[row, col].SetItem(row, col);
                itemMatrix[row, col].gameObject.SetActive(true);
            }
        }
        OnItemSetting?.Invoke(itemMatrix);
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
        newItem.OnClickWordAction = SelectWordItem;
        return newItem;
    }

    #endregion
    
    private void SelectWordItem(WordItem SelectItem)
    {
        if(currentSeletItem != null)
            currentSeletItem.DisSelect();

        if (currentSeletItem == SelectItem)
            SelectItem = null;
        
        currentSeletItem = SelectItem;
        OnClickItem?.Invoke(SelectItem);
    }
}
