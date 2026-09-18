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
        Random random = new Random();
        int randNums = random.Next(1, 101);
        int attempts = 0;
        int userGuessNum;
        //int maxAttemptForEasy = 5;
        //int maxAttemptForMedium = 10;
        //int maxAttemptForHard = 15;
        Console.WriteLine("Tqven unda gamoicnot ricxvi 1- dan 100-mde");

        do
        {
            Console.Write("Sheiyvanet ricxvi: ");

            if(!int.TryParse(Console.ReadLine(), out userGuessNum))
            {
                Console.WriteLine("Sheiyvanet swori ricxvi");
                continue;
            } 

            attempts++;

            if(userGuessNum < randNums)
            {
                Console.WriteLine("Scadet ufro magali ricxvi");
            } else if (userGuessNum > randNums)
            {
                Console.WriteLine("Scadet ufro dabali ricxvi");
            } else
            {
                Console.WriteLine($"Tqven gamoicanit ricxvi: {randNums}");
                Console.WriteLine($"Mcdelobebis raodenoba: {attempts}");
            }
           


        }

        while (userGuessNum != randNums);


    }

}
