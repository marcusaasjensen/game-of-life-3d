using UnityEngine;

public class GridCellManager : MonoBehaviour
{
    [SerializeField, Min(0)] Vector3Int gridSize = new(30, 30, 30);
    [SerializeField] Transform cellPrefab;
    [SerializeField] Transform deadCellsParent;
    [SerializeField] Transform aliveCellsParent;

    public static CellStateController[,,] s_cellGrid = default;
    public static Vector3Int s_gridSize = default;
    public Vector3Int GridSizeProperty { get { return gridSize; } }

    void Awake()
    {
        SetGridSize();
        InstantiateAllCells();
        SetCellNeighbours();
    }

    void SetGridSize()
    {
        s_gridSize = gridSize;
        s_cellGrid = new CellStateController[s_gridSize.x, s_gridSize.y, s_gridSize.z];
    }

    void InstantiateAllCells()
    {
        for (int i = 0; i < s_gridSize.x; i++)
            for (int j = 0; j < s_gridSize.y; j++)
                for (int k = 0; k < s_gridSize.z; k++)
                    InstantiateCell(new(i, j, k));
    }

    void InstantiateCell(Vector3Int position)
    {
        Transform cell = Instantiate(cellPrefab, position, Quaternion.identity);
        cell.SetParent(deadCellsParent);
        s_cellGrid[position.x, position.y, position.z] = cell.GetComponent<CellStateController>();
    }

    void SetCellNeighbours()
    {
        foreach (CellStateController cell in s_cellGrid)
            cell.AddAllCellNeighbours(s_cellGrid, gridSize);
    }
    public static Cell GetCellAtPosition(Vector3Int position) => IsPositionInsideZone(Vector3.zero, s_gridSize, position) ? s_cellGrid[position.x, position.y,position.z].CurrentCell : null;
    public void SortCellGameObject(Cell cell) => cell.transform.SetParent(cell.IsAlive ? aliveCellsParent : deadCellsParent);
    public bool IsInsideGrid(Vector3Int position) => IsPositionInsideZone(Vector3.zero, gridSize, position);
    static bool IsPositionInsideZone(Vector3 startZonePosition, Vector3 endZonePosition, Vector3 position)
    {
        Vector3 distWithStartZone = startZonePosition - position;
        Vector3 distWithEndZone= endZonePosition - position;
        return distWithStartZone.x <= 0 && distWithStartZone.y <= 0 && distWithStartZone.z <= 0 && distWithEndZone.x > 0 && distWithEndZone.y > 0 && distWithEndZone.z > 0;
    }
}

