using BankingSystem.Application.Exceptions;
using BankingSystem.Application.Services;
using BankingSystem.Core.Entities;
using BankingSystem.Core.Repositories;
using MediatR;

namespace BankingSystem.Application.Commands.Handlers
{
    public sealed class SignUpHandler : IRequestHandler<SignUp>
    {
        private readonly IUserRepository _userRepository;
        private readonly IClock _clock;
        private readonly IPasswordService _passwordService;

        public SignUpHandler(IUserRepository userRepository,
            IClock clock,
            IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _clock = clock;
            _passwordService = passwordService;
        }

        public async Task Handle(SignUp command, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetAsync(command.Email);

            if (existingUser is not null)
            {
                throw new EmailAlreadyInUseException();
            }

            var passwordHash = _passwordService.Hash(command.Password);
            var role = Role.CreateUser().Value;
            var now = _clock.CurrentDate();

            var user = new User(Guid.NewGuid(), command.Email, role, passwordHash, now, now);

            await _userRepository.AddAsync(user);
        }
    }
}
