using UnityEngine;

[RequireComponent(typeof(GameOfLifeController))]
public class GameOfLifeGizmos : MonoBehaviour
{
    [SerializeField] private GameOfLifeController gameOfLife;
    [SerializeField] private Color color = new(0, 255, 255, 50);
    private const float CubeSizeOffset = .5f;

    private void OnDrawGizmos() => gameOfLife = gameOfLife ? gameOfLife : GetComponent<GameOfLifeController>();
    private void OnDrawGizmosSelected()
    {
        ShowPattern();
        ShowGrid();
    }

    private void ShowPattern()
    {
        var coordinates = AliveCellsPatternLibrary.GetPatternCoordinatesAtPosition(gameOfLife.AliveCellsPattern, gameOfLife.InitialPatternPosition);
        
        foreach (var coordinate in coordinates)
        {
            Gizmos.color = gameOfLife.GridCellManager.IsInsideGrid(coordinate) ? color : Color.red;
            Gizmos.DrawCube(coordinate, new Vector3Int(1, 1, 1));
        }
    }

    private void ShowGrid()
    {
        Gizmos.color = color;
        var cm = gameOfLife.GridCellManager;
        Gizmos.DrawWireCube(new Vector3(cm.GridSizeProperty.x / 2f - CubeSizeOffset, cm.GridSizeProperty.y / 2f - CubeSizeOffset, cm.GridSizeProperty.z / 2f - CubeSizeOffset), cm.GridSizeProperty);
    }
}
