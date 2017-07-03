using System.Collections.Generic;
using System.Linq;
using ExpressMapper;
using SocialProject.BLL.Common.Models;
using SocialProject.BLL.Core.CQRS;
using SocialProject.DAL.Common.Entities;
using SocialProject.DAL.Core.Repositories;

namespace SocialProject.BLL.Common.CQRS.Queries.Users
{
    public class GetAllUsersQuery : IQuery
    {
        public GetAllUsersQuery(int count)
        {
            Count = count;
        }

        public int Count { get; }
    }

    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, List<UserInfoDto>>
    {
        private readonly IRepository<User> _userRepository;

        public GetAllUsersQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserInfoDto> Handle(GetAllUsersQuery query)
        {
            var users = _userRepository.All().Take(query.Count).ToList();

            return Mapper.Map<List<User>, List<UserInfoDto>>(users);
        }
    }
}