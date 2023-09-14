using BuberDinner.Domain;

namespace BuberDinner.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenarator
{
    string GenerateToken(User user);
}