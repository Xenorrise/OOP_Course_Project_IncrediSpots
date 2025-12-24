namespace IncrediSpots.Domain.Models;

public class SpotCategoryModel
{
	public int Id { get; private set; }

	public string Name { get; private set; } = string.Empty;
	public string Emoji { get; private set; } = string.Empty;

	protected SpotCategoryModel() { }

	public SpotCategoryModel(string name, string emoji)
    {
		Name = name;
		Emoji = emoji;
    }

	public void ChangeCategoryName(string name)
	{
		NameValidation(name);
		Name = name;
	}

	public void ChangeCategoryEmoji(string emoji)
	{
		Emoji = emoji;
	}

	private static void NameValidation(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Name cannot be empty");
	}
}
