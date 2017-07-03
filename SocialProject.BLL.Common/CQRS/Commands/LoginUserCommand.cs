using SocialProject.BLL.Common.Models;
using SocialProject.BLL.Core.CQRS;
using SocialProject.BLL.Core.Services;
using SocialProject.DAL.Common.Entities;
using SocialProject.DAL.Core.Repositories;

namespace SocialProject.BLL.Common.CQRS.Commands
{
    public class LoginUserCommand : ICommand
    {
        public LoginUserCommand(AuthUserDto model)
        {
            AuthUser = model;
        }

        public AuthUserDto AuthUser { get; }
    }

    public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IRepository<User> _userRepository;

        public LoginUserCommandHandler(IRepository<User> userRepository, IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        public void Handle(LoginUserCommand command)
        {
            var user = _userRepository.Query(x => x.Login == command.AuthUser.Email &&
                                                  x.Password == command.AuthUser.Password);

            if (user != null)
            {
                _authenticationService.Login(user, command.AuthUser.IsRememberMe);
            }
        }
    }
}