using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Interfaces
{
    public interface IPersonService: IService<Person>
    {
        IEnumerable<Person> FindByFirstName(string firstName);
        Person GetById(int id);

        IEnumerable<Person> GetFriendRequestSenders(int personId);
        IEnumerable<Person> GetFriendRequestReceivers(int personId);
        IEnumerable<Person> GetFriends(int personId);
        void SendFriendRequest(int senderId, int receiverId);
        void AcceptFriendRequest(int senderId, int receiverId);
    }
}
