using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AliveCellsPattern
{
    None,
    Block3D,
    Tub3D,
}

public class GameOfLifeController : MonoBehaviour
{
    [SerializeField] bool isGameRunning = false;
    [SerializeField] float timeBetweenEachIteration;
    [SerializeField] CellManager cellManager;
    [SerializeField] AliveCellsPattern initialAliveCellsPattern = default;
    [SerializeField] Vector3Int initialPatternPosition = Vector3Int.zero;

    IEnumerator _currentGameCoroutine;

    void Start()
    {
        StartGameOfLife();
    }

    [ContextMenu("Set Initial Alive Cells Pattern")]
    void InstantiateInitialAliveCellsPattern() => cellManager.SetInitialAliveCells(initialAliveCellsPattern, initialPatternPosition);

    [ContextMenu("Start Game Of Life")]
    void StartGameOfLife()
    {
        InstantiateInitialAliveCellsPattern();

        if (_currentGameCoroutine != null)
            StopCoroutine(_currentGameCoroutine);

        isGameRunning = true;
        _currentGameCoroutine = GameOfLifeCoroutine();
        StartCoroutine(GameOfLifeCoroutine());
    }

    [ContextMenu("Stop Game Of Life")]
    void StopGameOfLife()
    {
        isGameRunning = false;
        if (_currentGameCoroutine != null)
            StopCoroutine(_currentGameCoroutine);
    }

    IEnumerator GameOfLifeCoroutine()
    {
        while (isGameRunning)
        {
            yield return new WaitForSeconds(timeBetweenEachIteration);
            cellManager.MoveOnToNextGeneration();
        }
        yield break;
    }

}
