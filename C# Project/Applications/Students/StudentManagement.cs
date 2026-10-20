namespace C__Project.Models;

public class StudentManagement
{
    // სია ინახავს სტუდენტებს პროგრამის მუშაობის განმავლობაში.
    private List<Student> students = new List<Student>();
    private string studentsFile = Path.Combine(DataStorage.Folder, "students.txt");

    // აჩვენებს მენიუს და იძახებს მომხმარებლის მიერ არჩეულ მეთოდს.
    public void Run()
    {
        Console.WriteLine("Studentebis martvis sistema");
        
        try
        {
            LoadStudents();
            while (true)
            {
                Console.WriteLine("\n1 - Axali studentis damateba");
                Console.WriteLine("2 - Yvela studentis naxva");
                Console.WriteLine("3 - Studentis dzebna nomrit");
                Console.WriteLine("4 - Shefasebis ganaxleba");
                Console.WriteLine("5 - Studentis washla");
                Console.WriteLine("0 - Gasvla");
                Console.Write("Airchiet: ");

                switch (ReadText())
                {
                    case "1": AddStudent(); break;
                    case "2": ShowAllStudents(); break;
                    case "3": SearchStudent(); break;
                    case "4": UpdateStudentGrade(); break;
                    case "5": DeleteStudent(); break;
                    case "0":
                        Console.WriteLine("Programa dasrulda.");
                        return;
                    default:
                        Console.WriteLine("Airchiet meniushi mititebuli nomeri.");
                        break;
                }
            }
        }
        catch (EndOfStreamException)
        {
            Console.WriteLine("\nSheyvana dasrulda. Programa daixura.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Failis wakitxva an shenaxva ver moxerxda: {ex.Message}");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Failze wvdomis ufleba ar gvaqvs.");
        }
    }

    // კითხულობს სახელს, უნიკალურ ნომერსა და შეფასებას და ამატებს სტუდენტს.
    private void AddStudent()
    {
        Console.Write("Sheiyvanet saxeli: ");
        string name = ReadText();
        while (string.IsNullOrWhiteSpace(name) || name.Contains('|'))
        {
            Console.Write("Saxeli ar unda iyos carieli an sheicavdes | simbolos. Scadet tavidan: ");
            name = ReadText();
        }

        Console.Write("Sheiyvanet gvari: ");
        string lastName = ReadText();
        while (string.IsNullOrWhiteSpace(lastName) || lastName.Contains('|'))
        {
            Console.Write("Gvari ar unda iyos carieli an sheicavdes | simbolos. Scadet tavidan: ");
            lastName = ReadText();
        }

        DateOnly birthDate;
        while (true)
        {
            Console.Write("Sheiyvanet dabadebis tarigi (yyyy-MM-dd): ");
            if (DateOnly.TryParseExact(ReadText(), "yyyy-MM-dd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out birthDate)
                && birthDate <= DateOnly.FromDateTime(DateTime.Today))
                break;

            Console.WriteLine("Sheiyvanet swori tarigi!");
        }

        int rollNumber = ReadRollNumber();
        while (FindStudent(rollNumber) != null)
        {
            Console.WriteLine("Am nomrit studenti ukve arsebobs.");
            rollNumber = ReadRollNumber();
        }

        char grade = ReadGrade();
        Student student = new Student(name, lastName, birthDate, rollNumber, grade);
        students.Add(student);
        SaveStudents();
        Console.WriteLine("Studenti daemata.");
    }

    // აჩვენებს ყველა სტუდენტს და მათ საერთო რაოდენობას.
    private void ShowAllStudents()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("Studentebis sia carielia.");
            return;
        }

        foreach (Student student in students)
            student.ShowDetails();

