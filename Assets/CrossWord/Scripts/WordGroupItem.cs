using System;
using UnityEngine;
using UnityEngine.UI;

public class WordGroupItem : MonoBehaviour
{
    public Action<string> DeleteQuestion = null;

    [SerializeField] private Text Text_Answer;
    [SerializeField] private Text Text_Property;
    [SerializeField] private Text Text_Explantion;
    [SerializeField] private Button Btn_Delete;
    
    private string answer = String.Empty;
    
    private void Start()
    {
        Btn_Delete.onClick.AddListener(OnClickDelete);
    }

    private void OnDisable()
    {
        DeleteQuestion = null;
    }

    public void SetItem(CrossWordInfo.GroupWord data, Action<string> deleteQuiz)
    {
        answer = data.answer;
        Text_Answer.text = "정답 : " + data.answer;
        Text_Explantion.text = "설명 : " + data.explanation;
        Text_Property.text = data.wordItemType == WordItemType.COL ? "행렬 : 세로" : "행렬 : 가로";
        DeleteQuestion = deleteQuiz;
        gameObject.SetActive(true);
    }

    private void OnClickDelete()
    {
        DeleteQuestion?.Invoke(answer);
        DeleteQuestion = null;
        gameObject.SetActive(false);
    }
}
