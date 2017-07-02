using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SocialProject.DAL.Common.Entities;
using SocialProject.DAL.Core;
using SocialProject.Models;

namespace SocialProject.Controllers
{
    public class HomeController : ApiController
    {
        private readonly IRepository<User> _userRepository;

        public HomeController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        ///     Авторизация пользователя
        /// </summary>
        /// <param name="auth">Model</param>
        [HttpPost]
        public IHttpActionResult Autorization(AuthUserDto auth)
        {
            return Ok("OK!");
        }

        /// <summary>
        ///     Регистрация пользователя
        /// </summary>
        /// <param name="regUser">Model</param>
        /// <returns></returns>
        //[HttpPost]
        //public IHttpActionResult Registration(RegisterUserDto regUser)
        //{
        //    return Ok("OK!");
        //}


        /// <summary>
        ///     Получение инф-и о пользователе
        /// </summary>
        /// <param name="id">Ид пользователя</param>
        [HttpGet]
        //[Authorize]        //TODO: Своя реализация
        public IHttpActionResult GetUserInfo(long id)
        {
            _userRepository.Insert(new User
            {
                FirstName = "First",
                LastName = "Last",
                Login = "login",
                Password = "password"
            });

            return Ok(new {name = "Я", last = "Test"});
        }
    }
}
