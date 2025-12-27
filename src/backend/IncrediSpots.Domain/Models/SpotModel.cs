namespace IncrediSpots.Domain.Models;

public class SpotModel
{
	public int Id { get; private set; }

	public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

	public SpotCategoryModel? Category { get; private set; }

	public int CategoryId;

	public double? Latitude { get; private set; }
    public double? Longitude { get; private set; }

	public int Rating { get; private set; }

	public DateTime CreatedAt { get; private set; }

	public int? UserId { get; private set; }

	protected SpotModel() { }

	public SpotModel(
        string title,
        string description,
        SpotCategoryModel category,
        double latitude,
        double longitude,
        int? userId)
    {
        TitleValidation(title);
        CoordinatesValidation(latitude, longitude);

        Title = title;
        Description = description;
        Category = category;
		CategoryId = Category.Id;
        Latitude = latitude;
        Longitude = longitude;
        UserId = userId;

        Rating = 0;
        CreatedAt = DateTime.UtcNow;
    }
	public void UpdateDescription(string title, string description, SpotCategoryModel? category)
	{
		TitleValidation(title);

		Title = title;
		Description = description;
		Category = category;
	}

	public void ChangeLocation(double? latitude, double? longitude)
	{
		CoordinatesValidation(latitude, longitude);

		Latitude = latitude;
		Longitude = longitude;
	}

	public int GetCategoryId()
	{
		return Category != null ? Category.Id : 0;
	}
	public void UpVote()
	{
		Rating++;
	}
	
	public void DownVote()
	{
		Rating--;
	}

	private static void TitleValidation(string title)
	{
		if (string.IsNullOrWhiteSpace(title))
			throw new ArgumentException("Title cannot be empty");
	}

	private static void CoordinatesValidation(double? lat, double? lng)
    {
        if (lat < -90 || lat > 90)
            throw new ArgumentOutOfRangeException(nameof(lat));

        if (lng < -180 || lng > 180)
            throw new ArgumentOutOfRangeException(nameof(lng));
    }
}
