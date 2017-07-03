using SocialProject.BLL.Core.CQRS;
using SocialProject.BLL.Core.Services;

namespace SocialProject.BLL.Common.CQRS.Commands
{
    public class LogoutUserCommand : ICommand
    {
    }

    public class LogoutUserCommandHandler : ICommandHandler<LogoutUserCommand>
    {
        private readonly IAuthenticationService _authenticationService;

        public LogoutUserCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public void Handle(LogoutUserCommand command)
        {
            _authenticationService.Logout();
        }
    }
}