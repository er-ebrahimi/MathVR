using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelThreeLogicScript : MonoBehaviour
{
    public int rounds = 3;
    public int counter;
    [SerializeField] private GameObject startLevel;
    [SerializeField] private GameObject database;
    [SerializeField] private TextMeshPro equationText;
    [SerializeField] private TextMeshPro stat;
    [SerializeField] private List<LevelThreeTargetScript> answerTexts;

    private void Start()
    {
        SetStat();
    }
    private void SetStat()
    {
        if (database.GetComponent<DataBaseScript>().levelThreeTotalEquations > 0)
        {
            stat.text = "Accuracy: " + ((database.GetComponent<DataBaseScript>().levelThreeCorrectAnswer / database.GetComponent<DataBaseScript>().levelThreeTotalEquations) * 100).ToString();
        }
        else
        {
            stat.text = "Check this out";
        }
    }
    public void StartGame()
    {
        counter = rounds;
        StartCoroutine(WaitAndSetEquation());
    }
    public IEnumerator WaitAndSetEquation()
    {
        yield return new WaitForSeconds(1f);
        SetEquation();
    }
    public void SetEquation()
    {
        counter--;
        for (int i = 0; i < answerTexts.Count; i++)
        {
            answerTexts[i].gameObject.SetActive(true);
            answerTexts[i].particle.gameObject.SetActive(false);
        }
        if (counter <= 0)
        {
            equationText.text = "You Won";
            SetStat();
            startLevel.SetActive(true);

            for (int i = 0; i < answerTexts.Count; i++)
            {
                answerTexts[i].targetText.text = "O";
                answerTexts[i].isCorrectAnswer = false;
            }
            return;
        }
        database.GetComponent<DataBaseScript>().levelThreeTotalEquations++;
        database.GetComponent<DataBaseScript>().levelThreeCorrectAnswer++;
        for (int j = 0; j < answerTexts.Count; j++)
        {
            answerTexts[j].gameObject.GetComponent<Collider>().enabled = true;
        }
        var (equation, correctAnswer, answers) = EquationGenerator.Generate(Random.Range(1, 5));
        if (equationText != null)
        {
            equationText.text = equation;
        }
        for (int i = 0; i < answers.Count; i++)
        {
            answerTexts[i].targetText.text = answers[i].ToString();
            answerTexts[i].isCorrectAnswer = false;
            if (correctAnswer.Contains(answers[i]))
            {
                answerTexts[i].isCorrectAnswer = true;
            }
        }
    }
    public void CheckAnswers()
    {
        bool isEnd = true;
        for (int i = 0; i < answerTexts.Count; i++)
        {
            if (answerTexts[i].targetText.text == "O")
            {
                return;
            }
            if (!answerTexts[i].gameObject.activeSelf)
            {
                if (!answerTexts[i].isCorrectAnswer)
                {
                    counter = rounds;
                    for (int j = 0; j < answerTexts.Count; j++)
                    {
                        answerTexts[j].gameObject.GetComponent<Collider>().enabled = false;
                        answerTexts[j].targetText.text = "X";
                    }
                    database.GetComponent<DataBaseScript>().levelThreeCorrectAnswer--;
                    StartCoroutine(WaitAndSetEquation());
                }
            }
            if (answerTexts[i].gameObject.activeSelf && answerTexts[i].isCorrectAnswer)
            {
                isEnd = false;
            }
        }
        if (isEnd)
        {
            StartCoroutine(WaitAndSetEquation());
        }
    }
}
