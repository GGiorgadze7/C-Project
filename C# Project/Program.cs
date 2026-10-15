using C__Project.Models;

// ქართული და რუსული ტექსტის მხარდაჭერა პირველივე შეყვანამდე.
Console.InputEncoding = System.Text.Encoding.UTF8;
Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Airchiet programa:");
Console.WriteLine("1 - Calculator");
Console.WriteLine("2 - GuessNumber");
Console.WriteLine("3 - Hangman");
Console.WriteLine("4 - Translator");
Console.WriteLine("5 - Bankomat");

Console.WriteLine("6 - Exit");

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


    // მენიუს მეოთხე პუნქტი თარჯიმანს უშვებს.
    case "4":
        Translator.Run();
        return;

    case "5":
        ATM atm = new ATM();
        atm.Run();
        return;

    case "6":
        Console.WriteLine("Programa Dasrulda");
        break;

}




