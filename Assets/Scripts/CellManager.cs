using UnityEngine;

public class CellManager : MonoBehaviour
{
    [SerializeField, Min(0)] Vector3Int gridSize = new(30, 30, 30);
    [SerializeField] Transform cellPrefab;
    [SerializeField] Transform deadCellsParent;
    [SerializeField] Transform aliveCellsParent;

    static CellStateController [,,] s_cellGrid;
    static Vector3Int s_gridSize;
    public Vector3Int GridSize { get { return gridSize; } }
    public static CellStateController[,,] CellList { get { return s_cellGrid; } } 

    void Awake()
    {
        InstantiateAllCells();
        SetCellNeighbours();
    }

    void InstantiateAllCells()
    {
        s_gridSize = gridSize;
        s_cellGrid = new CellStateController[s_gridSize.x, s_gridSize.y, s_gridSize.z];

        for (int i = 0; i < s_gridSize.x; i++)
            for (int j = 0; j < s_gridSize.y; j++)
                for (int k = 0; k < s_gridSize.z; k++)
                {
                    Transform cell = Instantiate(cellPrefab, new Vector3Int(i, j, k), Quaternion.identity);
                    cell.SetParent(deadCellsParent);
                    s_cellGrid[i,j,k] = cell.GetComponent<CellStateController>();
                }
    }

    public static Cell GetCellAtPosition(Vector3Int position) => s_cellGrid[Mathf.Clamp(position.x, 0, s_gridSize.x - 1), Mathf.Clamp(position.y, 0, s_gridSize.y - 1), Mathf.Clamp(position.z, 0, s_gridSize.z - 1)].CurrentCell;

    void SetCellNeighbours()
    {
        foreach (CellStateController cell in s_cellGrid)
            cell.AddAllCellNeighbours(s_cellGrid, gridSize);
    }

    public void SortCellGameObject(Cell cell) => cell.transform.SetParent(cell.IsAlive ? aliveCellsParent : deadCellsParent);

    public bool IsPositionOutsideGrid(Vector3Int position)
    {
        bool isBelow = position.x < 0 || position.y < 0 || position.z < 0;
        bool isAbove = position.x >= gridSize.x || position.y >= gridSize.y || position.z >= gridSize.z;
        return isBelow || isAbove;
    }

}
