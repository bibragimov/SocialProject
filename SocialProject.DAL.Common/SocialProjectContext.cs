using System.Data.Entity;
using SocialProject.DAL.Common.Entities;
using SocialProject.DAL.Core;

namespace SocialProject.DAL.Common
{
    public class SocialProjectContext : DbContext, IDBContext
    {
        public SocialProjectContext()
            : base("SocialProjectContext")
        {
        }

        public DbSet<User> Users { get; set; }
    }
}