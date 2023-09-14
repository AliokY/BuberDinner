using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenarator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenarator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // 1. Validate thr user doesn't exist
        if (_userRepository.GetUserByEmail(email) is not null)
        {
           throw new Exception("User with given email already exists");
        }

        // Create a user (generate unique ID)
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        _userRepository.Add(user);

        // Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }

    public AuthenticationResult Login(string email, string password)
    {
        // 1. Validate that user exists
        if(_userRepository.GetUserByEmail(email) is not User user)
        {
           throw new Exception("User with given email does not exist.");     
        }

        // 2. Validate the password is correct
        if(user.Password != password)
        {
            throw new Exception("Password is incorrect.");
        }   

        // 3. Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token );
    }
}