namespace IncrediSpots.Domain.Models;

public class CommentModel : BaseEntity
{
    public int SpotId { get; private set; }
    public int AuthorId { get; private set; }
    public string Text { get; } = string.Empty;
    public SpotModel Spot { get; private set; } = null!;
    public UserModel Author { get; private set; } = null!;
	protected CommentModel() { }
    public CommentModel(string text, SpotModel spot, UserModel author)
    {
        Text = text;
        Spot = spot;
        Author = author;

        SpotId = spot.Id;
        AuthorId = author.Id;

        CreatedAt = DateTime.UtcNow;
    }
}
