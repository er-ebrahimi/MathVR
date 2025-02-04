using System;
using System.Collections.Generic;

public class MathGenerator
{
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }
    public static (string, int, List<int>) GenerateEquation(Difficulty difficulty, int numOfChoices)
    {
        Random rand = new Random();
        int num1 = 0, num2 = 0;
        string operatorSymbol = "";
        int correctAnswer = 0;

        switch (difficulty)
        {
            case Difficulty.Easy:
                num1 = rand.Next(1, 11);
                num2 = rand.Next(1, 11);
                break;

            case Difficulty.Normal:
                num1 = rand.Next(1, 21);
                num2 = rand.Next(1, 21);
                break;

            case Difficulty.Hard:
                num1 = rand.Next(1, 51);
                num2 = rand.Next(1, 51);
                break;
        }
        int operatorChoice = rand.Next(1, 5);

        switch (operatorChoice)
        {
            case 1:
                operatorSymbol = "+";
                correctAnswer = num1 + num2;
                break;
            case 2:
                operatorSymbol = "-";
                correctAnswer = num1 - num2;
                break;
            case 3:
                operatorSymbol = "*";
                correctAnswer = num1 * num2;
                break;
            case 4:
                operatorSymbol = "/";
                while (num2 == 0) num2 = rand.Next(1, 51);
                correctAnswer = num1 / num2;
                break;
        }
        string equation = $"{num1} {operatorSymbol} {num2}";
        List<int> answers = new List<int> { correctAnswer };
        for (int i = 1; i < numOfChoices; i++)
        {
            int wrongAnswer;
            do
            {
                wrongAnswer = correctAnswer + rand.Next(-5, 6);
            } while (wrongAnswer == correctAnswer || answers.Contains(wrongAnswer));

            answers.Add(wrongAnswer);
        }
        for (int i = 0; i < answers.Count; i++)
        {
            int temp = answers[i];
            int randomIndex = rand.Next(i, answers.Count);
            answers[i] = answers[randomIndex];
            answers[randomIndex] = temp;
        }
        return (equation, correctAnswer, answers);
    }
}
