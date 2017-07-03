namespace SocialProject.DAL.Common.Entities
{
    /// <summary>
    ///     Модель пользователя
    /// </summary>
    public class User : Entity<long>
    {
        /// <summary>
        ///     Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        ///     Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Город
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///     Штамп для авторизации
        /// </summary>
        public string SecurityStamp { get; set; }
    }
}