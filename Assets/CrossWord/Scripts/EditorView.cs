using System;
using UnityEngine;
using UnityEngine.UI;

public class EditorView : MonoBehaviour
{
    [SerializeField] private InputField inputField_MatrixSize = null;
    [SerializeField] private Button Btn_Matrix = null;
    public event Action<int> CreateMatrix = null;

    [SerializeField] private InputField inputField_Answer = null;
    [SerializeField] private InputField inputField_Explanation = null;
    [SerializeField] private Button Btn_writeWord = null;
    [SerializeField] private Toggle Toggle_isRow;

    public Action<string, string, bool> CreateNewQuestion = null;

    private void Start()
    {
        Btn_Matrix.onClick.AddListener(OnClickMatrix);
        Btn_writeWord.onClick.AddListener(OnClickWriteNewQuestion);
    }

    private void OnClickMatrix()
    {
        CreateMatrix?.Invoke(int.Parse(inputField_MatrixSize.text));
    }

    private void OnClickWriteNewQuestion()
    {
        if (string.IsNullOrEmpty(inputField_Answer.text) ||
            string.IsNullOrEmpty(inputField_Explanation.text))
        {
            Debug.Log("Check Question InputField");
            return;
        }
        
        CreateNewQuestion?.Invoke(inputField_Answer.text,inputField_Explanation.text,Toggle_isRow.isOn);
    }
}
