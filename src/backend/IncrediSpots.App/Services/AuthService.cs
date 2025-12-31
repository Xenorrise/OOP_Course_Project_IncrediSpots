using IncrediSpots.App.Interfaces;
using IncrediSpots.Domain.Models;
using IncrediSpots.Domain.Interfaces;

public class AuthService : IAuthService
{
    private readonly IUserRepository _users;
    private readonly IPasswordService _passwords;
    private readonly IJwtService _jwt;

    public AuthService(
        IUserRepository users,
        IPasswordService passwords,
        IJwtService jwt)
    {
        _users = users;
        _passwords = passwords;
        _jwt = jwt;
    }

    public async Task<string> RegisterAsync(string email, string password)
    {
        if (await _users.GetByEmailAsync(email) != null)
            throw new Exception("User already exists");

        var user = new UserModel(email, "");
        user = new UserModel(email, _passwords.Hash(user, password));

        await _users.AddAsync(user);
        return _jwt.Generate(user);
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await _users.GetByEmailAsync(email)
            ?? throw new Exception("Invalid credentials");

        if (!_passwords.Verify(user, password))
            throw new Exception("Invalid credentials");

        return _jwt.Generate(user);
    }
}
