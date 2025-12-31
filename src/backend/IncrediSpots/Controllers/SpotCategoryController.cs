using IncrediSpots.API.Contracts;
using IncrediSpots.App.Interfaces;
using IncrediSpots.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IncrediSpots.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class SpotCategoryController : ControllerBase
{
	private readonly ISpotCategoryService _categoryService;

	public SpotCategoryController(ISpotCategoryService categoryService)
	{
		_categoryService = categoryService;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var category = await _categoryService.GetAllSpotCategoriesAsync();
		return Ok(category);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var category = await _categoryService.GetSpotCategoryByIdAsync(id);
		return Ok(category);
	}
	[HttpGet]
	public async Task<IActionResult> GetByNameAndEmojiAsync(string name, string emoji)
	{
		var category = await _categoryService.GetSpotCategoryByNameAndEmojiAsync(name, emoji);
		return Ok(category);
	}
	[HttpPost]
	public async Task<IActionResult> Create([FromBody]SpotCategoryRequest request)
	{	
		var category = new SpotCategory(
			request.Name,
			request.Emoji
		);
		
		var categoryDomain =  await _categoryService.CreateSpotCategoryAsync(category);

		var response = new SpotCategoryResponse
    	(
			categoryDomain.Id,
			categoryDomain.Name,
			categoryDomain.Emoji
		);

		return Ok(response);
	}

	[HttpPatch("{id}")]
	public async Task<IActionResult> Update(int id)
	{
		//await _spotRepository.GetByIdAsync(id);
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		await _categoryService.DeleteSpotCategoryAsync(id);
		return NoContent();
	}
}
