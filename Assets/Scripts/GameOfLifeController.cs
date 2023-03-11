using System.Collections;
using UnityEngine;

[RequireComponent(typeof(GridCellManager))]
public class GameOfLifeController : MonoBehaviour
{
    public enum GameOfLifeRule { TwoDimension, ThreeDimension, Custom };

    [SerializeField] GridCellManager gridCellManager;

    [Header("Simulation")]
    [SerializeField] bool playOnStart = true;
    [SerializeField] float timeBetweenEachIteration;

    [Header("Pattern Editing")]
    [SerializeField] AliveCellsPatternLibrary.AliveCellsPatternName aliveCellsPattern = default;
    [SerializeField] Vector3Int patternPosition = Vector3Int.zero;

    [Header("Rules")]
    [SerializeField] GameOfLifeRule rule = default;
    [Header("Custom Rules")]
    [SerializeField] int minAmountOfAliveNeighbours = 5;
    [SerializeField] int maxAmountOfAliveNeighbours = 6;
    [SerializeField] int amountOfAliveNeighboursToLive = 4;

    //These default numbers are the 3D equivalent of the rules in 2D.

    bool _isGameRunning = false;
    IEnumerator _currentGameCoroutine;

    public GridCellManager GridCellManager { get { return gridCellManager; } }
    public Vector3Int InitialPatternPosition { get { return patternPosition; } }
    public AliveCellsPatternLibrary.AliveCellsPatternName AliveCellsPattern { get { return aliveCellsPattern; } }

    void Awake()
    {
        switch(rule)
        {
            case GameOfLifeRule.TwoDimension:
                minAmountOfAliveNeighbours = 2;
                maxAmountOfAliveNeighbours = 3;
                amountOfAliveNeighboursToLive = 3;
                break;
            case GameOfLifeRule.ThreeDimension:
                minAmountOfAliveNeighbours = 5;
                maxAmountOfAliveNeighbours = 6;
                amountOfAliveNeighboursToLive = 4;
                break;
            default:
                break;
        }
    }

    void Start() => OnPlayOnStart();

    void OnPlayOnStart()
    {
        if (!playOnStart) return;
        InstantiateInitialAliveCellsPattern();
        StartGameOfLife();
    }

    [ContextMenu("Game Of Life/Create Alive Cells Pattern")]
    void InstantiateInitialAliveCellsPattern()
    {
        SetInitialAliveCells(aliveCellsPattern, patternPosition);
        SkipGeneration();
    }
    public void SetInitialAliveCells(AliveCellsPatternLibrary.AliveCellsPatternName aliveCellsPattern, Vector3Int initialPosition) => AliveCellsPatternLibrary.SetAliveCellsPattern(aliveCellsPattern, initialPosition);

    [ContextMenu("Game Of Life/Move On To Next Generation")]
    public void MoveOnToNextGeneration()
    {
        foreach (CellStateController cell in GridCellManager.s_cellGrid)
            cell.SetCellStateOnNextGeneration(minAmountOfAliveNeighbours, maxAmountOfAliveNeighbours, amountOfAliveNeighboursToLive);
        ActualizeAllCellState();
    }

    void SkipGeneration()
    {
        foreach (CellStateController cell in GridCellManager.s_cellGrid)
            cell.DontChangeOnNextGeneration();
        ActualizeAllCellState();
    }

    void ActualizeAllCellState()
    {
        foreach (CellStateController cell in GridCellManager.s_cellGrid)
        {
            cell.UpdateState();
            gridCellManager.SortCellGameObject(cell.CurrentCell);
        }
    }

    [ContextMenu("Game Of Life/Start Game Of Life")]
    void StartGameOfLife()
    {
        StopGameOfLifeCoroutine();
        StartGameOfLifeCoroutine();
    }

    [ContextMenu("Game Of Life/Reset Game of Life")]
    void ResetGameOfLife()
    {
        foreach (CellStateController cell in GridCellManager.s_cellGrid)
            cell.CurrentCell.Die();
        MoveOnToNextGeneration();
    }

    void StartGameOfLifeCoroutine()
    {
        _currentGameCoroutine = GameOfLifeCoroutine();
        StartCoroutine(GameOfLifeCoroutine());
    }

    [ContextMenu("Game Of Life/Pause Game Of Life")]
    void StopGameOfLifeCoroutine()
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
