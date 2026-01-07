using IncrediSpots.DataAccess.Context;
using IncrediSpots.Domain.Models;
using IncrediSpots.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeApp.DataAccess.Repositories;
public class SpotCategoryRepository : BaseRepository<SpotCategoryModel>, ISpotCategoryRepository
{
	public SpotCategoryRepository(IncrediSpotsMainDbContext context) : base(context){}

	public override async Task<SpotCategoryModel?> GetByIdAsync(int id)
	{
		var category = await _context.SpotCategories.FindAsync(id) ?? throw new Exception($"Category with ID {id} not found");
		return category;
	}

	public async Task<SpotCategoryModel?> GetByNameAndEmoji(string name, string emoji)
	{
		var category = _context.SpotCategories.FirstOrDefault(c => c.Name == name && c.Emoji == emoji);
		return category;
	}

	public async Task<IReadOnlyList<SpotCategoryModel>> GetAllAsync()
	{
		return await _context.Set<SpotCategoryModel>().ToListAsync();
	}

	public override async Task AddAsync(SpotCategoryModel category)
	{
		await base.AddAsync(category);
        await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(SpotCategoryModel newCategory)
	{
		var categoryModel = await _context.SpotCategories.FindAsync(newCategory.Id);
		if(categoryModel == null) throw new Exception($"Category with ID {newCategory.Id} not found");

		categoryModel.ChangeCategoryName(newCategory.Name);
		categoryModel.ChangeCategoryEmoji(newCategory.Emoji);

		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var category = await _context.SpotCategories.FindAsync(id) ?? throw new Exception($"Spot with ID {id} not found");
		_context.SpotCategories.Remove(category);
		await _context.SaveChangesAsync();
	}
}