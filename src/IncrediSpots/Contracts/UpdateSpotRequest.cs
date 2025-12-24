namespace IncrediSpots.API.Contracts;

public class UpdateSpotRequest
{
	public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

	public int CategoryId { get; private set; }

	public double Latitude { get; private set; }
    public double Longitude { get; private set; }
}