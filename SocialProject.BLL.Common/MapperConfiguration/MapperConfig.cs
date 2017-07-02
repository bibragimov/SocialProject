using System;
using ExpressMapper;
using SocialProject.BLL.Common.Models;
using SocialProject.DAL.Common.Entities;

namespace SocialProject.BLL.Common.MapperConfiguration
{
    public class MapperConfig
    {
        public static void Configure()
        {
            Mapper.Register<User, UserInfoDto>()
                .Member(x => x.Email, y => y.Login)
                .Member(x => x.CreateDate, y => y.CreateDate.ToString("dd.MM.yy"));
            Mapper.Register<RegisterUserDto, User>()
                .Member(x => x.Login, y => y.Email)
                .Member(x => x.SecurityStamp, y => KeyGenerator.GetSecurityStamp());
        }
    }

    public static class KeyGenerator
    {
        public static string GetSecurityStamp()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}