using C__Project.Models;

Console.WriteLine("Airchiet programa:");
Console.WriteLine("1 - Calculator");
string choice  = Console.ReadLine();
Console.Clear();
switch (choice)
{
    case "1": Calculator.RunCalc();
        return;
}




