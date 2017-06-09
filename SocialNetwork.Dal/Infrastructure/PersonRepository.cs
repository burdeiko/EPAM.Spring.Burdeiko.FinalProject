using System;
using System.Data.Entity;
using SocialNetwork.Dal.ORM;

namespace SocialNetwork.Dal.Infrastructure
{
    public class PersonRepository: Repository<Person>
    {
        public PersonRepository(DbContext context) : base(context) { }

        public override void Update(Person entity)
        {
            throw new NotImplementedException();
        }
        public override Person GetById(int id)
        {
            return context.Set<Person>().Find(id);
        }
    }
}
