using System.Collections.Generic;
using UnityEngine;

public class CrossWordModel
{
    public bool CheckNewQuestion(List<WordItem> inputPosWordItems, string answer, WordItemType newType)
    {
        for (int index = 0; index < inputPosWordItems.Count; index++)
        {
            WordItem checkItem = inputPosWordItems[index];
            
            if (checkItem.WordData.wordItemType == newType)
            {
                Debug.Log("Type Duplicate And AlReayData");
                return false;
            }

            if (checkItem.WordData.word != '\0')
            {
                char alreadyWord = checkItem.WordData.word;
                Debug.Log("AlReady Word. Check Word Equals");
                Debug.Log("AlReady Word : " + alreadyWord);
                Debug.Log("Check Target Word : " + answer[index]);
                if (!alreadyWord.Equals(answer[index]))
                {
                    return false;
                }
            }
        }
        return true;
    }
    public bool GetNewQuestionData(string answer, bool isrow, List<WordItem> wordItems)
    {
        bool isCanInput = false;

        if (isrow)
            isCanInput = CheckNewQuestion(wordItems, answer, WordItemType.ROW);
        else
            isCanInput = CheckNewQuestion(wordItems, answer, WordItemType.COL);

        if (isCanInput)
        {
            WordItemType type = isrow ? WordItemType.ROW : WordItemType.COL;
            for (int index = 0; index < wordItems.Count; index++)
            {
                WordItem checkItem = wordItems[index];
                checkItem.SetItemData(answer[index], type);
            }
        }

        return isCanInput;
    }
}