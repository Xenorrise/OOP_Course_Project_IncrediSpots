using IncrediSpots.API.Contracts;
using IncrediSpots.App.Interfaces;
using IncrediSpots.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IncrediSpots.API.Controllers;

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
		var spot = await _categoryService.GetAllSpotCategoriesAsync();
		return Ok(spot);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var spot = await _categoryService.GetSpotCategoryByIdAsync(id);
		return Ok(spot);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody]SpotCategoryRequest request)
	{	
		var spot = new SpotCategory(
			request.Name,
			request.Emoji
		);
		await _categoryService.CreateSpotCategoryAsync(spot);
		return Ok(spot);
	}

	public async Task<IActionResult> Update(int id)
	{
		//await _spotRepository.GetByIdAsync(id);
		return NoContent();
	}

	public async Task<IActionResult> Delete(int id)
	{
		await _categoryService.DeleteSpotCategoryAsync(id);
		return NoContent();
	}
}
