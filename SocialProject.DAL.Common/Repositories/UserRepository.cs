using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SocialProject.DAL.Common.Entities;
using SocialProject.DAL.Core;
using SocialProject.DAL.Core.Repositories;

namespace SocialProject.DAL.Common.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly DbSet<User> _db;
        private readonly SocialProjectContext _dbase;

        public UserRepository(IDBContext db)
        {
            _dbase = (SocialProjectContext) db;
            _db = _dbase.Set<User>();
        }

        public IEnumerable<User> All()
        {
            return _db.ToList();
        }

        public void Insert(User item)
        {
            _db.Add(item);
        }

        public User Get(long id)
        {
            return _db.Find(id);
        }

        public User Query(Expression<Func<User, bool>> filter)
        {
            return _db.Where(filter).FirstOrDefault();
        }


        public void Update(User item)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public void Delete(User item)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _dbase.SaveChanges();
        }
    }
}