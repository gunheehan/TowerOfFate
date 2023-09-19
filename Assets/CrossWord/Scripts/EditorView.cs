using System;
using UnityEngine;
using UnityEngine.UI;

public class EditorView : MonoBehaviour
{
    [SerializeField] private InputField inputField_MatrixSize = null;
    [SerializeField] private Button Btn_Matrix = null;
    public event Action<int> CreateMatrixAction = null;

    private void Start()
    {
        Btn_Matrix.onClick.AddListener(OnClickMatrix);
    }

    private void OnClickMatrix()
    {
        CreateMatrixAction?.Invoke(int.Parse(inputField_MatrixSize.text));
    }
}
