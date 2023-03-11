using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellStateController : MonoBehaviour
{
    [SerializeField] private Cell currentCell;
    [SerializeField] private bool isCellAliveOnNextGeneration;
    
    private readonly HashSet<Cell> _surroundingNeighbours = new();
    public Cell CurrentCell => currentCell;

    public void AddAllCellNeighbours(CellStateController[,,] cellList, Vector3Int gridSize)
    {
        var cell = currentCell.Position;

        var minX = Mathf.Max(cell.x - 1, 0);
        var minY = Mathf.Max(cell.y - 1, 0);
        var minZ = Mathf.Max(cell.z - 1, 0);

        var maxX = Mathf.Min(cell.x + 1, gridSize.x - 1);
        var maxY = Mathf.Min(cell.y + 1, gridSize.y - 1);
        var maxZ = Mathf.Min(cell.z + 1, gridSize.z - 1);

        for (var i = minX; i <= maxX; i++) for (var j = minY; j <= maxY; j++) for (var k = minZ; k <= maxZ; k++)
                    _surroundingNeighbours.Add(cellList[i, j, k].CurrentCell);

        _surroundingNeighbours.Remove(cellList[cell.x, cell.y, cell.z].CurrentCell);
    }

    public void SetCellStateOnNextGenerationWithRules(int minAmountOfAliveNeighbours, int maxAmountOfAliveNeighbours, int amountOfAliveNeighboursToLive)
    {
        var nbOfLivingNeighbours = _surroundingNeighbours.Count(cell => cell.IsAlive);

        if (nbOfLivingNeighbours == amountOfAliveNeighboursToLive && !currentCell.IsAlive)
        {
            isCellAliveOnNextGeneration = true;
            return;
        }

        var isUnderpopulated = nbOfLivingNeighbours < minAmountOfAliveNeighbours;
        var isOverpopulated = nbOfLivingNeighbours > maxAmountOfAliveNeighbours;

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
