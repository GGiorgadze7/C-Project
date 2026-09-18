//2.3 თამაში 2: Hangman
//Hangman არის სიტყვების გამოცნობის თამაში. პროგრამა ირჩევს შემთხვევით სიტყვას 
//წინასწარ განსაზღვრული სიიდან და აჩვენებს მას ქვედა ტირეების გამოყენებით (რამდენი 
//ასოცაა სიტყვაში, იმდენი ქვედა ტირე), რომელიც წარმოადგენს ფარულ ასოებს. 
//მომხმარებლებს სთხოვენ გამოიცნონ ასო და პროგრამა ამოწმებს არის თუ არა ასო სიტყვაში. 
//ვლინდება სწორად გამოცნობილი ასოები და თამაში გრძელდება მანამ, სანამ მომხმარებელი 
//არ გამოიცნობს სიტყვას ან არ ამოიწურება მცდელობები.


using System.Diagnostics.Tracing;


namespace C__Project.Models;

public class Hungman
{
    private readonly string[] words = { "akademia", "kompiuteri", "leqcia", "saqartvelo", "funqcia", "memkvidreoba", "gamocana" };

    public void GuessPlay()
    {
        Random random = new Random();
        string wordToGuess = words[random.Next(words.Length)];

        char[] guessedWord = new char[wordToGuess.Length];
        for (int i = 0; i < guessedWord.Length; i++)
            guessedWord[i] = '_';

        int maxAttempts = 6;
        int wrongAttempts = 0;
        List<char> guessedLetters = new List<char>();

        Console.WriteLine("gamoicanit sityva!");

        while (wrongAttempts < maxAttempts && new string(guessedWord) != wordToGuess)
        {
            Console.WriteLine();
            Console.WriteLine("sityva: " + string.Join(" ", guessedWord));
            Console.WriteLine($"araswori mcdelobebis raodenoba: {wrongAttempts}/{maxAttempts}");
            Console.WriteLine("nacadi asoebi: " + string.Join(", ", guessedLetters));
            Console.Write("sheiyvanet aso: ");

            string input = Console.ReadLine();

           
            if (string.IsNullOrEmpty(input) || input.Length != 1 || !char.IsLetter(input[0]))
            {
                Console.WriteLine("gtxovt sheiyvanot mxolod erti aso.");
                continue;
            }

            char guess = char.ToLower(input[0]);

           
            if (guessedLetters.Contains(guess))
            {
                Console.WriteLine("es aso ukve gamoiyenet.");
                continue;
            }

            guessedLetters.Add(guess);

            if (wordToGuess.Contains(guess))
            {
                
                for (int i = 0; i < wordToGuess.Length; i++)
                {
                    if (wordToGuess[i] == guess)
                        guessedWord[i] = guess;
                }
                Console.WriteLine("sworia!");
            }
            else
            {
                wrongAttempts++;
                Console.WriteLine("arasworia!");
            }
        }

        Console.WriteLine();
        if (new string(guessedWord) == wordToGuess)
            Console.WriteLine($"Gilocavt tqven gamoicanit sityva: {wordToGuess}");
        else
            Console.WriteLine($"Samwuxarod waaget, Swori sityva iyo: {wordToGuess}");
    }



}
