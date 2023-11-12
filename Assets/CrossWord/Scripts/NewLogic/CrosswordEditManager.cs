using UnityEngine;

public class CrosswordEditManager : MonoBehaviour
{
    [SerializeField] private QuizGroupManager quizGroupManager;
    [SerializeField] private ItemController itemController;
    [SerializeField] private ViewController viewController;
    [SerializeField] private EditorView editorView = null;
    
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        SubScribe();
    }

    private void SubScribe()
    {
        editorView.CreateMatrix += itemController.SetGridCellSize;
        editorView.CreateMatrix += itemController.SetCellItem;
        editorView.CreateNewQuestion += quizGroupManager.AddNewQuizData;

        itemController.OnItemSetting += quizGroupManager.SetItemMatrix;
        itemController.OnClickItem += quizGroupManager.SetSeletcItem;

        quizGroupManager.ShowNewQuizData += viewController.SetItemView;
        quizGroupManager.InputNewQuiz += itemController.InputNewItemData;
        quizGroupManager.DeletQuizData += itemController.DeleteNewItemData;
        viewController.DeleteActiom += quizGroupManager.DeleteQuizData;
    }

    private void UnSubScribe()
    {
        
    }
}
