using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Dal.Interfaces
{
    public interface IFriendRequestRepository: IRepository<Dal.ORM.FriendRequest>
    {
        Dal.ORM.FriendRequest GetById(int senderId, int receiverId);
    }
}
