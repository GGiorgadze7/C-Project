namespace C__Project.Models;

public class Student : Human
{
    // private set იცავს მონაცემებს გარედან პირდაპირი შეცვლისგან (ინკაფსულაცია).
    public int RollNumber { get; private set; }
    public char Grade { get; private set; }

    // კონსტრუქტორი ქმნის სტუდენტს და ამოწმებს მის მონაცემებს.
    public Student(string name, int rollNumber, char grade)
        : base(name)
    {
        if (rollNumber <= 0)
            throw new ArgumentException("Siis nomeri unda iyos dadebiti.");

        RollNumber = rollNumber;
        UpdateGrade(grade);
    }

    public Student(string firstName, string lastName, DateOnly birthDate, int rollNumber, char grade)
        : base(firstName, lastName, birthDate)
    {
        if (rollNumber <= 0)
            throw new ArgumentException("Siis nomeri unda iyos dadebiti.");

        RollNumber = rollNumber;
        UpdateGrade(grade);
    }

    // ამოწმებს შეფასებას და ინახავს მას დიდი ასოთი.
    public void UpdateGrade(char grade)
    {
        grade = char.ToUpperInvariant(grade);
        if (grade < 'A' || grade > 'F')
            throw new ArgumentException("Shefaseba unda iyos A, B, C, D, E an F.");

        Grade = grade;
    }

    // კონსოლში აჩვენებს ერთი სტუდენტის მონაცემებს.
    public void ShowDetails()
    {
        string birthDate = BirthDate?.ToString("yyyy-MM-dd") ?? "Ucnobia";
        Console.WriteLine($"Id:{Id} | Saxeli: {Name} | Dabadebis tarigi: {birthDate} | Siis nomeri: {RollNumber} | Shefaseba: {Grade}");
    }
}

