using C__Project.Models;

// ქართული და რუსული ტექსტის წასაკითხად.
Console.InputEncoding = System.Text.Encoding.UTF8;
Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Airchiet programa:");
Console.WriteLine("1 - Calculator");
Console.WriteLine("2 - GuessNumber");
Console.WriteLine("3 - Hangman");
Console.WriteLine("4 - Translator");
Console.WriteLine("5 - ATM");
Console.WriteLine("6 - Studentebis Martva");
Console.WriteLine("7 - GASVLA");

string choice  = Console.ReadLine();
if (!Console.IsOutputRedirected)
{
    Console.Clear();
}
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

    case "4":
        Translator.Run();
        return;

    case "5":
        ATM atm = new ATM();
        atm.Run();
        return;

    case "6":
        StudentManagement studentManagement = new StudentManagement();
        studentManagement.Run();
        return;

    case "7":
        Console.WriteLine("Programa Dasrulda");
        break;

}





