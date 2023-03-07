using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLifeGizmos : MonoBehaviour
{
    [SerializeField] GameOfLifeController gameOfLife;
    [SerializeField] Color color = new Color(0, 255, 255, 50);
    void OnDrawGizmosSelected()
    {
        if (!gameOfLife) return;
        Gizmos.color = color;
        ShowAliveCellsPatternPosition();
        ShowGrid();
    }

    void ShowAliveCellsPatternPosition() => Gizmos.DrawCube(gameOfLife.InitialPatternPosition, new Vector3Int(1, 1, 1));

    void ShowGrid()
    {
        CellManager cm = gameOfLife.CellManager;
        if (!cm) return;
        Gizmos.DrawWireCube(new Vector3(cm.GridSize.x / 2 - .5f, cm.GridSize.y / 2 - .5f, cm.GridSize.z / 2 - .5f), cm.GridSize);
    }
}
