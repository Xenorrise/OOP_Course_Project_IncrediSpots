using IncrediSpots.App.Interfaces;
using IncrediSpots.DataAccess.Entities;
using IncrediSpots.Domain.Interfaces;
using IncrediSpots.Domain.Models;

namespace IncrediSpots.App.Services;

public class SpotCategoryService : ISpotCategoryService
{
	private readonly ISpotCategoryRepository _spotCategoryRepository;

	public SpotCategoryService(ISpotCategoryRepository spotCategoryRepository)
	{
		_spotCategoryRepository = spotCategoryRepository;
	}

	public async Task<IReadOnlyList<SpotCategoryModel>> GetAllSpotCategoriesAsync()
	{
		return await _spotCategoryRepository.GetAllAsync();
	}

	public async Task<SpotCategoryModel> GetSpotCategoryByIdAsync(int id)
	{
		return await _spotCategoryRepository.GetByIdAsync(id);
	}

	public async Task<SpotCategoryModel?> GetSpotCategoryByNameAndEmojiAsync(string name, string emoji)
	{
		return await _spotCategoryRepository.GetByNameAndEmoji(name, emoji);
	}

	public async Task<SpotCategoryModel> CreateSpotCategoryAsync(SpotCategory category)
	{
		var categoryModel = SpotCategoryMapper.From(category);

		await _spotCategoryRepository.AddAsync(categoryModel);

		return categoryModel;
	}

	public async Task UpdateSpotCategoryAsync(int id)
	{
		await _spotCategoryRepository.GetByIdAsync(id);
	}

	public async Task DeleteSpotCategoryAsync(int id)
	{
		await _spotCategoryRepository.DeleteAsync(id);
	}
}

public static class SpotCategoryMapper
{
	public static SpotCategoryModel From(SpotCategory category)
	{
		return new SpotCategoryModel(
			category.Name,
			category.Emoji
		);
	}
}