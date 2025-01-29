using System;
using System.Collections.Generic;

public class MathGenerator
{
    // Enum to define the difficulty levels
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

        // Randomly choose an operator
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
                while (num2 == 0) num2 = rand.Next(1, 51); // Avoid division by zero
                correctAnswer = num1 / num2;
                break;
        }

        string equation = $"{num1} {operatorSymbol} {num2}";

        // Generate incorrect answers close to the correct answer
        List<int> answers = new List<int> { correctAnswer };
        for (int i = 1; i < numOfChoices; i++)
        {
            int wrongAnswer;
            do
            {
                // Generate wrong answers that are close to the correct answer
                wrongAnswer = correctAnswer + rand.Next(-5, 6); // This generates a wrong answer close to the correct answer, within ±5
            } while (wrongAnswer == correctAnswer || answers.Contains(wrongAnswer)); // Ensure the wrong answer isn't the same as the correct one or already in the list

            answers.Add(wrongAnswer);
        }

        // Shuffle the list of answers to randomize the order
        for (int i = 0; i < answers.Count; i++)
        {
            int temp = answers[i];
            int randomIndex = rand.Next(i, answers.Count);
            answers[i] = answers[randomIndex];
            answers[randomIndex] = temp;
        }

        return (equation, correctAnswer, answers);
    }

    // Example usage:
    public static string Main()
    {
        var (equation, correctAnswer, answers) = GenerateEquation(Difficulty.Normal, 4);
        var str = "";
        str += ($"Equation: {equation}\n");
        str += ($"Correct Answer: {correctAnswer}\n");
        str += ("Answers:\n");
        foreach (var answer in answers)
        {
            str += (answer) + "\n";
        }
        return str;
    }
}
