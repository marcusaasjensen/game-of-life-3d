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
        ShowPattern();
        ShowGrid();
    }

    void ShowPattern()
    {
        Vector3Int [] coordinates = AliveCellsPatternLibrary.GetPatternCoordinatesAtPosition(gameOfLife.AliveCellsPattern, gameOfLife.InitialPatternPosition);
        
        foreach (Vector3Int coordinate in coordinates)
        {
            Gizmos.color = gameOfLife.GridCellManager.IsInsideGrid(coordinate) ? color : Color.red;
            Gizmos.DrawCube(coordinate, new Vector3Int(1, 1, 1));
        }
    }

    void ShowGrid()
    {
        Gizmos.color = color;
        GridCellManager cm = gameOfLife.GridCellManager;
        Gizmos.DrawWireCube(new Vector3(cm.GridSizeProperty.x / 2 - CubeSizeOffset, cm.GridSizeProperty.y / 2 - CubeSizeOffset, cm.GridSizeProperty.z / 2 - CubeSizeOffset), cm.GridSizeProperty);
    }
}