        Console.WriteLine($"Studentebis raodenoba: {students.Count}");
    }

    // ეძებს სტუდენტს შეყვანილი ნომრით და აჩვენებს მის მონაცემებს.
    private void SearchStudent()
    {
        Student? student = FindStudent(ReadRollNumber());
        if (student == null)
            Console.WriteLine("Am nomrit studenti ver moidzebna.");
        else
            student.ShowDetails();
    }

    // პოულობს სტუდენტს და უცვლის შეფასებას.
    private void UpdateStudentGrade()
    {
        Student? student = FindStudent(ReadRollNumber());
        if (student == null)
        {
            Console.WriteLine("Am nomrit studenti ver moidzebna.");
            return;
        }

        student.UpdateGrade(ReadGrade());
        SaveStudents();
        Console.WriteLine("Shefaseba ganaxlda.");
    }

    // დამატებითი ფუნქცია: დადასტურების შემდეგ შლის სტუდენტს სიიდან.
    private void DeleteStudent()
    {
        Student? student = FindStudent(ReadRollNumber());
        if (student == null)
        {
            Console.WriteLine("Am nomrit studenti ver moidzebna.");
            return;
        }

        student.ShowDetails();
        Console.Write("Washlis dasadastureblad sheiyvanet 1: ");
        if (ReadText() == "1")
        {
            students.Remove(student);
            SaveStudents();
            Console.WriteLine("Studenti waishala.");
        }
        else
            Console.WriteLine("Washla gauqmda.");
    }

    // სიაში ეძებს შესაბამის ნომერს; თუ ვერ იპოვა, აბრუნებს null-ს.
    private Student? FindStudent(int rollNumber)
    {

        return students.FirstOrDefault(s => s.RollNumber == rollNumber);
        
    }

    // იმეორებს შეყვანას, სანამ მომხმარებელი დადებით მთელ რიცხვს არ შეიყვანს.
    private int ReadRollNumber()
    {
        while (true)
        {
            Console.Write("Sheiyvanet siis nomeri: ");
            if (int.TryParse(ReadText(), out int number) && number > 0)
                return number;

            Console.WriteLine("Sheiyvanet dadebiti mteli ricxvi.");
        }
    }

    // იღებს მხოლოდ ერთ ასოს A-დან F-მდე; პატარა ასოს გარდაქმნის დიდად.
    private char ReadGrade()
    {
        while (true)
        {
            Console.Write("Sheiyvanet shefaseba (A, B, C, D, E, F): ");
            string input = ReadText().ToUpperInvariant();
            if (input.Length == 1 && input[0] >= 'A' && input[0] <= 'F')
                return input[0];

            Console.WriteLine("Araswori shefasebaa. Scadet tavidan.");
        }
    }

    
    // დროებითი ფაილი იცავს ძველ მონაცემებს, თუ ჩაწერა ვერ დასრულდა.
    private void SaveStudents()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(studentsFile)!);
        List<string> lines = new List<string>();
        foreach (Student student in students)
            lines.Add(student.BirthDate.HasValue
                ? $"{student.RollNumber}|{student.FirstName}|{student.Grade}|{student.LastName}|{student.BirthDate.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)}"
                : $"{student.RollNumber}|{student.FirstName}|{student.Grade}");

        string temporaryFile = studentsFile + ".tmp";
        File.WriteAllLines(temporaryFile, lines, System.Text.Encoding.UTF8);
        File.Move(temporaryFile, studentsFile, true);
    }

    // გაშვებისას კითხულობს სტუდენტებს; დაზიანებულ ფაილს არ გადაწერს.
    private void LoadStudents()
    {
        students.Clear();
        if (!File.Exists(studentsFile))
            return;

        foreach (string line in File.ReadAllLines(studentsFile, System.Text.Encoding.UTF8))
        {
            string[] parts = line.Split('|');
            if ((parts.Length != 3 && parts.Length != 5) ||
                !int.TryParse(parts[0], out int number) || number <= 0 ||
                string.IsNullOrWhiteSpace(parts[1]) ||
                parts[2].Length != 1 || parts[2][0] < 'A' || parts[2][0] > 'F' ||
                FindStudent(number) != null)
            {
                throw new IOException("Students.txt failshi araswori monacemebia. Sheamowmet faili.");
            }

            if (parts.Length == 3)
            {
                students.Add(new Student(parts[1], number, parts[2][0]));
                continue;
            }

            if (string.IsNullOrWhiteSpace(parts[3]) ||
                !DateOnly.TryParseExact(parts[4], "yyyy-MM-dd",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateOnly birthDate) ||
                birthDate > DateOnly.FromDateTime(DateTime.Today))
                throw new IOException("Students.txt failshi araswori monacemebia. Sheamowmet faili.");

            students.Add(new Student(parts[1], parts[3], birthDate, number, parts[2][0]));
        }
    }

    // კითხულობს ტექსტს, აშორებს კიდეებზე გამოტოვებებს და ამოიცნობს შეყვანის დასრულებას.
    private string ReadText()
    {
        string? input = Console.ReadLine();
        if (input == null)
            throw new EndOfStreamException();

        return input.Trim();
    }
}

