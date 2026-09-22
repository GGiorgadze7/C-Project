//2.1 კალკულატორი
//კალკულატორის ფუნქცია მომხმარებლებს საშუალებას აძლევს შეასრულონ ძირითადი 
//არითმეტიკული მოქმედებები (+, -, *, /). მომხმარებლებს შეუძლიათ შეიყვანონ ორი რიცხვი და 
//შემდეგ აირჩიონ ოპერაცია შედეგის მისაღებად. კალკულატორი ასევე შეიცავს შეყვანის 
//ვალიდაციას არასწორი შეყვანების დასამუშავებლად. 

using System.Diagnostics;


namespace C__Project.Models;

public class Calculator
{
    
    public static void RunCalc()
    {
        bool calcOnOF = true;


        while(calcOnOF)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("CALCULATOR");
            Console.WriteLine("-------------------------------");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sheiyvanet pirveli ricxvi:");
            
            Console.ResetColor();

            double num1;
            while(!double.TryParse(Console.ReadLine(), out num1))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sheiyvanet pirveli ricxvi!");
                Console.ResetColor();
            }


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sheiyvanet meore ricxvi:");
            Console.ResetColor();

            double num2;
            while (!double.TryParse(Console.ReadLine(), out num2))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sheiyvanet meore ricxvi!");
                Console.ResetColor();
            }


            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Chaweret Pirobiti operatori: (+, -, *, /)");
                Console.ResetColor();

                string action = Console.ReadLine();
                if (action != "+" && action != "-" && action != "*" && action != "/")
                {
                    throw new ArgumentException("Sheiyvanet swori pirobiti operatori");
                }

                switch (action)
                {

                    case "+":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Shedegi: {num1 + num2}");
                        Console.ResetColor();
                        
                        break;

                    case "-":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Shedegi: {num1 - num2}");
                        Console.ResetColor();
                        break;

                    case "*":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Shedegi: {num1 * num2}");
                        Console.ResetColor();
                        break;

                    case "/":
                        if (num2 == 0)
                        {
                            throw new DivideByZeroException();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Shedegi: {num1 / num2}");
                            Console.ResetColor();
                        }

                        break;
                       


                }

            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                continue;


            } catch (DivideByZeroException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                continue;
            }
            

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Gnebavt kidev gamotvla? (Y/N)");
            Console.ResetColor();

            string countineOrNot = Console.ReadLine();

            if(  countineOrNot.ToLower() == "n")
            {
                calcOnOF = false;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Programa dasrulda");
                Console.ResetColor();
                
            } else
            {
                Console.Clear();
            }




        }




    }

        



}