using System;
using System.Data.Entity;
using SocialNetwork.Dal.ORM;

namespace SocialNetwork.Dal.Infrastructure
{
    public class RoleRepository: Repository<Role>
    {
        public RoleRepository(DbContext context) : base(context) { }

        public override void Update(Role entity)
        {
            throw new NotImplementedException();
        }
    }
}
