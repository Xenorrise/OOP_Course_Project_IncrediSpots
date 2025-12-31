namespace IncrediSpots.DataAccess.Entities;

public class UpdateSpot
{
	public int Id { get; private set; }

	public string Title { get; private set; }
    public string Description { get; private set; }

	public int CategoryId { get; private set; }

	public double Latitude { get; private set; }
    public double Longitude { get; private set; }

	public UpdateSpot(
        int id,
        string title,
        string description,
        int categoryId,
        double latitude,
        double longitude)
    {
        Id = id;
        Title = title;
        Description = description;
        CategoryId = categoryId;
        Latitude = latitude;
        Longitude = longitude;
    }
}
