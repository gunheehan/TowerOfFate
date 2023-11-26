using System;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    [SerializeField] private List<WordGroupItem> groupItem = null;
    public event Action<string> DeleteActiom = null;

    private void Start()
    {
        DisAbleItem();
    }

    public void SetItemView(CrossWordInfo.GroupWord[] wordData)
    {
        DisAbleItem();

        for (int i = 0; i < wordData.Length; i++)
        {
            groupItem[i].SetItem(wordData[i], OnClickDelete);
        }
    }

    private void DisAbleItem()
    {
        foreach (WordGroupItem item in groupItem)
        {
            item.gameObject.SetActive(false);
        } 
    }
    
    private void OnClickDelete(string answerKey)
    {
        DeleteActiom?.Invoke(answerKey);
    }
}
