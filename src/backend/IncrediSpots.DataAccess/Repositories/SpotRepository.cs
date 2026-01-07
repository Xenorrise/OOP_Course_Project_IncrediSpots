using IncrediSpots.DataAccess.Context;
using IncrediSpots.Domain.Models;
using IncrediSpots.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeApp.DataAccess.Repositories;

public class SpotRepository : BaseRepository<SpotModel>, ISpotRepository
{
	public SpotRepository(IncrediSpotsMainDbContext context) : base(context){}

	public override async Task<SpotModel?> GetByIdAsync(int id)
	{
		var spot = await _context.Spots.FindAsync(id) ?? throw new Exception($"Spot with ID {id} not found");
		return spot;
	}

	public async Task<IReadOnlyList<SpotModel>> GetAllAsync()
	{
		return await _context.Spots.Include(s => s.Category).ToListAsync();
	}

	public override async Task AddAsync(SpotModel spot)
	{
		await base.AddAsync(spot);
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

	public async Task<SpotModel> VoteAsync(int id, int userVote)
	{
		var spot = await _context.Spots.FindAsync(id) ?? throw new Exception($"Spot with ID {id} not found");
		if(userVote == 1)
		{
			spot.UpVote();
		} else if(userVote == -1)
		{
			spot.DownVote();
		}
		await _context.SaveChangesAsync();
		return spot;
	}
}