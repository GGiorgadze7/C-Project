//2.1 კალკულატორი
//კალკულატორის ფუნქცია მომხმარებლებს საშუალებას აძლევს შეასრულონ ძირითადი 
//არითმეტიკული მოქმედებები (+, -, *, /). მომხმარებლებს შეუძლიათ შეიყვანონ ორი რიცხვი და 
//შემდეგ აირჩიონ ოპერაცია შედეგის მისაღებად. კალკულატორი ასევე შეიცავს შეყვანის 
//ვალიდაციას არასწორი შეყვანების დასამუშავებლად. 

using System.Diagnostics;
using System.Xml;

namespace C__Project.Models
{
    public class Calculator
    {
        //არითმეტიკული მოქმედებების ფუნქცია.
        static double calcFunct(double num1, double num2, string operation)
        {
            switch (operation)
            {
                case "+":
                    return num1 + num2;

                case "-":
                    return num1 - num2;

                case "*":
                    return num1 * num2;

                case "/":
                    //ვამოწმებთ ნულზე გაყოფას.

                    if (num2 == 0)
                    {
                        throw new DivideByZeroException("Nulze gayofa ar sheidzleba!");
                    }
                    else
                    {
                        return num1 / num2;
                    }
                default:
                    throw new ArgumentException("Moxda shecdoma!");

            }

        }

            //რიცხვის შემოყვანა მომხმარებლისგან.
            //While ციკლი.

            static void Main(string[] args)
            {
                bool countineOperation = true; // სანამ ეს true იქნება ციკლი არ დამთავრდება.

                while (countineOperation)
                {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("sheiyvanet pirveli ricxvi: ");
                    string inp1 = Console.ReadLine();
              
                    Console.WriteLine("Sheiyvanet meore ricxvi: ");
                    string inp2 = Console.ReadLine();

                    Console.WriteLine("Airchiet aritmetikuli moqmedeba: (+,-,*,/)");
                    string operation = Console.ReadLine();

                    //აქ ვაკეთებთ TryParse რაც ორივე რიცხვს ამოწმებს ერთდროულად.

                    if (double.TryParse(inp1, out double num1) && double.TryParse(inp2, out double num2))
                    {
                        try
                        {

                            double result = calcFunct(num1, num2, operation);
                            Console.ForegroundColor= ConsoleColor.Green;
                            Console.WriteLine($"Shedegi: {result} ");
                        Console.WriteLine("--------------------");
                        }

                        catch (Exception ex)
                        {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Shecdoma {ex.Message}");
                        }


                    } else
                    {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Sheiyvanet ricxvebi sworad!");
                    }

                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("Kidev gnebavt gamotvla? (Y/N) ");
                    string answer = Console.ReadLine();

                    if(answer.ToLower() != "y")
                    {
                        countineOperation = false;
                    }

                }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Programa dasrulda!");
            }
           
        }

    }

