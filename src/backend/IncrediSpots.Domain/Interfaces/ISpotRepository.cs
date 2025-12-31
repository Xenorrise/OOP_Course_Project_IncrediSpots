using IncrediSpots.Domain.Models;

namespace IncrediSpots.Domain.Interfaces;

public interface ISpotRepository
{
    Task<SpotModel> GetByIdAsync(int id);
    Task<IReadOnlyList<SpotModel>> GetAllAsync();
    Task AddAsync(SpotModel spot);
    Task UpdateAsync(SpotModel newSpot);
    Task DeleteAsync(int id);
    Task<SpotModel> VoteAsync(int id, int userVote);
}