using ExpressMapper;
using SocialProject.BLL.Common.Models;
using SocialProject.BLL.Core.CQRS;
using SocialProject.DAL.Common.Entities;
using SocialProject.DAL.Core.Repositories;

namespace SocialProject.BLL.Common.CQRS.Commands
{
    public class CreateUserCommand : ICommand
    {
        public CreateUserCommand(RegisterUserDto newUser)
        {
            NewUser = newUser;
        }

        public RegisterUserDto NewUser { get; }
    }

    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IRepository<User> _userRepository;

        public CreateUserCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Handle(CreateUserCommand command)
        {
            var user = Mapper.Map<RegisterUserDto, User>(command.NewUser);

            _userRepository.Insert(user);
            _userRepository.SaveChanges();
        }
    }
}