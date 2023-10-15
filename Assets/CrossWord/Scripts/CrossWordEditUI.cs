using UnityEngine;

public class CrossWordEditUI : MonoBehaviour
{
    [SerializeField] private CrossWordView crossWordView = null;
    [SerializeField] private EditorView editorView = null;
    [SerializeField] private GroupInfoController GroupInfoController = null;

    private CrossWordGroupManager CrossWordGroupManager;
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
        CrossWordGroupManager = new CrossWordGroupManager();
        Allow();
        isInit = true;
    }

    private void Allow()
    {
        if (editorView != null)
        {
            editorView.CreateMatrix += crossWordView.SetGridCellSize;
            editorView.CreateMatrix += crossWordView.SetCellItem;
            editorView.CreateNewQuestion += crossWordView.SetNewQuestionData;
        }

        if (crossWordView != null)
        {
            crossWordView.AddQuestion += CrossWordGroupManager.AddQuestion;
            crossWordView.SelctItem += GroupInfoController.SetItemQuestionInfo;
            GroupInfoController.DeleteActiom += CrossWordGroupManager.DeleteQuestion;
        }
    }

    private void DisAllow()
    {
        if (editorView != null)
        {
            editorView.CreateMatrix -= crossWordView.SetGridCellSize;
            editorView.CreateMatrix -= crossWordView.SetCellItem;
            editorView.CreateNewQuestion -= crossWordView.SetNewQuestionData;
        }
        
        if (crossWordView != null)
        {
            crossWordView.AddQuestion -= CrossWordGroupManager.AddQuestion;
            crossWordView.SelctItem -= GroupInfoController.SetItemQuestionInfo;
            GroupInfoController.DeleteActiom -= CrossWordGroupManager.DeleteQuestion;
        }
    }
}
