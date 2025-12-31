using IncrediSpots.Domain.Models;

namespace IncrediSpots.App.Interfaces;
public interface IJwtService
{
    string Generate(UserModel user);
}