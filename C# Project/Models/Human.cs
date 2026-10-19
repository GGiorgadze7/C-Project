namespace C__Project.Models;

public class Human
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateOnly? BirthDate { get; private set; }
    public string Name => $"{FirstName} {LastName}".Trim();

    public Human(string firstName, string lastName, DateOnly birthDate)
        : this(firstName)
    {
        if (string.IsNullOrWhiteSpace(lastName) || lastName.Contains('|'))
            throw new ArgumentException("Gvari ar unda iyos carieli an sheicavdes | simbolos.");
        if (birthDate > DateOnly.FromDateTime(DateTime.Today))
            throw new ArgumentException("Sheiyvanet swori dabadebis tarigi!.");

        LastName = lastName.Trim();
        BirthDate = birthDate;
    }

    
    protected Human(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Contains('|'))
            throw new ArgumentException("Saxeli ar unda iyos carieli an sheicavdes | simbolos.");

        FirstName = firstName.Trim();
        LastName = string.Empty;
    }
}
