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
            var personSet = context.Set<Person>();
            var person = personSet.Find(entity.Id);
            context.Entry(person).CurrentValues.SetValues(entity);
            context.Entry(person).State = EntityState.Modified;
        }
        public override Person GetById(int id)
        {
            return context.Set<Person>().Find(id);
        }
    }
}
