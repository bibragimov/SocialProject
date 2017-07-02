using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using SocialProject.BLL.Common.CQRS.Commands;
using SocialProject.BLL.Common.CQRS.Queries.Users;
using SocialProject.BLL.Common.Models;
using SocialProject.BLL.Core.CQRS;
using SocialProject.DAL.Common.Entities;

namespace SocialProject.Controllers
{
    [SocialApiAuthorize(typeof(User))]
    public class AccountController : ApiController
    {
        private readonly AuthenticationService _authenticationService;
        private readonly IQueryHandler<CheckAuthQuery, User> _checkAuthQueryHandler;
        private readonly ICommandHandler<CreateUserCommand> _userCommandHandler;
        private readonly IQueryHandler<GetUserInfoQuery, UserInfoDto> _userQueryHandler;
        private readonly IQueryHandler<GetAllUsersQuery, List<UserInfoDto>> _usersQueryHandler;

        public AccountController(IQueryHandler<GetUserInfoQuery, UserInfoDto> userQueryHandler,
            IQueryHandler<GetAllUsersQuery, List<UserInfoDto>> usersQueryHandler,
            ICommandHandler<CreateUserCommand> userCommandHandler,
            AuthenticationService authenticationService,
            IQueryHandler<CheckAuthQuery, User> checkAuthQueryHandler)
        {
            _userQueryHandler = userQueryHandler;
            _usersQueryHandler = usersQueryHandler;
            _userCommandHandler = userCommandHandler;
            _authenticationService = authenticationService;
            _checkAuthQueryHandler = checkAuthQueryHandler;
        }

        /// <summary>
        ///     Регистрация пользователя
        /// </summary>
        /// <remarks>Регистрация нового пользователя</remarks>
        /// <param name="regUser">Model</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Registration(RegisterUserDto regUser)
        {
            _userCommandHandler.Handle(new CreateUserCommand(regUser));
            return Ok();
        }

        /// <summary>
        ///     Авторизация пользователя
        /// </summary>
        /// <param name="auth">Model</param>
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Autorization(AuthUserDto auth)
        {
            var result = _checkAuthQueryHandler.Handle(new CheckAuthQuery(auth));

            if (result != null)
            {
                _authenticationService.Login(result, auth.IsRememberMe);
                return Ok();
            }
            return Content(HttpStatusCode.BadRequest, "Unauthorized");
        }


        /// <summary>
        ///     Получение пользователей
        /// </summary>
        /// <param name="count">Кол-во пользователей</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetAllUsers(int count = 10)
        {
            var users = _usersQueryHandler.Handle(new GetAllUsersQuery(count));

            return Ok(users);
        }

        /// <summary>
        ///     Получение инф-и о пользователе
        /// </summary>
        /// <param name="id">Ид пользователя</param>
        [HttpGet]
        public IHttpActionResult GetUserInfo(long id)
        {
            var user = _userQueryHandler.Handle(new GetUserInfoQuery(id));

            return Ok(user);
        }

        /// <summary>
        ///     Выход из системы
        /// </summary>
        [HttpGet]
        public IHttpActionResult Logout()
        {
            _authenticationService.Logout();
            return Ok();
        }
    }
}