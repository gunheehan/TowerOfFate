using System.Collections.Generic;
using UnityEngine;

public class CrossWordGroupManager
{
    private Dictionary<string, CrossWordInfo.GroupWord> wordsQuestionDic = new Dictionary<string, CrossWordInfo.GroupWord>();

    public void AddQuestion(CrossWordInfo.GroupWord questionInfo)
    {
        if (wordsQuestionDic.ContainsKey(questionInfo.answer))
        {
            Debug.Log("Already answer");
            return;
        }
        wordsQuestionDic.Add(questionInfo.answer,questionInfo);
    }

    public void DeleteQuestion(string answer)
    {
        wordsQuestionDic.Remove(answer);
    }

    public CrossWordInfo.GroupWord GetQuestionInfo(string answerKey)
    {
        if (wordsQuestionDic.ContainsKey(answerKey))
        {
            return wordsQuestionDic[answerKey];
        }

        return null;
    }
}
