using IncrediSpots.Domain.Models;

namespace IncrediSpots.Domain.Interfaces;

public interface ISpotCategoryRepository
{
    Task<SpotCategoryModel> GetByIdAsync(int id);
    Task<IReadOnlyList<SpotCategoryModel>> GetAllAsync();
    Task<SpotCategoryModel?> GetByNameAndEmoji(string name, string emoji);
    Task AddAsync(SpotCategoryModel category);
    Task UpdateAsync(SpotCategoryModel newCategory);
    Task DeleteAsync(int id);
}