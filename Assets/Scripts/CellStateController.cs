using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellStateController : MonoBehaviour
{
    [SerializeField] Cell currentCell;
    [SerializeField] HashSet<Cell> surroundingNeighbours = new();
    [SerializeField] bool isCellAliveOnNextGeneration = false;
    public Cell CurrentCell { get { return currentCell; } }

    public void AddAllCellNeighbours(CellStateController[,,] cellList, Vector3Int gridSize)
    {
        Vector3Int cell = currentCell.Position;

        int minX = Mathf.Max(cell.x - 1, 0);
        int minY = Mathf.Max(cell.y - 1, 0);
        int minZ = Mathf.Max(cell.z - 1, 0);

        int maxX = Mathf.Min(cell.x + 1, gridSize.x - 1);
        int maxY = Mathf.Min(cell.y + 1, gridSize.y - 1);
        int maxZ = Mathf.Min(cell.z + 1, gridSize.z - 1);

        for (int i = minX; i <= maxX; i++) for (int j = minY; j <= maxY; j++) for (int k = minZ; k <= maxZ; k++)
                    surroundingNeighbours.Add(cellList[i, j, k].CurrentCell);

        surroundingNeighbours.Remove(cellList[cell.x, cell.y, cell.z].CurrentCell);
    }

    public void SetCellStateOnNextGenerationWithRules(int minAmountOfAliveNeighbours, int maxAmountOfAliveNeighbours, int amountOfAliveNeighboursToLive)
    {
        int nbOfLivingNeighbours = surroundingNeighbours.Count(cell => cell.IsAlive);

        if (nbOfLivingNeighbours == amountOfAliveNeighboursToLive && !currentCell.IsAlive)
        {
            isCellAliveOnNextGeneration = true;
            return;
        }

        bool isUnderpopulated = nbOfLivingNeighbours < minAmountOfAliveNeighbours;
        bool isOverpopulated = nbOfLivingNeighbours > maxAmountOfAliveNeighbours;

        if (isUnderpopulated || isOverpopulated)
        {
            isCellAliveOnNextGeneration = false;
            return;
        }
        
        DontChangeOnNextGeneration();
    }

    public void DontChangeOnNextGeneration() => isCellAliveOnNextGeneration = currentCell.IsAlive;

    public void UpdateState()
    {
        if (isCellAliveOnNextGeneration) 
            currentCell.Live();
        else
            currentCell.Die();
    }
}
