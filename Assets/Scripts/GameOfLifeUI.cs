using UnityEngine;
using TMPro;

public class GameOfLifeUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numberOfgenerationsText;

    void Update() => numberOfgenerationsText.text = "Number of generations: " + GameOfLifeController.NumberOfGenerations;
}
