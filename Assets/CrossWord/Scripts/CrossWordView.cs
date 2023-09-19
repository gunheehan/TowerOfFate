using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossWordView : MonoBehaviour
{
    [SerializeField] private RectTransform RootRect = null;
    [SerializeField] private GridLayoutGroup LayoutGroup = null;

    [SerializeField] private WordItem prefab;

    public void SetGridCellSize(int DPcount)
    {
        LayoutGroup.constraintCount = DPcount;

        int cellsize = ((int)RootRect.rect.width - ((int)LayoutGroup.spacing.x * (DPcount - 1))) / DPcount;
        LayoutGroup.cellSize = new Vector2(cellsize, cellsize);
    }
}
