using IncrediSpots.DataAccess.Entities;
using IncrediSpots.Domain.Models;

namespace IncrediSpots.App.Interfaces;
public interface ISpotCategoryService
{
    Task<SpotCategoryModel> CreateSpotCategoryAsync(SpotCategory category);
    Task<SpotCategoryModel> GetSpotCategoryByIdAsync(int id);
    Task<IReadOnlyList<SpotCategoryModel>> GetAllSpotCategoriesAsync();
    Task UpdateSpotCategoryAsync(int id);
    Task DeleteSpotCategoryAsync(int id);
}
