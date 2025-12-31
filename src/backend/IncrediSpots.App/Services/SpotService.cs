using IncrediSpots.App.Interfaces;
using IncrediSpots.DataAccess.Entities;
using IncrediSpots.Domain.Interfaces;
using IncrediSpots.Domain.Models;

namespace IncrediSpots.App.Services;

public class SpotService : ISpotService
{
	private readonly ISpotRepository _spotRepository;
	private readonly ISpotCategoryRepository _spotCategoryRepository;

	public SpotService(ISpotRepository spotRepository, ISpotCategoryRepository spotCategoryRepository)
	{
		_spotRepository = spotRepository;
		_spotCategoryRepository = spotCategoryRepository;
	}

	public async Task<IReadOnlyList<SpotModel>> GetAllSpotsAsync()
	{
		return await _spotRepository.GetAllAsync();
	}

	public async Task<SpotModel> GetSpotByIdAsync(int id)
	{
		return await _spotRepository.GetByIdAsync(id);
	}

	public async Task<SpotModel> CreateSpotAsync(Spot spot)
	{
		var categoryModel = await _spotCategoryRepository.GetByIdAsync(spot.CategoryId);
		var spotModel = SpotMapper.From(spot, categoryModel);

		await _spotRepository.AddAsync(spotModel);

		return spotModel;
	}

	public async Task UpdateSpotAsync(UpdateSpot updateSpot)
	{	
		var categoryModel = await _spotCategoryRepository.GetByIdAsync(updateSpot.CategoryId) ?? throw new Exception("Category not found");
		var spotModel = SpotMapper.FromUpdate(updateSpot, categoryModel);

		await _spotRepository.UpdateAsync(spotModel);
	}

	public async Task DeleteSpotAsync(int id)
	{
		await _spotRepository.DeleteAsync(id);
	}
	
	public async Task<SpotModel> VoteSpotAsync(int id, int userVote)
	{
		var spot = await _spotRepository.VoteAsync(id, userVote);
		return spot;
	}
}

public static class SpotMapper
{
	public static SpotModel From(Spot spot, SpotCategoryModel category)
	{
		return new SpotModel(
			spot.Title,
			spot.Description,
			category,
			spot.Latitude,
			spot.Longitude,
			spot.UserId
		);
	}

	public static SpotModel FromUpdate(UpdateSpot updateSpot, SpotCategoryModel category)
	{
		return new SpotModel(
			updateSpot.Title,
			updateSpot.Description,
			category,
			updateSpot.Latitude,
			updateSpot.Longitude,
			null
		);
	}
}