using System;
using UnityEngine;
using UnityEngine.UI;

public class WordItem : MonoBehaviour
{
    [HideInInspector] public WordItemType ItemType = WordItemType.NONE;
    [SerializeField] private Text Text_word;

    private void OnDisable()
    {
        ItemType = WordItemType.NONE;
    }

    public void SetItem(string number)
    {
        Text_word.text = number;
    }
}
