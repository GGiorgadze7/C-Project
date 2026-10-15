using C__Project.Models.Enum;

namespace C__Project.Models;

// ეს კლასი ერთი მომხმარებლის ანგარიშის მონაცემებს აერთიანებს.
public class UserForATM
{
    // უნიკალური ნომერი მომხმარებელს სესხის ჩანაწერთან აკავშირებს.
    public Guid Id { get; set; } = Guid.NewGuid();

    // პირადი მონაცემები რეგისტრაციისას ივსება.
    public string Name { get; set; } = "";
    public string Lastname { get; set; } = "";

    // ეს ორი ველი ანგარიშში შესვლისთვის გამოიყენება.
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";

    // როლი განსაზღვრავს, რომელი მენიუ გაიხსნება შესვლის შემდეგ.
    public UserRoleATM Role { get; set; }

    // decimal თანხის შესანახად გამოიყენება.
    public decimal Balance { get; set; }

    // მოთხოვნილი თანხა, რომელსაც ადმინის პასუხი ჯერ არ აქვს.
    public decimal PendingLoan { get; set; }

    // მომხმარებლისთვის დამტკიცებული სესხების საერთო თანხა.
    public decimal Loan { get; set; }
}
