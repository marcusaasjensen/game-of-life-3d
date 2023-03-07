using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellStateController : MonoBehaviour
{
    [SerializeField] Cell currentCell;
    [SerializeField] List<Cell> neighboursList;
    [SerializeField] bool isCellAliveOnNextGeneration;
    public Cell CurrentCell { get { return currentCell; } }

    void Awake() => isCellAliveOnNextGeneration = currentCell.IsAlive;

    public void AddCellNeighbour(Cell cell) => neighboursList?.Add(cell);

    public void SetCellStateOnNextGeneration(int minAmountOfAliveNeighbours, int maxAmountOfAliveNeighbours, int amountOfAliveNeighboursToLive)
    {
        int nbOfLivingNeighbours = neighboursList.Count(cell => cell.IsAlive);

        if (nbOfLivingNeighbours == amountOfAliveNeighboursToLive)
        {
            isCellAliveOnNextGeneration = true;
            return;
        }

        bool isUnderpopulated = nbOfLivingNeighbours < minAmountOfAliveNeighbours;
        bool isOverpopulated = nbOfLivingNeighbours > maxAmountOfAliveNeighbours;

        if(isUnderpopulated || isOverpopulated) 
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
