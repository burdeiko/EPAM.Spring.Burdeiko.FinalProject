using System;
using System.Data.Entity;
using SocialNetwork.Dal.ORM;

namespace SocialNetwork.Dal.Infrastructure
{
    public class MessageRepository : Repository<Message>
    {
        public MessageRepository(DbContext context) : base(context)
        {
        }

        public override Message GetById(int id)
        {
            return context.Set<Message>().Find(id);
        }

        public override void Update(Message entity)
        {
            var messageSet = context.Set<Message>();
            var message = messageSet.Find(entity.Id);
            context.Entry(message).CurrentValues.SetValues(entity);
            context.Entry(message).State = EntityState.Modified;
        }
    }
}
