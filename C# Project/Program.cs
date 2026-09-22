using C__Project.Models;

Console.WriteLine("Airchiet programa:");
Console.WriteLine("1 - Calculator");
Console.WriteLine("2 - GuessNumber");
Console.WriteLine("3 - Hangman");
Console.WriteLine("4 - Translator");
Console.WriteLine("5 - Bankomat");

Console.WriteLine("6 - Exit");

string choice  = Console.ReadLine();
Console.Clear();
switch (choice)
{
    case "1": Calculator.RunCalc();
        return;

    case "2": GuessNumber.GuessPlay();
        return;

    case "3":
        Hungman hangman = new Hungman();
        hangman.GuessPlay();
        return;


    case "6":
        Console.WriteLine("Programa Dasrulda");
        break;

}




