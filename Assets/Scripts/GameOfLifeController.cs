using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(GridCellManager))]
public class GameOfLifeController : MonoBehaviour
{
    private enum GameOfLifeRules { TwoDimension, ThreeDimension, Custom };

    [SerializeField] private GridCellManager gridCellManager;

    [Header("Simulation")]
    [SerializeField] private bool playOnStart = true;
    [SerializeField] private float timeBetweenEachIteration;

    [Header("Pattern Editing")]
    [SerializeField] private AliveCellsPatternLibrary.AliveCellsPatternName aliveCellsPattern;
    [SerializeField] private Vector3Int patternPosition = Vector3Int.zero;
    
    [Header("Rules")]
    [SerializeField] private GameOfLifeRules rulesOnStart;
    [Header("Custom Rules")]
    [SerializeField] private int minAmountOfAliveNeighbours = 5;
    [SerializeField] private int maxAmountOfAliveNeighbours = 6;
    [SerializeField] private int amountOfAliveNeighboursToLive = 4;
    
    //These default numbers are what I prefer as the 3D equivalent of the 2D Game Of Life rules. It is more interesting to look at.
    
    private bool _isGameRunning;
    private IEnumerator _currentGameCoroutine;
    public static int NumberOfGenerations { get; private set; }
    public GridCellManager GridCellManager => gridCellManager;
    public Vector3Int InitialPatternPosition => patternPosition;
    public AliveCellsPatternLibrary.AliveCellsPatternName AliveCellsPattern => aliveCellsPattern;

    private void Awake() => SetRules();

    private void SetRules()
    {
        switch(rulesOnStart)
        {
            case GameOfLifeRules.TwoDimension:
                Set2DRules();
                break;
            case GameOfLifeRules.ThreeDimension:
                Set3DRules();
                break;
            case GameOfLifeRules.Custom:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Set2DRules()
    {
        minAmountOfAliveNeighbours = 2;
        maxAmountOfAliveNeighbours = 3;
        amountOfAliveNeighboursToLive = 3;
    }

    private void Set3DRules()
    {
        minAmountOfAliveNeighbours = 5;
        maxAmountOfAliveNeighbours = 6;
        amountOfAliveNeighboursToLive = 4;
    }

    private void Start() => OnPlayOnStart();

    private void OnPlayOnStart()
    {
        if (!playOnStart)
        {
            return;
        }
        SetInitialAliveCells(aliveCellsPattern, patternPosition);
        StartGameOfLife();
    }

    [ContextMenu("Game Of Life/Create Alive Cells Pattern")]
    private void CreateAliveCellsPattern()
    {
        SetInitialAliveCells(aliveCellsPattern, patternPosition);
        print($"<color=green>New {aliveCellsPattern.ToString()} pattern created at position {patternPosition}.</color>");
    }

    private void SetInitialAliveCells(AliveCellsPatternLibrary.AliveCellsPatternName pattern, Vector3Int initialPosition)
    {
        AliveCellsPatternLibrary.SetAliveCellsPattern(pattern, initialPosition);
        GridCellManager.SortAllCells();
    }

    [ContextMenu("Game Of Life/Move On To Next Generation")]
    public void MoveOnToNextGeneration()
    {
        foreach (var aliveCell in GridCellManager.AllAliveCells)
        {
            aliveCell.UpdateNeighboursOfAliveCell();
        }
        UpdateAllCellState();
    }

    private void UpdateAllCellState()
    {
        foreach (var cell in GridCellManager.CellGrid)
        {
            cell.UpdateStateWithRules(minAmountOfAliveNeighbours, maxAmountOfAliveNeighbours, amountOfAliveNeighboursToLive);
            gridCellManager.SortCell(cell);
        }
        
        NumberOfGenerations = GridCellManager.NumberOfAliveCells > 0 ? NumberOfGenerations + 1 : NumberOfGenerations;
    }

    [ContextMenu("Game Of Life/Start Game Of Life")]
    private void StartGameOfLife()
    {
        StopGameOfLifeCoroutine();
        StartGameOfLifeCoroutine();
        print("<color=green>Game Of Life is playing...</color>");
    }

    [ContextMenu("Game Of Life/Reset Game of Life")]
    private void ResetGameOfLife()
    {
        SetAllCellsToDeadState();
        MoveOnToNextGeneration();
        NumberOfGenerations = 0;
        
        print("<color=green>Reset of Game Of Life.</color>");
        if (_isGameRunning)
        {
            print("<color=orange>Game is still running.</color>");
        }
    }

    private void SetAllCellsToDeadState()
    {
        foreach (var cell in GridCellManager.CellGrid)
        {
            cell.CurrentCell.Die();
        }
        GridCellManager.SortAllCells();
    }

    private void StartGameOfLifeCoroutine()
    {
        _currentGameCoroutine = GameOfLifeCoroutine();
        StartCoroutine(GameOfLifeCoroutine());
    }

    [ContextMenu("Game Of Life/Pause Game Of Life")]
    private void PauseGameOfLife()
    {
        StopGameOfLifeCoroutine();
        print("<color=green>Game Of Life Paused.</color>");
    }

    private void StopGameOfLifeCoroutine()
    {
        _isGameRunning = false;
        if (_currentGameCoroutine != null)
        {
            StopCoroutine(_currentGameCoroutine);
        }
    }

    private IEnumerator GameOfLifeCoroutine()
    {
        _isGameRunning = true;
        while (_isGameRunning)
        {
            yield return new WaitForSeconds(timeBetweenEachIteration);
            MoveOnToNextGeneration();
        }
    }
}
