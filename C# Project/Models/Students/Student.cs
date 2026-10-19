namespace C__Project.Models;

public class Student
{
    // private set იცავს მონაცემებს გარედან პირდაპირი შეცვლისგან (ინკაფსულაცია).
    public string Name { get; private set; }
    public int RollNumber { get; private set; }
    public char Grade { get; private set; }

    // კონსტრუქტორი ქმნის სტუდენტს და ამოწმებს მის მონაცემებს.
    public Student(string name, int rollNumber, char grade)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Saxeli ar unda iyos carieli.");

        if (rollNumber <= 0)
            throw new ArgumentException("Siis nomeri unda iyos dadebiti.");

        Name = name.Trim();
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
        Console.WriteLine($"Saxeli: {Name} | Siis nomeri: {RollNumber} | Shefaseba: {Grade}");
    }
}

