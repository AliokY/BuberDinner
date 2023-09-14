namespace BuberDinner.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenarator
{
    string GenerateToken(Guid useId, string firstNmae, string lastName);
}