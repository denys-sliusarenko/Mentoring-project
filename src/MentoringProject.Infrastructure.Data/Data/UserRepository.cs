using MentoringProject.Domain.Core.Entities;
using MentoringProject.Domain.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MentoringProject.Infrastructure.Data.Data
{
    public class UserRepository : IRepository<User>
    {
        private DbProjectContext db;

        public UserRepository(DbProjectContext context)
        {
            this.db = context;
        }
        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
