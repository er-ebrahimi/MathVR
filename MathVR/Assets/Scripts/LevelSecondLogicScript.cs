using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

public class LevelSecondLogicScript : MonoBehaviour
{
    public int rounds = 3;
    public int counter;
    public bool sendOn = false;
    public List<GameObject> answerTexts;
    [SerializeField] private TextMeshPro equationText;
    [SerializeField] private TextMeshPro stat;
    [SerializeField] private GameObject startLevel;
    public GameObject database;

    private void Start()
    {
        SetStat();
    }
    private void SetStat()
    {
        if (database.GetComponent<DataBaseScript>().levelTwoTotalEquations > 0)
        {
            stat.text = "Accuracy: " + ((database.GetComponent<DataBaseScript>().levelTwoCorrectAnswer / database.GetComponent<DataBaseScript>().levelTwoTotalEquations) * 100).ToString();
        }
        else
        {
            stat.text = "Check This Out";
        }
    }
    public void StartGame()
    {
        counter = rounds;
        sendOn = true;
    }

    public void SetEquation()
    {
        counter--;
        if (counter <= 0)
        {
            equationText.text = "You Won";
            SetStat();
            startLevel.SetActive(true);
            for (int i = 0; i < answerTexts.Count; i++)
            {
                answerTexts[i].GetComponent<LevelSecondTargerScript>().targetText.text = "";
                answerTexts[i].GetComponent<LevelSecondTargerScript>().isCorrectAnswer = false;
            }
            return;
        }
        database.GetComponent<DataBaseScript>().levelTwoTotalEquations++;
        var (equation, correctAnswer, answers) = MathGenerator.GenerateEquation(
            MathGenerator.Difficulty.Normal,
            numOfChoices: 2
        );
        if (equationText != null)
        {
            equationText.text = $"Solve: {equation}";
        }
        for (int i = 0; i < answers.Count; i++)
        {
            if (i < answerTexts.Count && answerTexts[i] != null)
            {
                answerTexts[i].GetComponent<LevelSecondTargerScript>().targetText.text = answers[i].ToString();
                answerTexts[i].GetComponent<LevelSecondTargerScript>().isCorrectAnswer = false;
                if (correctAnswer == answers[i])
                {
                    answerTexts[i].GetComponent<LevelSecondTargerScript>().isCorrectAnswer = true;
                }
            }
        }

    }
}
