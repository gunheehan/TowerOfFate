using System;
using System.Collections.Generic;
using UnityEngine;

public class QuizGroupManager : MonoBehaviour
{
    public event Action<CrossWordInfo.GroupWord> InputNewQuiz = null;
    public event Action<CrossWordInfo.GroupWord[]> ShowNewQuizData = null;
    public event Action<CrossWordInfo.GroupWord> DeletQuizData = null;
    
    private WordItem[,] itemMatrix = null;
    private CrossWordModel wordModel;
    private WordItem currentSeletItem = null;
    private List<CrossWordInfo.GroupWord> currentGroupList;
    private Dictionary<string, CrossWordInfo.GroupWord> wordsQuestionDic = new Dictionary<string, CrossWordInfo.GroupWord>();

    private void Start()
    {
        wordModel = new CrossWordModel();
        currentGroupList = new List<CrossWordInfo.GroupWord>();
    }

    public void SetItemMatrix(WordItem[,] matrix)
    {
        itemMatrix = matrix;
    }

    public void SetSeletcItem(WordItem newItem)
    {
        currentGroupList.Clear();
        currentSeletItem = newItem;
        CrossWordInfo.GroupWord newwordData;
        if (currentSeletItem.WordData.ColGroup != null)
        {
            if (wordsQuestionDic.TryGetValue(currentSeletItem.WordData.ColGroup.answer, out newwordData))
            {
                currentGroupList.Add(newwordData);
            }
        }

        if (currentSeletItem.WordData.RowGroup != null)
        {
            if (wordsQuestionDic.TryGetValue(currentSeletItem.WordData.RowGroup.answer, out newwordData))
            {
                currentGroupList.Add(newwordData);
            }
        }

        ShowNewQuizData?.Invoke(currentGroupList.ToArray());
    }

    public void AddNewQuizData(string answer, string explantion, bool isrow)
    {
        Debug.Log("Insert New Data : " + answer);
        if (wordsQuestionDic.ContainsKey(answer))
        {
            Debug.Log("Already answer");
            return;
        }
        WordItemType type = isrow ? WordItemType.ROW : WordItemType.COL;

        if (!currentSeletItem.IsAbleNewData(type))
        {
            Debug.Log("Item Select Error");
            return;
        }

        int wordLengh = answer.Length;
        int startIndex = currentSeletItem.GetMatrixIndex(!isrow);
        int fixIndex = currentSeletItem.GetMatrixIndex(isrow);
        List<WordItem> currentSelectItemList = GetInputAreaWord(isrow, startIndex, startIndex + wordLengh, fixIndex);

        bool isAbleNewQuiz = wordModel.CheckAbleInputNewQuiz(answer, currentSelectItemList, type);

        if (!isAbleNewQuiz)
        {
            Debug.Log("Available Error");
            return;
        }
        
        CrossWordInfo.GroupWord newWordGroup = new CrossWordInfo.GroupWord()
        {
            answer = answer,
            explanation = explantion,
            startCol = currentSeletItem.WordData.colIndex,
            startRow = currentSeletItem.WordData.rowIndex,
            wordItemType = type
        };
        
        wordsQuestionDic.Add(answer, newWordGroup);
        
        InputNewQuiz?.Invoke(newWordGroup);
        
        Debug.Log("Success new Item Insert");
    }

    public void DeleteQuizData(string answer)
    {
        if (!wordsQuestionDic.ContainsKey(answer))
        {
            Debug.Log("Quiz Dictionary Not Placed");
            return;
        }
        DeletQuizData?.Invoke(wordsQuestionDic[answer]);
        wordsQuestionDic.Remove(answer);
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
}
