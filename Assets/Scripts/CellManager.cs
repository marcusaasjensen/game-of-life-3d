using UnityEngine;

public class CellManager : MonoBehaviour
{
    [SerializeField] Vector3Int gridSize = new(4, 4, 4);
    [SerializeField] Transform cellPrefab;
    [SerializeField] Transform deadCellsParent;
    [SerializeField] Transform aliveCellsParent;
    [SerializeField] CellStateController [] cellList;
    [SerializeField] int minAmountOfAliveNeighbours = 2;
    [SerializeField] int maxAmountOfAliveNeighbours = 9;
    [SerializeField] int amountOfAliveNeighboursToLive = 3;

    void Awake()
    {
        InstantiateAllCells();
        SetCellNeighbours();
    }

    void InstantiateAllCells()
    {
        cellList = new CellStateController[gridSize.x * gridSize.y * gridSize.z];

        int index = 0;

        for (int i = 0; i < gridSize.x; i++)
            for (int j = 0; j < gridSize.y; j++)
                for (int k = 0; k < gridSize.z; k++)
                {
                    Transform cell = Instantiate(cellPrefab, new Vector3Int(i, j, k), Quaternion.identity);
                    cell.SetParent(deadCellsParent);
                    cellList[index] = cell.GetComponent<CellStateController>();
                    index++;
                }
    }

    Cell GetCellAtPosition(Vector3Int position)
    {
        foreach(CellStateController cell in cellList)
        {
            if (cell.CurrentCell.Position == position)
                return cell.CurrentCell;
        }
        return null;
    }

    void SetCellNeighbours()
    {
        foreach (CellStateController cell in cellList)
        {
            Vector3Int currentCellPosition = cell.CurrentCell.Position;

            foreach (CellStateController otherCell in cellList)
            {
                Vector3Int otherCellPosition = otherCell.CurrentCell.Position;

                if (otherCellPosition == currentCellPosition) continue;

                bool isNeighbourOnX = otherCellPosition.x == currentCellPosition.x + 1 || otherCellPosition.x == currentCellPosition.x - 1 || otherCellPosition.x == currentCellPosition.x;
                bool isNeighbourOnY = otherCellPosition.y == currentCellPosition.y + 1 || otherCellPosition.y == currentCellPosition.y - 1 || otherCellPosition.y == currentCellPosition.y;
                bool isNeighbourOnZ = otherCellPosition.z == currentCellPosition.z + 1 || otherCellPosition.z == currentCellPosition.z - 1 || otherCellPosition.z == currentCellPosition.z;

                if (!(isNeighbourOnX && isNeighbourOnY && isNeighbourOnZ)) continue;

                cell.AddCellNeighbour(otherCell.CurrentCell);
            }
        }
    }

    public void SetInitialAliveCells(AliveCellsPattern aliveCellsPattern, Vector3Int initialPosition) 
    {
        switch(aliveCellsPattern)
        {
            case AliveCellsPattern.Block3D:
                CreateBlock3D(initialPosition);
                break;
            case AliveCellsPattern.Tub3D:
                CreateTub3D(initialPosition);
                break;
            default:
                break;
        }
    }

    void CreateBlock3D(Vector3Int position)
    {
        GetCellAtPosition(position)?.Live();
        GetCellAtPosition(new(position.x + 1, position.y, position.z))?.Live();
        GetCellAtPosition(new(position.x, position.y + 1, position.z))?.Live();
        GetCellAtPosition(new(position.x + 1, position.y + 1, position.z))?.Live();
        GetCellAtPosition(new(position.x, position.y, position.z + 1))?.Live();
        GetCellAtPosition(new(position.x + 1, position.y, position.z + 1))?.Live();
        GetCellAtPosition(new(position.x, position.y + 1, position.z + 1))?.Live();
        GetCellAtPosition(new(position.x + 1, position.y + 1, position.z + 1))?.Live();
    }

    void CreateTub3D(Vector3Int position)
    {
        GetCellAtPosition(new(position.x + 1, position.y + 1, position.z))?.Live();
        GetCellAtPosition(new(position.x + 1, position.y, position.z + 1))?.Live();
        GetCellAtPosition(new(position.x, position.y + 1, position.z + 1))?.Live();
        GetCellAtPosition(new(position.x + 2, position.y + 1, position.z + 1))?.Live();
        GetCellAtPosition(new(position.x + 1, position.y + 1, position.z + 2))?.Live();
        GetCellAtPosition(new(position.x + 1, position.y + 2, position.z + 1))?.Live();
    }

    void SortCellGameObject(Cell cell) => cell.transform.SetParent(cell.IsAlive ? aliveCellsParent : deadCellsParent);

    void PrepareNextGeneration()
    {
        foreach(CellStateController cell in cellList)
            cell.SetCellStateOnNextGeneration(minAmountOfAliveNeighbours, maxAmountOfAliveNeighbours, amountOfAliveNeighboursToLive);
    }

    void ChangeAllCellState()
    {
        foreach (CellStateController cell in cellList)
        {
            cell.ChangeToNewState();
            SortCellGameObject(cell.CurrentCell);
        }
    }

    public void MoveOnToNextGeneration()
    {
        PrepareNextGeneration();
        ChangeAllCellState();
    }

}
