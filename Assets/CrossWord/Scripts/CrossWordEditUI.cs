using UnityEngine;

public class CrossWordEditUI : MonoBehaviour
{
    [SerializeField] private CrossWordView crossWordView = null;
    [SerializeField] private EditorView editorView = null;

    private bool isInit = false;
    private void Start()
    {
        InitMVC();
    }

    private void OnDestroy()
    {
        DisAllow();
    }

    private void InitMVC()
    {
        if(isInit)
            return;

        Allow();
        isInit = true;
    }

    private void Allow()
    {
        if (editorView != null)
        {
            editorView.CreateMatrixAction += crossWordView.SetGridCellSize;
            editorView.CreateMatrixAction += crossWordView.SetCellItem;
            editorView.CreateNewQuestion += crossWordView.GetNewQuestionData;
        }
    }

    private void DisAllow()
    {
        if (editorView != null)
        {
            editorView.CreateMatrixAction -= crossWordView.SetGridCellSize;
            editorView.CreateMatrixAction -= crossWordView.SetCellItem;
            editorView.CreateNewQuestion -= crossWordView.GetNewQuestionData;
        }
    }
}
