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
        isCellAliveOnNextGeneration = currentCell.IsAlive;

        int nbOfLivingNeighbours = neighboursList.Count(cell => cell.IsAlive);

        bool hasWrongAmountOfLivingNeighbours = nbOfLivingNeighbours < minAmountOfAliveNeighbours || nbOfLivingNeighbours > maxAmountOfAliveNeighbours;

        if (hasWrongAmountOfLivingNeighbours)
            isCellAliveOnNextGeneration = false;
        
        if(nbOfLivingNeighbours == amountOfAliveNeighboursToLive)
            isCellAliveOnNextGeneration = true;
    }

    public void ChangeToNewState()
    {
        if (isCellAliveOnNextGeneration) 
            currentCell.Live();
        else
            currentCell.Die();
    }
}
