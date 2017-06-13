using System;
using System.Data.Entity;
using SocialNetwork.Dal.ORM;

namespace SocialNetwork.Dal.Infrastructure
{
    public class FriendRequestRepository : Repository<FriendRequest>, Dal.Interfaces.IFriendRequestRepository
    {
        public FriendRequestRepository(DbContext context) : base(context)
        {
        }

        public override FriendRequest GetById(int id)
        {
            throw new NotImplementedException();
        }

        public FriendRequest GetById(int senderId, int receiverId)
        {
            return context.Set<FriendRequest>().Find(senderId, receiverId );
        }

        public override void Update(FriendRequest entity)
        {
            var requestSet = context.Set<FriendRequest>();
            var request = requestSet.Find(entity.SenderId, entity.ReceiverId);
            context.Entry(request).CurrentValues.SetValues(entity);
            context.Entry(request).State = EntityState.Modified;
        }
    }
}
