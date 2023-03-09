using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOfLifeGizmos : MonoBehaviour
{
    [SerializeField] GameOfLifeController gameOfLife;
    [SerializeField] Color color = new Color(0, 255, 255, 50);
    void OnDrawGizmosSelected()
    {
        if (!gameOfLife) return;
        ShowAliveCellsPatternPosition();
        ShowGrid();
    }

    void ShowAliveCellsPatternPosition()
    {
        Gizmos.color = gameOfLife.CellManager.IsPositionOutsideGrid(gameOfLife.InitialPatternPosition)? Color.red : color;
        Gizmos.DrawCube(gameOfLife.InitialPatternPosition, new Vector3Int(1, 1, 1));
    }

    void ShowGrid()
    {
        Gizmos.color = color;
        CellManager cm = gameOfLife.CellManager;
        if (!cm) return;
        Gizmos.DrawWireCube(new Vector3(cm.GridSize.x / 2 - .5f, cm.GridSize.y / 2 - .5f, cm.GridSize.z / 2 - .5f), cm.GridSize);
    }
}
