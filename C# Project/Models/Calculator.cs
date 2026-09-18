//2.1 კალკულატორი
//კალკულატორის ფუნქცია მომხმარებლებს საშუალებას აძლევს შეასრულონ ძირითადი 
//არითმეტიკული მოქმედებები (+, -, *, /). მომხმარებლებს შეუძლიათ შეიყვანონ ორი რიცხვი და 
//შემდეგ აირჩიონ ოპერაცია შედეგის მისაღებად. კალკულატორი ასევე შეიცავს შეყვანის 
//ვალიდაციას არასწორი შეყვანების დასამუშავებლად. 

using System.Diagnostics;
using System.Xml;

namespace C__Project.Models;

public class Calculator
{
    
    public static void RunCalc()
    {
        bool calcOnOF = true;


        while(calcOnOF)
        {
            Console.WriteLine("CALCULATOR");
            Console.WriteLine("-------------------------------");

            Console.WriteLine("Sheiyvanet pirveli ricxvi:");

            double num1;
            while(!double.TryParse(Console.ReadLine(), out num1))
            {
                Console.WriteLine("Sheiyvanet pirveli ricxvi!");
            }
         

            Console.WriteLine("Sheiyvanet meore ricxvi:");
            double num2;
            while (!double.TryParse(Console.ReadLine(), out num2))
            {
                Console.WriteLine("Sheiyvanet meore ricxvi!");
            }


            try
            {
                Console.WriteLine("Chaweret Pirobiti operatori: (+, -, *, /)");
                string action = Console.ReadLine();
                if (action != "+" && action != "-" && action != "*" && action != "/")
                {
                    throw new ArgumentException("Sheiyvanet swori pirobiti operatori");
                }

                switch (action)
                {

                    case "+":
                        Console.WriteLine($"Shedegi: {num1 + num2}");
                        break;

                    case "-":
                        Console.WriteLine($"Shedegi: {num1 - num2}");
                        break;

                    case "*":
                        Console.WriteLine($"Shedegi: {num1 * num2}");
                        break;

                    case "/":
                        if (num2 == 0)
                        {
                            throw new DivideByZeroException();
                        }
                        else
                        {
                            Console.WriteLine($"Shedegi: {num1 / num2}");
                        }

                        break;
                }

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                continue;

             
            } catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            } 
            

            Console.WriteLine("Gnebavt kidev gamotvla? (Y/N)");
            string countineOrNot = Console.ReadLine();
            
            if(  countineOrNot.ToLower() == "n")
            {
                calcOnOF = false;
                Console.WriteLine("Programa dasrulda");
            }




        }




    }

        



}