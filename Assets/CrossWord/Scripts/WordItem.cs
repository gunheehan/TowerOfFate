using System;
using Unity.VisualScripting;
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

    public void SetItem(int row, int col)
    {
        Text_word.text = String.Empty;
        wordData.word = '\0';
        wordData.rowIndex = row;
        wordData.colIndex = col;
        wordData.wordItemType = WordItemType.NONE;
    }

    public void SetItemData(char word, WordItemType type)
    {
        Text_word.text = word.ToString();
        wordData.word = word;
        ChangeWordType(type);
    }

    private void ChangeWordType(WordItemType type)
    {
        if (wordData.wordItemType == WordItemType.NONE)
        {
            wordData.wordItemType = type;
            return;
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
