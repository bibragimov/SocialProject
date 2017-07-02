using System;

namespace SocialProject.DAL.Common.Entities
{
    public abstract class Entity<T>
    {
        public Entity()
        {
            CreateDate = DateTimeOffset.Now;
        }

        public T Id { get; set; }

        public DateTimeOffset CreateDate { get; private set; }
    }
}