using UnityEngine;

public enum WordItemType
{
    NONE,
    ROW,
    COL,
    CROSS
}
public class CrossWordEditUI : MonoBehaviour
{
    [SerializeField] private CrossWordView crossWordView = null;
    [SerializeField] private EditorView editorView = null;
    private CrossWordModel crossWordModel = null;

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
        crossWordModel = new CrossWordModel();
        Allow();
        isInit = true;
    }

    private void Allow()
    {
        if (editorView != null)
        {
            editorView.CreateMatrixAction += crossWordView.SetGridCellSize;
            editorView.CreateMatrixAction += crossWordView.SetCellItem;
        }
    }

    private void DisAllow()
    {
        if (editorView != null)
        {
            editorView.CreateMatrixAction -= crossWordView.SetGridCellSize;
            editorView.CreateMatrixAction -= crossWordView.SetCellItem;
        }
    }
}
