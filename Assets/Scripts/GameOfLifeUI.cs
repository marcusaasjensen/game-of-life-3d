using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class GameOfLifeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberOfGenerationsText;

    private void Update() => numberOfGenerationsText.text = "Number of generations: " + GameOfLifeController.NumberOfGenerations;
}
