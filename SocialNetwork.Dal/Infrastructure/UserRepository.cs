using System;
using SocialNetwork.Dal.ORM;
using System.Data.Entity;

namespace SocialNetwork.Dal.Infrastructure
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(DbContext context): base(context)
        { }
        public override void Update(User entity)
        {
            User user = context.Set<User>().Find(entity.Id);
            user.E_Mail = entity.E_Mail;
            user.PasswordHash = entity.PasswordHash;
            user.RoleId = entity.RoleId;
        }
        public override User GetById(int id)
        {
            return context.Set<User>().Find(id);
        }
    }
}
