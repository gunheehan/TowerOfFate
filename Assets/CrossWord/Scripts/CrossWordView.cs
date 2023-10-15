using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossWordView : MonoBehaviour
{
    [SerializeField] private RectTransform RootRect = null;
    [SerializeField] private GridLayoutGroup gridLayoutGroup = null;

    [SerializeField] private WordItem wordItemPrefab;
    [SerializeField] private Transform itemContentsTr;

    private WordItem[,] itemMatrix = null;
    private Stack<WordItem> itemPool;

    private CrossWordModel wordModel = new CrossWordModel();
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

    private WordItem GetMatrixItem(int row, int col)
    {
        return itemMatrix[row, col];
    }

    public void SetNewQuestionData(string answer, string explantion, bool isrow)
    {
        int wordLengh = answer.Length;
        int startIndex = currentSeletItem.GetMatrixIndex(!isrow);
        int fixIndex = currentSeletItem.GetMatrixIndex(isrow);
        List<WordItem> currentSelectItemList = GetInputAreaWord(isrow, startIndex, startIndex + wordLengh, fixIndex);
        wordModel.GetNewQuestionData(answer, isrow, currentSelectItemList);
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
    }
}