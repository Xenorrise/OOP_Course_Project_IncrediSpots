using IncrediSpots.Domain.Models;

namespace IncrediSpots.Domain.Interfaces;

public interface ISpotRepository : IRepository<SpotModel>
{
    Task<IReadOnlyList<SpotModel>> GetAllAsync();
    Task UpdateAsync(SpotModel newSpot);
    Task DeleteAsync(int id);
    Task<SpotModel> VoteAsync(int id, int userVote);
}