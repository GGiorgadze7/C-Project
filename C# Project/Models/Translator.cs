//2.4 თარჯიმანი
//თარჯიმანის აპლიკაცია მომხმარებელს საშუალებას აძლევს აირჩიოს ენების წყვილი - რომელი 
//ენიდან რომელ ენეაზე უნდა ითარგმნოს სიტყვები (მაგ: ქართული - ინგლისური, ქართული
//რუსული, ინგლისური-ქართული, რუსული-ქართული). ამის შემდეგ მომხმარებელს  შეყავს 
//სიტყვა ან მოკლე ფრაზა. პროგრამა კითხულობს თარგმანებს ტექსტური ფაილიდან 
//(ლექსიკონიდან) და აჩვენებს შესაბამის ნათარგმნ სიტყვას ან ფრაზას. იმ შემთხვევაში თუ 
//სათარგმნი სიტყვა არ აღმოჩნდა ლექსიკონში, პროგრამა მომხმარებელს სთავაზობს დაამატოს 
//ამ ახალი სიტყვის თარგმანი ლექსიკონს (ტექსტურ ფაილს). 

namespace C__Project.Models;

using System.IO;

public class Translator
{
    public static void Run()
    {
        bool triggerForLoop = true;

        

        
        



        while (triggerForLoop)
        {
            Console.WriteLine("Translator");
            Console.WriteLine("1 - Targmna: qartulidan inglisurze");
            Console.WriteLine("2 - Targmna: inglisuridan qartulze");
            Console.WriteLine("3 - Targmna: qartulidan rusulze");
            Console.WriteLine("4 - Targmna: rusulidan qartulze");

            string choice = Console.ReadLine();

            //switch (choice)
            //{
            //    case "1": 
            //}

        }



    }

}
