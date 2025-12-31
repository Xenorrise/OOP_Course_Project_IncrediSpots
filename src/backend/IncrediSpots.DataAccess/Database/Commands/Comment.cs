using Npgsql.Replication;

namespace IncrediSpots.DataAccess.Entities;

public class Comment
{
    public int Id { get; set; }

    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public int SpotId { get; set; }
    public int UserId { get; set; }

	public Comment(string text, int spotId, int userId)
    {
		Text = text;
		SpotId = spotId;
		UserId = userId;

		CreatedAt = DateTime.Now;
    }
}

