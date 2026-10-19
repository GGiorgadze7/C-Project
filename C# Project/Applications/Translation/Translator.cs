using System.Text;

namespace C__Project.Models;

public class Translator
{
    // ლექსიკონებიც სხვა მონაცემებთან ერთად საერთო Data საქაღალდეში ინახება.
    private static string folder = DataStorage.Folder;
    private static string englishFile = Path.Combine(folder, "georgian-english.txt");
    private static string russianFile = Path.Combine(folder, "georgian-russian.txt");

    // ამ მეთოდიდან იწყება თარჯიმნის მუშაობა.
    public static void Run()
    {
        try
        {
            CreateDictionaries();
            Console.WriteLine($"Leqsikonebis saqaghalde: {folder}");

            // მენიუ მეორდება, სანამ მომხმარებელი გასვლას არ აირჩევს.
            while (true)
            {
                Console.WriteLine("\nTranslator");
                Console.WriteLine("1 - Qartulidan inglisurze");
                Console.WriteLine("2 - Inglisuridan qartulze");
                Console.WriteLine("3 - Qartulidan rusulze");
                Console.WriteLine("4 - Rusulidan qartulze");
                Console.WriteLine("0 - Gasvla");
                string? choice = Console.ReadLine();

                // ერთი ფაილი ორივე მიმართულებით თარგმნისთვის გამოიყენება.
                switch (choice)
                {
                    case "1": Translate(englishFile, false); break;
                    case "2": Translate(englishFile, true); break;
                    case "3": Translate(russianFile, false); break;
                    case "4": Translate(russianFile, true); break;
                    case "0":
                    case null: return;
                    default: Console.WriteLine("Airchiet 0-dan 4-mde ricxvi."); break;
                }
            }
        }
        catch (IOException)
        {
            Console.WriteLine("Leqsikonis failis wakitxva an shenaxva ver moxerxda.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Leqsikonis failze wvdomis ufleba ar aris.");
        }
    }

    // ფაილები პირველად იქმნება რამდენიმე მზა სიტყვითა და მოკლე ფრაზით.
    private static void CreateDictionaries()
    {
        Directory.CreateDirectory(folder);

        // არსებული ფაილი არ იცვლება, რომ დამატებული სიტყვები არ დაიკარგოს.
        if (!File.Exists(englishFile))
        {
            string[] words =
            {
                "გამარჯობა|hello",
                "მადლობა|thank you",
                "ნახვამდის|goodbye",
                "კატა|cat",
                "სახლი|house",
                "დილა მშვიდობისა|good morning"
            };
            File.WriteAllLines(englishFile, words, Encoding.UTF8);
        }

        if (!File.Exists(russianFile))
        {
            string[] words =
            {
                "გამარჯობა|привет",
                "მადლობა|спасибо",
                "ნახვამდის|до свидания",
                "კატა|кошка",
                "სახლი|дом",
                "დილა მშვიდობისა|доброе утро"
            };
            File.WriteAllLines(russianFile, words, Encoding.UTF8);
        }
    }

    // reverse = false: ქართულიდან თარგმნა; true: ქართულზე თარგმნა.
    private static void Translate(string filePath, bool reverse)
    {
        Console.Write("Sheiyvanet sityva an mokle fraza: ");
        string word = (Console.ReadLine() ?? "").Trim();
        if (!IsValidText(word)) return;

        // ვკითხულობთ ფაილის ყველა ხაზს და სათითაოდ ვეძებთ სიტყვას.
        string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
        foreach (string line in lines)
        {
            // მაგალითად: გამარჯობა|hello იყოფა ორ ნაწილად.
            string[] parts = line.Split('|');
            if (parts.Length != 2) continue;

            string original = parts[0].Trim();
            string translation = parts[1].Trim();
            if (reverse)
            {
                // უკუმიმართულებით ძებნისას სიტყვასა და თარგმანს ადგილებს ვუცვლით.
                original = parts[1].Trim();
                translation = parts[0].Trim();
            }

            // ინგლისური სიტყვის დიდი და პატარა ასოები ერთნაირად აღიქმება.
            if (word.Equals(original, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Targmani: {translation}");
                return;
            }
        }

        // თუ სიტყვა ვერ მოიძებნა, მომხმარებელს შეუძლია თავად დაამატოს თარგმანი.
        Console.WriteLine("Sityva leqsikonshi ar aris. Daamatebt targmans? 1 - Ki, 0 - Ara");
        if (Console.ReadLine() != "1") return;

        Console.Write("Sheiyvanet targmani archeul enaze: ");
        string newTranslation = (Console.ReadLine() ?? "").Trim();
        if (!IsValidText(newTranslation)) return;

        // ფაილის მარცხენა სვეტში ყოველთვის ქართული ტექსტი ინახება.
        string newLine;
        if (reverse)
            newLine = newTranslation + "|" + word;
        else
            newLine = word + "|" + newTranslation;

        // AppendAllText ამატებს ახალ ხაზს ისე, რომ ძველი სიტყვები არ წაიშალოს.
        // დამატებითი ხაზის გამყოფი ხელით რედაქტირებული ფაილის შემთხვევაშიც გამოგვადგება.
        File.AppendAllText(filePath, Environment.NewLine + newLine + Environment.NewLine, Encoding.UTF8);
        Console.WriteLine("Axali targmani shenaxulia.");
    }

    // ცარიელ ტექსტსა და ფაილის სვეტების გამყოფ სიმბოლოს არ ვიღებთ.
    private static bool IsValidText(string text)
    {
        if (string.IsNullOrWhiteSpace(text) || text.Contains('|'))
        {
            Console.WriteLine("Sheiyvanet teqsti da ar gamoiyenot simbolo |.");
            return false;
        }
        return true;
    }
}
