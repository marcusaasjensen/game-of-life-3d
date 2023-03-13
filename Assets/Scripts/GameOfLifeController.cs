using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(GridCellManager))]
public class GameOfLifeController : MonoBehaviour
{
    private enum GameOfLifeRule { TwoDimension, ThreeDimension, Custom };

    [SerializeField] private GridCellManager gridCellManager;

    [Header("Simulation")]
    [SerializeField] private bool playOnStart = true;
    [SerializeField] private float timeBetweenEachIteration;

    [Header("Pattern Editing")]
    [SerializeField] private AliveCellsPatternLibrary.AliveCellsPatternName aliveCellsPattern;
    [SerializeField] private Vector3Int patternPosition = Vector3Int.zero;

    [FormerlySerializedAs("rule")]
    [Header("Rules")]
    [SerializeField] private GameOfLifeRule ruleOnStart;
    [Header("Custom Rules")]
    [SerializeField] private int minAmountOfAliveNeighbours = 5;
    [SerializeField] private int maxAmountOfAliveNeighbours = 6;
    [SerializeField] private int amountOfAliveNeighboursToLive = 4;

    //These default numbers are the 3D equivalent of the rules in 2D.

    private bool _allCellsDead;
    private bool _isGameRunning;
    private IEnumerator _currentGameCoroutine;

    public static int NumberOfGenerations { get; private set; }
    public GridCellManager GridCellManager => gridCellManager;
    public Vector3Int InitialPatternPosition => patternPosition;
    public AliveCellsPatternLibrary.AliveCellsPatternName AliveCellsPattern => aliveCellsPattern;

    private void Awake()
    {
        switch(ruleOnStart)
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
            case GameOfLifeRule.Custom:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Start() => OnPlayOnStart();

    private void OnPlayOnStart()
    {
        if (!playOnStart) return;
        InstantiateInitialAliveCellsPattern();
        StartGameOfLife();
    }

    [ContextMenu("Game Of Life/Create Alive Cells Pattern")]
    private void InstantiateInitialAliveCellsPattern()
    {
        SetInitialAliveCells(aliveCellsPattern, patternPosition);
        SkipGeneration();
    }
    private static void SetInitialAliveCells(AliveCellsPatternLibrary.AliveCellsPatternName aliveCellsPattern, Vector3Int initialPosition) => AliveCellsPatternLibrary.SetAliveCellsPattern(aliveCellsPattern, initialPosition);

    [ContextMenu("Game Of Life/Move On To Next Generation")]
    public void MoveOnToNextGeneration()
    {
        foreach (var cell in GridCellManager.CellGrid)
            cell.SetCellStateOnNextGenerationWithRules(minAmountOfAliveNeighbours, maxAmountOfAliveNeighbours, amountOfAliveNeighboursToLive);
        ActualizeAllCellState();
    }

    private void SkipGeneration()
    {
        foreach (var cell in GridCellManager.CellGrid)
            cell.DontChangeOnNextGeneration();
        ActualizeAllCellState();
    }

    private void ActualizeAllCellState()
    {
        _allCellsDead = true;

        foreach (var cell in GridCellManager.CellGrid)
        {
            cell.UpdateState();
            gridCellManager.SortCellGameObject(cell.CurrentCell);
            
            if(cell.CurrentCell.IsAlive)
                _allCellsDead = false;
        }

        if(!_allCellsDead)
            NumberOfGenerations++;
    }

    [ContextMenu("Game Of Life/Start Game Of Life")]
    private void StartGameOfLife()
    {
        StopGameOfLifeCoroutine();
        StartGameOfLifeCoroutine();
    }

    [ContextMenu("Game Of Life/Reset Game of Life")]
    private void ResetGameOfLife()
    {
        foreach (var cell in GridCellManager.CellGrid)
            cell.CurrentCell.Die();
        MoveOnToNextGeneration();
        NumberOfGenerations = 0;
    }

    private void StartGameOfLifeCoroutine()
    {
        _currentGameCoroutine = GameOfLifeCoroutine();
        StartCoroutine(GameOfLifeCoroutine());
    }

    [ContextMenu("Game Of Life/Pause Game Of Life")]
    private void StopGameOfLifeCoroutine()
    {
        _isGameRunning = false;
        if (_currentGameCoroutine != null)
            StopCoroutine(_currentGameCoroutine);
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
