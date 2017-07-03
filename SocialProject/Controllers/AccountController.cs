using System.Collections.Generic;
using System.Web.Http;
using SocialProject.Authorize;
using SocialProject.BLL.Common.CQRS.Commands;
using SocialProject.BLL.Common.CQRS.Queries.Users;
using SocialProject.BLL.Common.Models;
using SocialProject.BLL.Core.CQRS;
using SocialProject.DAL.Common.Entities;

namespace SocialProject.Controllers
{
    [SocialApiAuthorize(typeof (User))]
    public class AccountController : ApiController
    {
        private readonly ICommandHandler<LoginUserCommand> _loginCommandHandler;
        private readonly ICommandHandler<CreateUserCommand> _userCommandHandler;
        private readonly IQueryHandler<GetUserInfoQuery, UserInfoDto> _userQueryHandler;
        private readonly IQueryHandler<GetAllUsersQuery, List<UserInfoDto>> _usersQueryHandler;
        private readonly ICommandHandler<LogoutUserCommand> _logoutCommandHandler;

        public AccountController(IQueryHandler<GetUserInfoQuery, UserInfoDto> userQueryHandler,
            IQueryHandler<GetAllUsersQuery, List<UserInfoDto>> usersQueryHandler,
            ICommandHandler<CreateUserCommand> userCommandHandler,
            ICommandHandler<LogoutUserCommand> logoutCommandHandler, 
            ICommandHandler<LoginUserCommand> loginCommandHandler)
        {
            _userQueryHandler = userQueryHandler;
            _usersQueryHandler = usersQueryHandler;
            _userCommandHandler = userCommandHandler;
            _logoutCommandHandler = logoutCommandHandler;
            _loginCommandHandler = loginCommandHandler;
        }

        /// <summary>
        ///     Регистрация пользователя
        /// </summary>
        /// <remarks>Регистрация нового пользователя</remarks>
        /// <param name="regUser">Model</param>
        [AllowAnonymous]
        [HttpPost]
        [Route("account/registration")]
        public IHttpActionResult Registration(RegisterUserDto regUser)
        {
            _userCommandHandler.Handle(new CreateUserCommand(regUser));
            return Ok();
        }

        /// <summary>
        ///     Авторизация пользователя
        /// </summary>
        /// <param name="auth">Модель авторизации пользователя</param>
        [AllowAnonymous]
        [HttpPost]
        [Route("account/login")]
        public IHttpActionResult Autorization(AuthUserDto auth)
        {
            _loginCommandHandler.Handle(new LoginUserCommand(auth));

            return Ok();
        }


        /// <summary>
        ///     Получение пользователей
        /// </summary>
        /// <param name="count">Кол-во пользователей</param>
        /// <returns>Список пользователей</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("account/getUsers")]
        public IHttpActionResult GetAllUsers(int count = 10)
        {
            var users = _usersQueryHandler.Handle(new GetAllUsersQuery(count));

            return Ok(users);
        }

        /// <summary>
        ///     Получение инф-и о пользователе
        /// </summary>
        /// <param name="id">Ид пользователя</param>
        /// <returns>Пользователь</returns>
        [HttpGet]
        [Route("account/getUserInfo")]
        public IHttpActionResult GetUserInfo(long id)
        {
            var user = _userQueryHandler.Handle(new GetUserInfoQuery(id));

            return Ok(user);
        }

        /// <summary>
        ///     Выход из системы
        /// </summary>
        [HttpGet]
        [Route("account/logout")]
        public IHttpActionResult Logout()
        {
            _logoutCommandHandler.Handle(new LogoutUserCommand());

            return Ok();
        }
    }
}