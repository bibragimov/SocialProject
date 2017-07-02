using SocialProject.BLL.Common.Models;
using SocialProject.BLL.Core.CQRS;
using SocialProject.DAL.Common.Entities;
using SocialProject.DAL.Core.Repositories;

namespace SocialProject.BLL.Common.CQRS.Queries.Users
{
    public class CheckAuthQuery : IQuery
    {
        public CheckAuthQuery(AuthUserDto model)
        {
            AuthUser = model;
        }

        public AuthUserDto AuthUser { get; private set; }
    }

    public class CheckAuthQueryHandler : IQueryHandler<CheckAuthQuery, User>
    {
        private readonly IRepository<User> _userRepository;

        public CheckAuthQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User Handle(CheckAuthQuery query)
        {
            // TODO: нельзя передаать юзера

            var user = _userRepository.Query(x => x.Login == query.AuthUser.Email &&
                                                  x.Password == query.AuthUser.Password);

            return user;
        }
    }
}