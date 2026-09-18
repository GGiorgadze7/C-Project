using C__Project.Models;

Console.WriteLine("Airchiet programa:");
Console.WriteLine("1 - Calculator");
Console.WriteLine("2 - GuessNumber");

string choice  = Console.ReadLine();
Console.Clear();
switch (choice)
{
    case "1": Calculator.RunCalc();
        return;

    case "2": GuessNumber.GuessPlay();
        return;
}




