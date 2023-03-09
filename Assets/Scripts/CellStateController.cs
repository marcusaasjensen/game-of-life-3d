using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellStateController : MonoBehaviour
{
    [SerializeField] Cell currentCell;
    [SerializeField] List<Cell> neighboursList = new();
    [SerializeField] bool isCellAliveOnNextGeneration;
    public Cell CurrentCell { get { return currentCell; } }

    void Awake() => isCellAliveOnNextGeneration = currentCell.IsAlive;
    public void AddAllCellNeighbours(CellStateController[,,] cellList, Vector3Int gridSize)
    {
        Vector3Int cell = currentCell.Position;
        HashSet<Cell> neighboursSet = new();

        for (int i = -1; i <= 1; i++) for (int j = -1; j <= 1; j++) for (int k = -1; k <= 1; k++)
        {
            Vector3Int neighbour = new(cell.x + i, cell.y + j, cell.z + k);
            neighbour.Clamp(Vector3Int.zero, gridSize - Vector3Int.one);
            neighboursSet?.Add(cellList[neighbour.x, neighbour.y, neighbour.z].CurrentCell);
        }

        neighboursSet?.Remove(cellList[cell.x, cell.y, cell.z].CurrentCell);
        neighboursList?.AddRange(neighboursSet);
    }

    public void SetCellStateOnNextGeneration(int minAmountOfAliveNeighbours, int maxAmountOfAliveNeighbours, int amountOfAliveNeighboursToLive)
    {
        int? nbOfLivingNeighbours = neighboursList?.Count(cell => cell.IsAlive);

        if (nbOfLivingNeighbours == amountOfAliveNeighboursToLive && !currentCell.IsAlive)
        {
            isCellAliveOnNextGeneration = true;
            return;
        }

        bool isUnderpopulated = nbOfLivingNeighbours < minAmountOfAliveNeighbours;
        bool isOverpopulated = nbOfLivingNeighbours > maxAmountOfAliveNeighbours;

        if(isUnderpopulated || isOverpopulated && currentCell.IsAlive) 
            isCellAliveOnNextGeneration = false;
        else
            DontChangeOnNextGeneration();
    }

    public void DontChangeOnNextGeneration() => isCellAliveOnNextGeneration = currentCell.IsAlive;

    public void ChangeToNewState()
    {
        if (isCellAliveOnNextGeneration) 
            currentCell.Live();
        else
            currentCell.Die();
    }
}
