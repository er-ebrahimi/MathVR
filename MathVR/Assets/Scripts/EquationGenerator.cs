using System;
using System.Collections.Generic;
using UnityEditor.Rendering;

class EquationGenerator
{
    static Random random = new Random();
    static int min = 10;
    static int max = 100;
    static string question;
    static List<int> options = new List<int>();
    static List<int> correctOptions = new List<int>();

    public static (string, List<int>, List<int>) Generate(int choice)
    {
        question = null;
        options.Clear();
        correctOptions.Clear();
        switch (choice)
        {
            case 1:
                GenerateOddQuestion();
                break;
            case 2:
                GenerateEvenQuestion();
                break;
            case 3:
                GenerateGreaterThanQuestion();
                break;
            case 4:
                GenerateLessThanQuestion();
                break;
        }
        return (question, correctOptions, options);
    }
    static List<int> GenerateDistinctRandomNumbers(int count, int min, int max)
    {
        HashSet<int> numbers = new HashSet<int>();
        while (numbers.Count < count)
        {
            int num = random.Next(min, max + 1);
            numbers.Add(num);
        }
        return new List<int>(numbers);
    }
    static void GenerateOddQuestion()
    {
        question = "odd numbers";
        List<int> numbers = GenerateDistinctRandomNumbers(5, min, max);
        for (int i = 0; i < numbers.Count; i++)
        {
            options.Add(numbers[i]);
        }
        for (int i = 0; i < numbers.Count; i++)
        {
            if (numbers[i] % 2 != 0)
                correctOptions.Add(numbers[i]);
        }
        if (correctOptions.Count <= 0) 
        {
            numbers[2] = 15;
            correctOptions.Add(15);
        }
    }
    static void GenerateEvenQuestion()
    {
        List<int> numbers = GenerateDistinctRandomNumbers(5, min, max);
        question = "even numbers";
        for (int i = 0; i < numbers.Count; i++)
        {
            options.Add(numbers[i]);
        }
        for (int i = 0; i < numbers.Count; i++)
        {
            if (numbers[i] % 2 == 0)
                correctOptions.Add(numbers[i]);
        }
        if (correctOptions.Count <= 0)
        {
            numbers[2] = 34;
            correctOptions.Add(34);
        }
    }
    static void GenerateGreaterThanQuestion()
    {
        int x = random.Next(min, max);
        int countGreater = random.Next(1, 5);
        int countSmaller = 5 - countGreater;
        List<int> greaterNumbers = GenerateDistinctRandomNumbers(countGreater, x + 1, x + 20);
        List<int> smallerNumbers = GenerateDistinctRandomNumbers(countSmaller, x - 20, x - 1);
        options.AddRange(greaterNumbers);
        options.AddRange(smallerNumbers);
        Shuffle(options);

        question = "greater than " + x.ToString();
        for (int i = 0; i < options.Count; i++)
        {
            if (options[i] > x)
                correctOptions.Add(options[i]);
        }
        if (correctOptions.Count <= 0)
        {
            options[2] = x + 5;
            correctOptions.Add(x + 5);
        }
    }
    static void GenerateLessThanQuestion()
    {
        int x = random.Next(min, max);
        int countSmaller = random.Next(1, 5);
        int countGreater = 5 - countSmaller;
        List<int> smallerNumbers = GenerateDistinctRandomNumbers(countSmaller, x - 20, x - 1);
        List<int> greaterNumbers = GenerateDistinctRandomNumbers(countGreater, x + 1, x + 20);
        options.AddRange(smallerNumbers);
        options.AddRange(greaterNumbers);
        Shuffle(options);
        question = "less than " + x.ToString();
        for (int i = 0; i < options.Count; i++)
        {
            if (options[i] < x)
                correctOptions.Add(options[i]);
        }
        if (correctOptions.Count <= 0)
        {
            options[2] = x - 5;
            correctOptions.Add(x - 5);
        }
    }
    static void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = 0; i < n; i++)
        {
            int r = i + random.Next(n - i);
            T temp = list[r];
            list[r] = list[i];
            list[i] = temp;
        }
    }
}
