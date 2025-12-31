using IncrediSpots.App.Interfaces;
using IncrediSpots.Domain.Models;
using Microsoft.AspNetCore.Identity;

public class PasswordService : IPasswordService
{
    private readonly PasswordHasher<UserModel> _hasher = new();

    public string Hash(UserModel user, string password)
        => _hasher.HashPassword(user, password);

    public bool Verify(UserModel user, string password)
        => _hasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            password
        ) == PasswordVerificationResult.Success;
}
