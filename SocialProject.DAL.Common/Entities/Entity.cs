using System;

namespace SocialProject.DAL.Common.Entities
{
    /// <summary>
    ///     Абстрактное представление записей в БД
    /// </summary>
    public abstract class Entity<T>
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        protected Entity()
        {
            CreateDate = DateTimeOffset.Now;
        }

        /// <summary>
        ///     Ид записи
        /// </summary>
        public T Id { get; set; }

        /// <summary>
        ///     Дата создания записи
        /// </summary>
        public DateTimeOffset CreateDate { get; private set; }
    }
}