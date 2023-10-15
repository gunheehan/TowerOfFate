using System.Collections.Generic;
using UnityEngine;

public class CrossWordGroupManager
{
    private Dictionary<string, CrossWordInfo.GroupWord> wordsQuestionDic = new Dictionary<string, CrossWordInfo.GroupWord>();

    public bool AddQuestion(CrossWordInfo.GroupWord questionInfo)
    {
        if (wordsQuestionDic.ContainsKey(questionInfo.answer))
        {
            Debug.Log("Already answer");
            return false;
        }
        wordsQuestionDic.Add(questionInfo.answer,questionInfo);
        return true;
    }

    public bool DeleteQuestion(string answer)
    {
        return wordsQuestionDic.Remove(answer);
    }
}
