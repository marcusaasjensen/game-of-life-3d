using System.Collections.Generic;
using UnityEngine;

public class CellStateController : MonoBehaviour
{
    [SerializeField] private Cell currentCell;
    private readonly HashSet<CellStateController> _surroundingNeighbours = new();
    public Cell CurrentCell => currentCell;
    private int NumberOfLivingNeighbours { get; set; }

    public void AddAllCellNeighbours(CellStateController[,,] cellList, Vector3Int gridSize)
    {
        var cell = currentCell.Position;

        var minX = Mathf.Max(cell.x - 1, 0);
        var minY = Mathf.Max(cell.y - 1, 0);
        var minZ = Mathf.Max(cell.z - 1, 0);

        var maxX = Mathf.Min(cell.x + 1, gridSize.x - 1);
        var maxY = Mathf.Min(cell.y + 1, gridSize.y - 1);
        var maxZ = Mathf.Min(cell.z + 1, gridSize.z - 1);

        for (var i = minX; i <= maxX; i++)
        {
            for (var j = minY; j <= maxY; j++)
            {
                for (var k = minZ; k <= maxZ; k++)
                {
                    _surroundingNeighbours.Add(cellList[i, j, k]);
                }
            }
        }

        _surroundingNeighbours.Remove(cellList[cell.x, cell.y, cell.z]);
    }

    public void UpdateNeighboursOfAliveCell()
    {
        foreach (var neighbour in _surroundingNeighbours)
        {
            neighbour.NumberOfLivingNeighbours++;
        }
    }

    public void UpdateStateWithRules(int minAmountOfAliveNeighbours, int maxAmountOfAliveNeighbours, int amountOfAliveNeighboursToLive)
    {
        if (NumberOfLivingNeighbours == amountOfAliveNeighboursToLive && !currentCell.IsAlive)
        {
            currentCell.Live();
            NumberOfLivingNeighbours = 0;
            return;
        }
            
        var isUnderpopulated = NumberOfLivingNeighbours < minAmountOfAliveNeighbours;
        var isOverpopulated = NumberOfLivingNeighbours > maxAmountOfAliveNeighbours;

        if (isUnderpopulated || isOverpopulated)
        {
            currentCell.Die();
        }
        
        NumberOfLivingNeighbours = 0;
    }
}
