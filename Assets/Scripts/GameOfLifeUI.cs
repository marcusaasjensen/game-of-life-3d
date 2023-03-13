using UnityEngine;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine.Serialization;

public class GameOfLifeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberOfGenerationsText;
    [SerializeField] private TextMeshProUGUI numberOfAliveCellsText;

    private void Update() => UpdateText();

    private void UpdateText()
    {
        numberOfGenerationsText.text = "Number of generations:\n" + GameOfLifeController.NumberOfGenerations;
        numberOfAliveCellsText.text = "Number of alive cells:\n" + GameOfLifeController.NumberOfAliveCells + " out of " + GridCellManager.NumberOfCells;
    }
}
