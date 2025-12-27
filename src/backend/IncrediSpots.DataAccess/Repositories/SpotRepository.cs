using IncrediSpots.DataAccess.Context;
using IncrediSpots.Domain.Models;
using IncrediSpots.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeApp.DataAccess.Repositories;
public class SpotRepository : ISpotRepository
{
	private readonly IncrediSpotsMainDbContext _context;

	public SpotRepository(IncrediSpotsMainDbContext context)
	{
		_context = context;
	}

	public async Task<SpotModel> GetByIdAsync(int id)
	{
		var spot = await _context.Spots.FindAsync(id) ?? throw new Exception($"Spot with ID {id} not found");
		return spot;
	}

	public async Task<IReadOnlyList<SpotModel>> GetAllAsync()
	{
		return await _context.Set<SpotModel>().ToListAsync();
	}

	public async Task AddAsync(SpotModel spot)
	{
		await _context.Spots.AddAsync(spot);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(SpotModel newSpot)
	{
		var spot = await _context.Spots.FindAsync(newSpot.Id) ?? throw new Exception($"Spot with ID {newSpot.Id} not found");
	
		spot.UpdateDescription(newSpot.Title ?? spot.Title, 
		newSpot.Description ?? spot.Description, 
		newSpot.Category ?? spot.Category);
		
		spot.ChangeLocation(newSpot.Latitude ?? spot.Latitude, 
		newSpot.Longitude ?? spot.Longitude);

		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var spot = await _context.Spots.FindAsync(id) ?? throw new Exception($"Spot with ID {id} not found");
		_context.Spots.Remove(spot);
		await _context.SaveChangesAsync();
	}
}