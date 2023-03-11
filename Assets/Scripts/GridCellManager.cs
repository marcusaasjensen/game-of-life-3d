using UnityEngine;

public class GridCellManager : MonoBehaviour
{
    [SerializeField, Min(0)] private Vector3Int gridSizeProperty = new(20, 20, 20);
    [SerializeField] private Transform cellPrefab;
    [SerializeField] private Transform deadCellsParent;
    [SerializeField] private Transform aliveCellsParent;

    public static CellStateController[,,] CellGrid;
    public static Vector3Int GridSize;
    public Vector3Int GridSizeProperty => gridSizeProperty;

    private void Awake()
    {
        SetGridSize();
        InstantiateAllCells();
        SetCellNeighbours();
    }

    private void SetGridSize()
    {
        GridSize = gridSizeProperty;
        CellGrid = new CellStateController[GridSize.x, GridSize.y, GridSize.z];
    }

    private void InstantiateAllCells()
    {
        for (var i = 0; i < GridSize.x; i++)
            for (var j = 0; j < GridSize.y; j++)
                for (var k = 0; k < GridSize.z; k++)
                    InstantiateCell(new Vector3Int(i, j, k));
    }

    private void InstantiateCell(Vector3Int position)
    {
        var cell = Instantiate(cellPrefab, position, Quaternion.identity);
        cell.SetParent(deadCellsParent);
        CellGrid[position.x, position.y, position.z] = cell.GetComponent<CellStateController>();
    }

    private void SetCellNeighbours()
    {
        foreach (var cell in CellGrid)
            cell.AddAllCellNeighbours(CellGrid, gridSizeProperty);
    }
    public static Cell GetCellAtPosition(Vector3Int position) => IsPositionInsideZone(Vector3.zero, GridSize, position) ? CellGrid[position.x, position.y,position.z].CurrentCell : null;
    public void SortCellGameObject(Cell cell) => cell.transform.SetParent(cell.IsAlive ? aliveCellsParent : deadCellsParent);
    public bool IsInsideGrid(Vector3Int position) => IsPositionInsideZone(Vector3.zero, gridSizeProperty, position);
    private static bool IsPositionInsideZone(Vector3 startZonePosition, Vector3 endZonePosition, Vector3 position)
    {
        var distWithStartZone = startZonePosition - position;
        var distWithEndZone= endZonePosition - position;
        return distWithStartZone is {x: <= 0, y: <= 0, z: <= 0 } && distWithEndZone is { x: > 0, y: > 0, z: > 0 };
    }
}

