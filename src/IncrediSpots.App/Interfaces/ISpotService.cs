using IncrediSpots.DataAccess.Entities;
using IncrediSpots.Domain.Models;

namespace IncrediSpots.App.Interfaces;
public interface ISpotService
{
    Task<SpotModel> CreateSpotAsync(Spot spot);
    Task<SpotModel> GetSpotByIdAsync(int id);
    Task<IReadOnlyList<SpotModel>> GetAllSpotsAsync();
    Task UpdateSpotAsync(UpdateSpot updateSpot);
    Task DeleteSpotAsync(int id);
    Task LikeSpotAsync(int id);
}
