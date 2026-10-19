using System.Globalization;
using System.Text.Json;
using C__Project.Models.Enum;

namespace C__Project.Models;

public class ATM
{
    // პროგრამის მუშაობის დროს მომხმარებლები ამ სიაში ინახება.
    private List<UserForATM> users = new List<UserForATM>();

    // ფაილები დისკზე, პროგრამის გვერდით მდებარე Data საქაღალდეში შეიქმნება.
    private string dataFolder = DataStorage.Folder;
    private string usersFile = Path.Combine(DataStorage.Folder, "users.txt");
    private string loansFile = Path.Combine(DataStorage.Folder, "loans.txt");

    // ტვირთავს მონაცემებს და აჩვენებს ბანკომატის საწყის მენიუს.
    public void Run()
    {
        try
        {
            Directory.CreateDirectory(dataFolder);
            LoadUsers();
            CreateAdmin();
            SaveUsers();
            Console.WriteLine($"Monacemebis saqaghalde: {dataFolder}");
            while (true)
            {
                Console.WriteLine("\n1 - Registracia\n2 - Shesvla\n0 - Gasvla");
                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": Register(); break;
                    case "2": Login(); break;
                    case "0":
                    case null: return;
                    default: Console.WriteLine("Araswori archevani"); break;
                }
            }
        }
        catch (IOException)
        {
            Console.WriteLine("Failis wakitxva an shenaxva ver moxerxda. Bankomati daixura.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Failze wvdomis ufleba ar aris. Bankomati daixura.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Monacemebis failshi araswori chanaweria. Bankomati daixura.");
        }
        catch (JsonException)
        {
            Console.WriteLine("Monacemebis faili dazianebulia. Bankomati daixura.");
        }
    }

    // კითხულობს პირად მონაცემებს და ამატებს ახალ კლიენტს.
    private void Register()
    {
        Console.Write("Saxeli: ");
        string name = (Console.ReadLine() ?? "").Trim();
        Console.Write("Gvari: ");
        string lastname = (Console.ReadLine() ?? "").Trim();
        Console.Write("Momxmareblis saxeli: ");
        string username = (Console.ReadLine() ?? "").Trim();
        Console.Write("Paroli: ");
        string password = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(lastname)
            || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Yvela veli unda sheavsot.");
            return;
        }

        // ფაილში სვეტების გამყოფია |, ამიტომ მონაცემებში მას არ ვუშვებთ.
        if (name.Contains('|') || lastname.Contains('|') || username.Contains('|') || password.Contains('|'))
        {
            Console.WriteLine("Simbolo | ar gamoiyenot.");
            return;
        }

        // ერთნაირი მომხმარებლის სახელით ორი ანგარიში ვერ შეიქმნება.
        foreach (UserForATM user in users)
        {
            if (user.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Es momxmareblis saxeli ukve arsebobs.");
                return;
            }
        }

        UserForATM newUser = new UserForATM();
        newUser.Name = name;
        newUser.Lastname = lastname;
        newUser.Username = username;
        newUser.Password = password;
        // რეგისტრაციით მხოლოდ კლიენტის ანგარიში იქმნება.
        newUser.Role = UserRoleATM.client;
        users.Add(newUser);
        SaveUsers();
        Console.WriteLine("Registracia warmatebit dasrulda.");
    }

    // ამოწმებს სახელსა და პაროლს, შემდეგ ხსნის შესაბამისი როლის მენიუს.
    private void Login()
    {
        Console.Write("Momxmareblis saxeli: ");
        string username = (Console.ReadLine() ?? "").Trim();
        Console.Write("Paroli: ");
        string? password = Console.ReadLine();

        foreach (UserForATM user in users)
        {
            if (user.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                && user.Password == password)
            {
                Console.WriteLine($"Mogesalmebit, {user.Name}!");
                if (user.Role == UserRoleATM.admin)
                    AdminMenu();
                else
                    ClientMenu(user);
                return;
            }
        }
        Console.WriteLine("Momxmareblis saxeli an paroli arasworia.");
    }

    // კლიენტის ბალანსი და სესხის მონაცემები იცვლება ამ მენიუდან.
    private void ClientMenu(UserForATM user)
    {
        while (true)
        {
            Console.WriteLine("\n1 - Balansi\n2 - Tanxis shetana\n3 - Tanxis gatana\n4 - Sesxis motxovna\n0 - Angarishidan gasvla");
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine($"Balansi: {user.Balance:F2} GEL");
                    Console.WriteLine($"Damtkicebuli sesxi: {user.Loan:F2} GEL");
                    Console.WriteLine($"Momlodine motxovna: {user.PendingLoan:F2} GEL");
                    break;
                case "2":
                    // შეყვანილი თანხა ემატება ბალანსს და მაშინვე ინახება ფაილში.
                    decimal deposit = ReadAmount();
                    if (deposit == 0) break;
                    user.Balance += deposit;
                    SaveUsers();
                    Console.WriteLine($"Tanxa shetanilia. Balansi: {user.Balance:F2} GEL");
                    break;
                case "3":
                    // თანხის გატანამდე ვამოწმებთ, საკმარისია თუ არა ბალანსი.
                    decimal withdrawal = ReadAmount();
                    if (withdrawal == 0) break;
                    if (withdrawal > user.Balance)
                    {
                        Console.WriteLine("Balansze sakmarisi tanxa ar aris.");
                        break;
                    }
                    user.Balance -= withdrawal;
                    SaveUsers();
                    Console.WriteLine($"Tanxa gatanilia. Balansi: {user.Balance:F2} GEL");
                    break;
                case "4":
                    // სანამ ადმინი პასუხს გასცემს, მეორე მოთხოვნა არ მიიღება.
                    if (user.PendingLoan > 0)
                    {
                        Console.WriteLine("Tqven ukve gaqvt momlodine motxovna.");
                        break;
                    }
                    decimal amount = ReadAmount();
                    if (amount == 0) break;
                    user.PendingLoan = amount;
                    SaveUsers();
                    Console.WriteLine("Sesxis motxovna gaigzavna adminis dasadastureblad.");
                    break;
                case "0":
                case null: return;
                default: Console.WriteLine("Araswori archevani"); break;
            }
        }
    }

    // ადმინი ხედავს მხოლოდ მომლოდინე სესხებს და იღებს გადაწყვეტილებას.
    private void AdminMenu()
    {
        while (true)
        {
            Console.WriteLine("\n1 - Sesxis motxovnebi\n0 - Angarishidan gasvla");
            string? choice = Console.ReadLine();
            if (choice == "0" || choice == null) return;
            if (choice != "1")
            {
                Console.WriteLine("Araswori archevani");
                continue;
            }

            // ცალკე სიაში ვაგროვებთ კლიენტებს, რომლებსაც სესხი აქვთ მოთხოვნილი.
            List<UserForATM> requests = new List<UserForATM>();
            foreach (UserForATM user in users)
            {
                if (user.Role == UserRoleATM.client && user.PendingLoan > 0)
                {
                    requests.Add(user);
                    Console.WriteLine($"{requests.Count} - {user.Username}: {user.PendingLoan:F2} GEL");
                }
            }
            if (requests.Count == 0)
            {
                Console.WriteLine("Momlodine motxovnebi ar aris.");
                continue;
            }

            Console.Write("Airchiet motxovnis nomeri (0 - Ukan): ");
            if (!int.TryParse(Console.ReadLine(), out int number) || number < 1 || number > requests.Count)
            {
                Console.WriteLine("Motxovna ar archeula.");
                continue;
            }

            // სიის ინდექსი 0-დან იწყება, მენიუში ნუმერაცია კი 1-დან.
            UserForATM client = requests[number - 1];
            Console.WriteLine("1 - Dadastureba, 2 - Uaryofa, 0 - Ukan");
            string? decision = Console.ReadLine();
            if (decision == "1")
            {
                // დამტკიცებული სესხი კლიენტის ბალანსს ემატება.
                client.Balance += client.PendingLoan;
                client.Loan += client.PendingLoan;
                client.PendingLoan = 0;
                SaveUsers();
                Console.WriteLine("Sesxi damtkicebulia.");
            }
            else if (decision == "2")
            {
                // უარყოფისას მოთხოვნა იშლება, ბალანსი უცვლელი რჩება.
                client.PendingLoan = 0;
                SaveUsers();
                Console.WriteLine("Motxovna uaryofilia.");
            }
        }
    }

    // ტექსტს გარდაქმნის თანხაში; არასწორი მნიშვნელობისას აბრუნებს 0-ს.
    private decimal ReadAmount()
    {
        Console.Write("Sheiyvanet tanxa (magalitad 10.50): ");
        string input = (Console.ReadLine() ?? "").Trim().Replace(',', '.');
        // თანხა უნდა იყოს დადებითი და ჰქონდეს მაქსიმუმ ორი ათწილადი.
        if (!decimal.TryParse(input, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal amount)
            || amount <= 0 || amount > 1000000000 || decimal.Round(amount, 2) != amount)
        {
            Console.WriteLine("Sheiyvanet dadebiti tanxa 1 miliardamde, maqsimum 2 atwiladit.");
            return 0;
        }
        return amount;
    }

    // პირველ გაშვებაზე ქმნის წინასწარ განსაზღვრულ ადმინისტრატორს.
    private void CreateAdmin()
    {
        // ძველ ანგარიშებს არ ვცვლით; დაკავებული სახელისას ვცდით admin1-ს და ა.შ.
        string username = "admin";
        while (true)
        {
            UserForATM? existing = users.Find(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (existing == null) break;
            if (existing.Role == UserRoleATM.admin) return;
            username += "1";
        }

        UserForATM admin = new UserForATM();
        admin.Name = "Admin";
        admin.Lastname = "ATM";
        admin.Username = username;
        admin.Password = "Admin123";
        admin.Role = UserRoleATM.admin;
        users.Add(admin);
        Console.WriteLine($"Adminis angarishi: {username}, paroli: Admin123");
    }

    // ფაილის თითოეულ ხაზს ვყოფთ სვეტებად და ვქმნით მომხმარებლის ობიექტს.
    private void LoadUsers()
    {
        if (!File.Exists(usersFile))
        {
            if (File.Exists(loansFile))
                throw new FormatException();

            // ძველი ვერსიის მონაცემებსაც ვკითხულობთ, რომ ანგარიშები არ დაიკარგოს.
            string oldFile = Path.Combine(DataStorage.Folder, "users-legacy.txt");
            if (File.Exists(oldFile))
            {
                string text = File.ReadAllText(oldFile);
                users = JsonSerializer.Deserialize<List<UserForATM>>(text) ?? throw new JsonException();
            }
            return;
        }

        // ორივე ფაილი საჭიროა, რომ სესხების მონაცემები შემთხვევით არ განულდეს.
        if (!File.Exists(loansFile))
            throw new FormatException();

        foreach (string line in File.ReadAllLines(usersFile))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            string[] parts = line.Split('|');
            if (parts.Length != 7)
                throw new FormatException();

            UserForATM user = new UserForATM();
            user.Id = Guid.Parse(parts[0]);
            user.Name = parts[1];
            user.Lastname = parts[2];
            user.Username = parts[3];
            user.Password = parts[4];
            if (parts[5] != "client" && parts[5] != "admin")
                throw new FormatException();
            user.Role = parts[5] == "admin" ? UserRoleATM.admin : UserRoleATM.client;
            user.Balance = decimal.Parse(parts[6], CultureInfo.InvariantCulture);
            users.Add(user);
        }

        // მომხმარებლის Id აკავშირებს სესხის ჩანაწერს შესაბამის ანგარიშთან.
        foreach (string line in File.ReadAllLines(loansFile))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            string[] parts = line.Split('|');
            if (parts.Length != 3)
                throw new FormatException();

            Guid userId = Guid.Parse(parts[0]);
            UserForATM? user = users.Find(item => item.Id == userId);
            if (user == null)
                throw new FormatException();
            user.Loan = decimal.Parse(parts[1], CultureInfo.InvariantCulture);
            user.PendingLoan = decimal.Parse(parts[2], CultureInfo.InvariantCulture);
        }
    }

    // სიიდან ვქმნით ტექსტურ ხაზებს და ვწერთ ორ ცალკე ფაილში.
    private void SaveUsers()
    {
        List<string> userLines = new List<string>();
        List<string> loanLines = new List<string>();

        foreach (UserForATM user in users)
        {
            // users.txt: Id|სახელი|გვარი|მომხმარებლის სახელი|პაროლი|როლი|ბალანსი
            userLines.Add(user.Id + "|" + user.Name + "|" + user.Lastname + "|"
                + user.Username + "|" + user.Password + "|" + user.Role + "|"
                + user.Balance.ToString(CultureInfo.InvariantCulture));

            // loans.txt: მომხმარებლის Id|დამტკიცებული სესხების ჯამი|მომლოდინე თანხა
            if (user.Role == UserRoleATM.client)
            {
                loanLines.Add(user.Id + "|" + user.Loan.ToString(CultureInfo.InvariantCulture)
                    + "|" + user.PendingLoan.ToString(CultureInfo.InvariantCulture));
            }
        }

        // WriteAllLines ფაილს ქმნის, ხოლო არსებულ ფაილს ახალი მონაცემებით ცვლის.
        File.WriteAllLines(usersFile, userLines);
        File.WriteAllLines(loansFile, loanLines);
    }
}
