using System;
using System.Collections.Generic;

class Hangman
{
    private static string[] words = { "бмв", "мерседес", "ауди", "киа", "шевроле" };
    private string secretWord;
    private char[] guessedLetters;
    private int attemptsLeft;
    private HashSet<char> guessedChars = new HashSet<char>();

    public Hangman() 
    {
        Random random = new Random();
        secretWord = words[random.Next(words.Length)];
        guessedLetters = new char[secretWord.Length];
        for(int i = 0; i < guessedLetters.Length; i++)
        {
            guessedLetters[i] = '_';
        }
        attemptsLeft = 6;
    }

    public void Play()
    {
        Console.WriteLine("Добро пожаловать в игу 'Виселица'!");
        Console.WriteLine($"У вас есть {attemptsLeft} попыток, чтобы угадать слово");

        while (attemptsLeft > 0 && !IsWordGuessed())
        {
            DisplayGameState();
            Console.Write("Введите букву: ");
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) || input.Length != 1)
            {
                Console.WriteLine("Пожалуйста, напишите одну букву");
                continue;
            }
            char guess = input[0];
            if(!char.IsLetter(guess))
            {
                Console.WriteLine("Пожалуйста, введите букву");
                continue;
            }
            ProcessGuess(guess);
        }
        if (IsWordGuessed())
        {
            Console.WriteLine($"\nПоздравляю! Вы угадали слово: {secretWord}");
        }
        else
        {
            Console.WriteLine($"\nИгра окончена! Вы проиграли. Загаданное слово: {secretWord}");
        }
    }
    private void DisplayGameState()
    {
        Console.WriteLine("\n" + string.Join(" ", guessedLetters));
        Console.WriteLine($"Осталось попыток: {attemptsLeft}");
        Console.WriteLine($"Использованные буквы: {string.Join(", ", guessedChars)}");
        DrawHangman();
    }
    private void ProcessGuess(char guess)
    {
        if(guessedChars.Contains(guess))
        {
            Console.WriteLine("Вы уже вводили эту букву");
            return;
        }
        guessedChars.Add(guess);

        bool correctGuess = false;
        for (int i = 0; i < secretWord.Length; i++)
        {
            if (secretWord[i] == guess)
            {
                guessedLetters[i] = guess;
                correctGuess = true;
            }
        }
        if (!correctGuess)
        {
            attemptsLeft--;
            Console.WriteLine("Такой буквы в этом слове нету");
        }
        else
        {
            Console.WriteLine("Правильная буква, продолжай!");
        }
    }
    private bool IsWordGuessed()
    {
        foreach (char c in guessedLetters)
        {
            if (c == '_') return false;
        }
        return true;
    }
    private void DrawHangman()
    {
        string[] hangmanStages = {
            @"
              _______
              |     |
              |
              |
              |
              |
            =========",
            @"
              _______
              |     |
              |     O
              |
              |
              |
            =========",
            @"
              _______
              |     |
              |     O
              |     |
              |
              |
            =========",
            @"
              _______
              |     |
              |     O
              |    /|
              |
              |
            =========",
            @"
              _______
              |     |
              |     O
              |    /|\
              |
              |
            =========",
            @"
              _______
              |     |
              |     O
              |    /|\
              |    /
              |
            =========",
            @"
              _______
              |     |
              |     O
              |    /|\
              |    / \
              |
            ========="
        };
        int stage = 6 -attemptsLeft;
        if (stage >= 0 && stage < hangmanStages.Length)
        {
            Console.WriteLine(hangmanStages[stage]);
        }

    }
}
class Program
{
    static void Main(string[] args)
    {
        Hangman game = new Hangman();
        game.Play();
        Console.WriteLine("Нажми любую клавишу для выхода");
        Console.ReadKey();
    }
}