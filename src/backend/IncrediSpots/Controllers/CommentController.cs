using System.Security.Claims;
using IncrediSpots.API.Contracts;
using IncrediSpots.App.Interfaces;
using IncrediSpots.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IncrediSpots.API.Controllers;

[ApiController]
public class CommentController : ControllerBase
{
	private readonly ICommentService _commentService;

	public CommentController(ICommentService commentService)
	{
		_commentService = commentService;
	}

	[HttpGet("{spotId}/comments")]
	public async Task<IActionResult> GetAllBySpotId(int spotId)
	{
		var comments = await _commentService.GetAllCommentsBySpotIdAsync(spotId);
		var responses = comments.Select(c => new CommentResponse(
			c.Id,
			c.Text,
			c.Author.Email,
			c.CreatedAt
    	));
		return Ok(responses);
	}

	[Authorize]
	[HttpPost("{spotId}/comments")]
	public async Task<IActionResult> CreateComment(
		int spotId,
		[FromBody]CommentCreateRequest request
	)
	{
		var userId = int.Parse(
			User.FindFirstValue(ClaimTypes.NameIdentifier)!
		);

		var comment = new Comment(
			request.Text,
			spotId,
			userId
		);

		var commentDomain = await _commentService.CreateAsync(comment);

		var response = new CommentResponse
    	(
			commentDomain.Id,
			commentDomain.Text,
			commentDomain.Author?.Email,
			commentDomain.CreatedAt
		);

		return Ok(response);
	}

}
