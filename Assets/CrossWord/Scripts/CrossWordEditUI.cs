using UnityEngine;

public enum WordItemType
{
    NONE,
    ROW,
    LOW,
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
        InitMVP();
    }

    private void OnDestroy()
    {
        DisAllow();
    }

    private void InitMVP()
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
        }
    }

    private void DisAllow()
    {
        if (editorView != null)
        {
            editorView.CreateMatrixAction -= crossWordView.SetGridCellSize;
        }
    }
}
