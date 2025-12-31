namespace IncrediSpots.Domain.Models;

public class UserModel
{
    public int Id { get; private set; }
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }

    private UserModel() {}

    public UserModel(string email, string passwordHash)
    {
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = DateTime.UtcNow;
    }
    
    public ICollection<CommentModel> Comments { get; set; } = new List<CommentModel>();
}
