using IncrediSpots.Domain.Models;

namespace IncrediSpots.App.Interfaces;
public interface IPasswordService
{
    string Hash(UserModel user, string password);

    bool Verify(UserModel user, string password);
}