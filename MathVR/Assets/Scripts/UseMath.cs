using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;                     // Needed for TextMeshPro
using System.Collections.Generic;

public class AButtonAction : MonoBehaviour
{
    // The InputActionReference for your A button press
    public InputActionReference aButtonAction;

    // TextMeshPro for the equation
    [SerializeField] private TextMeshPro equationText;

    // TextMeshPro objects for answers 
    [SerializeField] private List<TextMeshPro> answerTexts;

    private void OnEnable()
    {
        // Subscribe to the performed event (A button pressed)
        aButtonAction.action.performed += OnAPressed;
        aButtonAction.action.Enable();
    }

    private void OnDisable()
    {
        // Unsubscribe
        aButtonAction.action.performed -= OnAPressed;
        aButtonAction.action.Disable();
    }

    private void OnAPressed(InputAction.CallbackContext ctx)
    {
        // Example: 4 total answers, one correct and 3 near misses
        var (equation, correctAnswer, answers) = MathGenerator.GenerateEquation(
            MathGenerator.Difficulty.Normal,
            numOfChoices: 4
        );

        // 1) Display the equation
        if (equationText != null)
        {
            equationText.text = $"Solve: {equation}";
        }

        // 2) Display answers in each TextMeshPro
        for (int i = 0; i < answers.Count; i++)
        {
            if (i < answerTexts.Count && answerTexts[i] != null)
            {
                answerTexts[i].text = answers[i].ToString();
            }
        }

        // Optional debug in the console
        Debug.Log($"Equation: {equation} = {correctAnswer}  |  Answers: {string.Join(", ", answers)}");
    }
}
