using System;
using System.Data.Entity;
using SocialNetwork.Dal.ORM;

namespace SocialNetwork.Dal.Infrastructure
{
    public class RoleRepository: Repository<Role>
    {
        public RoleRepository(DbContext context) : base(context) { }
        public override Role GetById(int id)
        {
            return context.Set<Role>().Find(id);
        }

        public override void Update(Role entity)
        {
            throw new NotImplementedException();
        }
    }
}
