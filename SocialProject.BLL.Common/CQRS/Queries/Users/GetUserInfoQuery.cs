using ExpressMapper;
using SocialProject.BLL.Common.Models;
using SocialProject.BLL.Core.CQRS;
using SocialProject.DAL.Common.Entities;
using SocialProject.DAL.Core.Repositories;

namespace SocialProject.BLL.Common.CQRS.Queries.Users
{
    public class GetUserInfoQuery : IQuery
    {
        public GetUserInfoQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }

    public class GetUserInfoQueryHandler : IQueryHandler<GetUserInfoQuery, UserInfoDto>
    {
        private readonly IRepository<User> _userRepository;

        public GetUserInfoQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public UserInfoDto Handle(GetUserInfoQuery query)
        {
            var user = _userRepository.Get(query.Id);

            return Mapper.Map<User, UserInfoDto>(user);
        }
    }
}