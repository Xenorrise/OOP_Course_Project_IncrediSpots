namespace IncrediSpots.DataAccess.Entities;

public record class SpotCategory
{
	public int Id { get; private set; }

	public string Name { get; private set; }
	public string Emoji { get; private set; }

	public SpotCategory(string name, string emoji)
    {
		Name = name;
		Emoji = emoji;
    }
}
