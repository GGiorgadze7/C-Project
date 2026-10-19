//2.2 თამაში 1: გამოიცანით რიცხვი 
//ამ თამაშში პროგრამა აგენერირებს შემთხვევით რიცხვს მითითებული დიაპაზონიდან. 
//მომხმარებლებს სთხოვენ გამოიცნონ რიცხვი. არასწორი რიცხვის შემთხვევაში პროგრამა 
//მომხმარებელს აძლევს მინიშნებას (უფრო მაღალი/უფრო დაბალი). თამაში აკონტროლებს 
//მცდელობების რაოდენობას და აჩვენებს შედეგს, როდესაც მომხმარებელი გამოიცნობს სწორ 
//რიცხვს.


namespace C__Project.Models;

public class GuessNumber
{
    public static void GuessPlay()
    {
        Console.WriteLine("Airchiet sirtule:");
        Console.WriteLine("1 - Martivi (15 mcdeloba)");
        Console.WriteLine("2 - Sashualo (10 mcdeloba)");
        Console.WriteLine("3 - Rtuli (5 mcdeloba)");

        int maxAttempts;
        while (true)
        {
            Console.Write("Tqveni archevani: ");
            string? choice = Console.ReadLine();
            if (choice == null)
            {
                return;
            }

            maxAttempts = choice switch
            {
                "1" => 15,
                "2" => 10,
                "3" => 5,
                _ => 0
            };

            if (maxAttempts > 0)
            {
                break;
            }

            Console.WriteLine("Airchiet 1, 2 an 3");
        }

        Random random = new Random();
        int randNums = random.Next(1, 101);
        int attempts = 0;
        Console.WriteLine("Tqven unda gamoicnot ricxvi 1-dan 100-mde");

        while (attempts < maxAttempts)
        {
            Console.WriteLine($"Darchenili mcdelobebi: {maxAttempts - attempts}");
            Console.Write("Sheiyvanet ricxvi: ");
            string? input = Console.ReadLine();
            if (input == null)
            {
                return;
            }

            if (!int.TryParse(input, out int userGuessNum) || userGuessNum < 1 || userGuessNum > 100)
            {
                Console.WriteLine("Sheiyvanet swori ricxvi 1-dan 100-mde");
                continue;
            }

            attempts++;

            if (userGuessNum == randNums)
            {
                Console.WriteLine($"Tqven gamoicanit ricxvi: {randNums}");
                Console.WriteLine($"Mcdelobebis raodenoba: {attempts}");
                return;
            }

            if (attempts < maxAttempts)
            {
                Console.WriteLine(userGuessNum < randNums
                    ? "Scadet ufro magali ricxvi"
                    : "Scadet ufro dabali ricxvi");
            }
        }

        Console.WriteLine($"Mcdelobebi amoitsura! Swori ricxvi iyo: {randNums}");
    }
}
