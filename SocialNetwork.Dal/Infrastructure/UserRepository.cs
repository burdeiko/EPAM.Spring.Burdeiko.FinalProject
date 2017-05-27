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
            throw new NotImplementedException();
        }
    }
}
