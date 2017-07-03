using SocialProject.DAL.Common.Entities;

namespace SocialProject.BLL.Core.Services
{
    public interface IAuthenticationService
    {
        void Login(User user, bool remember = true);

        void Logout();
    }
}