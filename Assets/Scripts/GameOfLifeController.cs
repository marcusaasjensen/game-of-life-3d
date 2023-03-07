using System.Collections;
using UnityEngine;

public class GameOfLifeController : MonoBehaviour
{
    [SerializeField] CellManager cellManager;

    [Header("Simulation")]
    [SerializeField] bool playOnStart = true;
    [SerializeField] float timeBetweenEachIteration;

    [Header("Pattern Editing")]
    [SerializeField] PatternCreator.AliveCellsPatternName aliveCellsPattern = default;
    [SerializeField] Vector3Int patternPosition = Vector3Int.zero;
    
    [Header("Rules")]
    [SerializeField] int minAmountOfAliveNeighbours = 3;
    [SerializeField] int maxAmountOfAliveNeighbours = 4;
    [SerializeField] int amountOfAliveNeighboursToLive = 4;

    bool _isGameRunning = false;
    IEnumerator _currentGameCoroutine;

    public CellManager CellManager { get { return cellManager; } }
    public Vector3Int InitialPatternPosition { get { return patternPosition; } }

    void Awake() => cellManager = cellManager ? cellManager : GetComponent<CellManager>();
    void Start() { if (playOnStart) StartGameOfLife(); }

    [ContextMenu("Game Of Life/Create Alive Cells Pattern")]
    void InstantiateInitialAliveCellsPattern()
    {
        SetInitialAliveCells(aliveCellsPattern, patternPosition);
        SkipGeneration();
    }
    public void SetInitialAliveCells(PatternCreator.AliveCellsPatternName aliveCellsPattern, Vector3Int initialPosition) => PatternCreator.CreateNewPattern(aliveCellsPattern, initialPosition);

    [ContextMenu("Game Of Life/Move On To Next Generation")]
    public void MoveOnToNextGeneration()
    {
        foreach (CellStateController cell in CellManager.CellList)
            cell.SetCellStateOnNextGeneration(minAmountOfAliveNeighbours, maxAmountOfAliveNeighbours, amountOfAliveNeighboursToLive);
        ActualizeAllCellState();
    }

    void SkipGeneration()
    {
        foreach (CellStateController cell in CellManager.CellList)
            cell.DontChangeOnNextGeneration();
        ActualizeAllCellState();
    }

    void ActualizeAllCellState()
    {
        foreach (CellStateController cell in CellManager.CellList)
        {
            cell.ChangeToNewState();
            cellManager.SortCellGameObject(cell.CurrentCell);
        }
    }

    [ContextMenu("Game Of Life/Start Game Of Life")]
    void StartGameOfLife()
    {
        InstantiateInitialAliveCellsPattern();
        StartGameOfLifeCoroutine();
    }

    [ContextMenu("Game Of Life/Reset Game of Life")]
    void ResetGameOfLife()
    {
        foreach (CellStateController cell in CellManager.CellList)
            cell.CurrentCell.Die();
    }

    [ContextMenu("Game Of Life/Continue Game Of Life")]
    void ContinueGameOfLife() => StartGameOfLifeCoroutine();

    void StartGameOfLifeCoroutine()
    {
        if (_currentGameCoroutine != null)
            StopCoroutine(_currentGameCoroutine);

        _currentGameCoroutine = GameOfLifeCoroutine();
        StartCoroutine(GameOfLifeCoroutine());
    }

    [ContextMenu("Game Of Life/Stop Game Of Life")]
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
