using System;
using UnityEngine;
using UnityEngine.UI;

public class WordGroupItem : MonoBehaviour
{
    [SerializeField] private Text Text_Answer;
    [SerializeField] private Text Text_Property;
    [SerializeField] private Text Text_Explantion;
    [SerializeField] private Button Btn_Delete;

    public Action<string> DeleteQuestion = null;

    private void Start()
    {
        Btn_Delete.onClick.AddListener(OnClickDelete);
    }

    public void SetItem(CrossWordInfo.GroupWord data)
    {
        Text_Answer.text = "정답 : " + data.answer;
        Text_Explantion.text = "설명 : " + data.explanation;
        Text_Property.text = data.wordItemType == WordItemType.COL ? "행렬 : 세로" : "행렬 : 가로";
        gameObject.SetActive(true);
    }

    private void OnClickDelete()
    {
        DeleteQuestion?.Invoke(Text_Answer.text);
    }
}
