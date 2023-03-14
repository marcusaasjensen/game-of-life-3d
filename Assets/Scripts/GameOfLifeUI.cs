using UnityEngine;
using TMPro;

public class GameOfLifeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberOfGenerationsText;
    [SerializeField] private TextMeshProUGUI numberOfAliveCellsText;

    private void Update() => UpdateText();

    private void UpdateText()
    {
        numberOfGenerationsText.text = "Number of generations:\n" + GameOfLifeController.NumberOfGenerations;
        numberOfAliveCellsText.text = "Number of alive cells:\n" + GridCellManager.NumberOfAliveCells + " out of " + GridCellManager.NumberOfCells;
    }
}
