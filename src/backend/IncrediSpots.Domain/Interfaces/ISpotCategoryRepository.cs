using IncrediSpots.Domain.Models;

namespace IncrediSpots.Domain.Interfaces;

public interface ISpotCategoryRepository : IRepository<SpotCategoryModel>
{
    Task<IReadOnlyList<SpotCategoryModel>> GetAllAsync();
    Task<SpotCategoryModel?> GetByNameAndEmoji(string name, string emoji);
    Task UpdateAsync(SpotCategoryModel newCategory);
    Task DeleteAsync(int id);
}