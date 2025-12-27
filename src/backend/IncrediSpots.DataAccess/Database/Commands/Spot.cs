namespace IncrediSpots.DataAccess.Entities;

public class Spot
{
	public int Id { get; private set; }

	public string Title { get; private set; }
    public string Description { get; private set; }

	public int CategoryId { get; private set; }

	public double Latitude { get; private set; }
    public double Longitude { get; private set; }

	public int Rating { get; private set; }

	public DateTime CreatedAt { get; private set; }

	public int? UserId { get; private set; }
	public Spot(
        string title,
        string description,
        int categoryId,
        double latitude,
        double longitude,
        int? userId)
    {
        Title = title;
        Description = description;
        CategoryId = categoryId;
        Latitude = latitude;
        Longitude = longitude;
        UserId = userId;

        Rating = 0;
        CreatedAt = DateTime.UtcNow;
    }
}
