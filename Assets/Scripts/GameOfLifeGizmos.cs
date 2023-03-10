using UnityEngine;

[RequireComponent(typeof(GameOfLifeController))]
public class GameOfLifeGizmos : MonoBehaviour
{
    [SerializeField] GameOfLifeController gameOfLife;
    [SerializeField] Color color = new(0, 255, 255, 50);
    const float CubeSizeOffset = .5f;

    void OnDrawGizmos() => gameOfLife = gameOfLife ? gameOfLife : GetComponent<GameOfLifeController>();
    void OnDrawGizmosSelected()
    {
        ShowAliveCellsPatternPosition();
        ShowGrid();
    }

    void ShowAliveCellsPatternPosition()
    {
        Gizmos.color = gameOfLife.GridCellManager.IsInsideGrid(gameOfLife.InitialPatternPosition) ? color : Color.red;
        Gizmos.DrawCube(gameOfLife.InitialPatternPosition, new Vector3Int(1, 1, 1));
    }

    void ShowGrid()
    {
        Gizmos.color = color;
        GridCellManager cm = gameOfLife.GridCellManager;
        Gizmos.DrawWireCube(new Vector3(cm.GridSizeProperty.x / 2 - CubeSizeOffset, cm.GridSizeProperty.y / 2 - CubeSizeOffset, cm.GridSizeProperty.z / 2 - CubeSizeOffset), cm.GridSizeProperty);
    }
}
