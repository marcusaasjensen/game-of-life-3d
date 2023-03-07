using System.Collections;
using UnityEngine;

public class GameOfLifeController : MonoBehaviour
{
    [SerializeField] CellManager cellManager;
    [SerializeField] bool playOnStart = true;
    [SerializeField] float timeBetweenEachIteration;
    [SerializeField] PatternCreator.AliveCellsPatternName initialAliveCellsPattern = default;
    [SerializeField] Vector3Int initialPatternPosition = Vector3Int.zero;
    [SerializeField] int minAmountOfAliveNeighbours = 2;
    [SerializeField] int maxAmountOfAliveNeighbours = 9;
    [SerializeField] int amountOfAliveNeighboursToLive = 3;

    bool _isGameRunning = false;
    IEnumerator _currentGameCoroutine;

    void Awake() => cellManager = cellManager ? cellManager : GetComponent<CellManager>();
    void Start() { if (playOnStart) StartGameOfLife(); }

    [ContextMenu("Set Initial Alive Cells Pattern At Initial Position")]
    void InstantiateInitialAliveCellsPattern() => SetInitialAliveCells(initialAliveCellsPattern, initialPatternPosition);
    public void SetInitialAliveCells(PatternCreator.AliveCellsPatternName aliveCellsPattern, Vector3Int initialPosition) => PatternCreator.CreateNewPattern(aliveCellsPattern, initialPosition);

    [ContextMenu("Move On To Next Generation")]
    public void MoveOnToNextGeneration()
    {
        PrepareNextGeneration();
        ChangeAllCellState();
    }

    void PrepareNextGeneration()
    {
        foreach (CellStateController cell in CellManager.CellList)
            cell.SetCellStateOnNextGeneration(minAmountOfAliveNeighbours, maxAmountOfAliveNeighbours, amountOfAliveNeighboursToLive);
    }

    void ChangeAllCellState()
    {
        foreach (CellStateController cell in CellManager.CellList)
        {
            cell.ChangeToNewState();
            cellManager.SortCellGameObject(cell.CurrentCell);
        }
    }

    [ContextMenu("Start Game Of Life")]
    void StartGameOfLife()
    {
        InstantiateInitialAliveCellsPattern();
        StartGameOfLifeCoroutine();
    }

    void StartGameOfLifeCoroutine()
    {
        if (_currentGameCoroutine != null)
            StopCoroutine(_currentGameCoroutine);

        _currentGameCoroutine = GameOfLifeCoroutine();
        StartCoroutine(GameOfLifeCoroutine());
    }

    [ContextMenu("Stop Game Of Life")]
    void StopGameOfLife()
    {
        _isGameRunning = false;
        if (_currentGameCoroutine != null)
            StopCoroutine(_currentGameCoroutine);
    }

    IEnumerator GameOfLifeCoroutine()
    {
        _isGameRunning = true;
        while (_isGameRunning)
        {
            yield return new WaitForSeconds(timeBetweenEachIteration);
            MoveOnToNextGeneration();
        }
    }
}
