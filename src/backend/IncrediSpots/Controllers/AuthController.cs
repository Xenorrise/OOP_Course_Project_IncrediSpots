using IncrediSpots.App.Interfaces;
using IncrediSpots.API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IncrediSpots.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth)
    {
        _auth = auth;
    }

    [HttpPost]
    public async Task<AuthResponse> Register(RegisterRequest request)
    {
        var token = await _auth.RegisterAsync(
            request.Email,
            request.Password
        );

        return new AuthResponse { Token = token };
    }

    [HttpPost]
    public async Task<AuthResponse> Login(LoginRequest request)
    {
        var token = await _auth.LoginAsync(
            request.Email,
            request.Password
        );

        return new AuthResponse { Token = token };
    }
}
