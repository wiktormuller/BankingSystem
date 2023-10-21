using BankingSystem.Application.Contracts.Responses;
using BankingSystem.Application.Exceptions;
using BankingSystem.Application.Services;
using BankingSystem.Core.Repositories;
using MediatR;

namespace BankingSystem.Application.Commands.Handlers
{
    public sealed class SignInHandler : IRequestHandler<SignIn, JsonWebTokenResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordService _passwordService;

        public SignInHandler(IUserRepository userRepository,
            IJwtService jwtService,
            IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordService = passwordService;
        }

        public async Task<JsonWebTokenResponse> Handle(SignIn command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(command.Email);

            if (user is null)
            {
                throw new InvalidCredentialsException();
            }

            if (!user.IsActive)
            {
                throw new UserNotActiveException(user.Email);
            }

            if (!_passwordService.IsValid(user.PasswordHash, command.Password))
            {
                throw new InvalidCredentialsException();
            }

            var jwt = _jwtService.CreateToken(user.Id.ToString(), user.Email, user.Role);

            return new JsonWebTokenResponse
            (
                jwt.AccessToken,
                jwt.Email,
                jwt.Expires,
                Guid.Parse(jwt.Id),
                jwt.Role
            );
        }
    }
}
