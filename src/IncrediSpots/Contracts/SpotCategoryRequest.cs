namespace IncrediSpots.API.Contracts;

public class SpotCategoryRequest
{
	public string Name { get; private set; } = string.Empty;
	public string Emoji { get; private set; } = string.Empty;
}