using System;
using UnityEngine;
using UnityEngine.UI;

public class WordItem : MonoBehaviour
{
    public Action<WordItem> OnClickWordAction = null;
    [SerializeField] private Text Text_word;
    [SerializeField] private Button Btn_word;

    private CrossWordInfo.SingleWord wordData = new CrossWordInfo.SingleWord();
    public CrossWordInfo.SingleWord WordData => wordData;
    
    private void Start()
    {
        Btn_word.onClick.AddListener(OnClickWord);
    }
    private void OnClickWord()
    {
        Btn_word.image.color = Color.green;
        OnClickWordAction?.Invoke(this);
    }

    public bool IsAbleNewData(WordItemType dataType)
    {
        bool isAble = true;

        if (dataType == wordData.wordItemType)
            isAble = false;
        else if (wordData.wordItemType == WordItemType.CROSS)
            isAble = false;

        return isAble;
    }

    public void DeleteData(CrossWordInfo.GroupWord itemData)
    {
        if (itemData.wordItemType == WordItemType.COL)
        {
            wordData.ColGroup = null;
            if (itemData.wordItemType == WordItemType.CROSS)
                itemData.wordItemType = WordItemType.ROW;
            else
            {
                itemData.wordItemType = WordItemType.NONE;
                Text_word.text = String.Empty;
            }
        }
        else if (itemData.wordItemType == WordItemType.ROW)
        {
            wordData.RowGroup = null;
            if (itemData.wordItemType == WordItemType.CROSS)
                itemData.wordItemType = WordItemType.ROW;
            else
            {
                itemData.wordItemType = WordItemType.NONE;
                Text_word.text = String.Empty;
            }
        }
    }

    public void SetItem(int row, int col)
    {
        Text_word.text = String.Empty;
        wordData.word = '\0';
        wordData.rowIndex = row;
        wordData.colIndex = col;
        wordData.wordItemType = WordItemType.NONE;
    }

    public void SetItemData(char word, CrossWordInfo.GroupWord groupData)
    {
        Text_word.text = word.ToString();
        wordData.word = word;

        if (groupData.wordItemType == WordItemType.COL)
            wordData.ColGroup = groupData;
        else
            wordData.RowGroup = groupData;
        
        ChangeWordType(groupData.wordItemType);
    }

    private void ChangeWordType(WordItemType type)
    {
        if (wordData.wordItemType == WordItemType.NONE)
        {
            wordData.wordItemType = type;
        }
        else
        {
            wordData.wordItemType = WordItemType.CROSS;
        }
    }

    public int GetMatrixIndex(bool isrow)
    {
        if (isrow)
            return wordData.rowIndex;

        return wordData.colIndex;
    }

    public void DisSelect()
    {
        Btn_word.image.color = Color.white;
    }
}
