using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SocialProject.DAL.Core.Repositories
{
    /// <summary>
    ///     Интерфейс репозитория
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        ///     Получение всех записей
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> All();

        /// <summary>
        ///     Создание записи
        /// </summary>
        /// <param name="item"></param>
        void Insert(T item);

        /// <summary>
        ///     Получение эл-та по ид
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(long id);

        /// <summary>
        ///     Запрос к бд с фильтром
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        T Query(Expression<Func<T, bool>> filter = null);

        /// <summary>
        ///     Обновление записи
        /// </summary>
        /// <param name="item"></param>
        void Update(T item);

        /// <summary>
        ///     Удаление записи по ид
        /// </summary>
        /// <param name="id"></param>
        void Delete(long id);

        /// <summary>
        ///     Удаление записи
        /// </summary>
        /// <param name="item"></param>
        void Delete(T item);

        /// <summary>
        ///     Сохранение изменения в БД
        /// </summary>
        void SaveChanges();
    }
}