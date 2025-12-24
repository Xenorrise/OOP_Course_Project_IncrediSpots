using IncrediSpots.API.Contracts;
using IncrediSpots.App.Interfaces;
using IncrediSpots.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IncrediSpots.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class SpotController : ControllerBase
{
	private readonly ISpotService _spotService;

	public SpotController(ISpotService spotService)
	{
		_spotService = spotService;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var spot = await _spotService.GetAllSpotsAsync();
		return Ok(spot);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var spot = await _spotService.GetSpotByIdAsync(id);
		return Ok(spot);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody]SpotRequest request)
	{	
		var spot = new Spot(
			request.Title,
			request.Description,
			request.CategoryId,
			request.Latitude,
			request.Longitude,
			request.UserId
		);
		await _spotService.CreateSpotAsync(spot);
		return Ok(spot);
	}

	[HttpPatch("{id}")]
	public async Task<IActionResult> Update(int id, [FromBody]UpdateSpotRequest request)
	{
		var spot = new UpdateSpot(
			id,
			request.Title,
			request.Description,
			request.CategoryId,
			request.Latitude,
			request.Longitude
		);
		await _spotService.UpdateSpotAsync(spot);
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		await _spotService.DeleteSpotAsync(id);
		return NoContent();
	}

	[HttpPost("{id}")]
	public async Task<IActionResult> Like(int id)
	{
		await _spotService.LikeSpotAsync(id);
		return NoContent();
	}
}
