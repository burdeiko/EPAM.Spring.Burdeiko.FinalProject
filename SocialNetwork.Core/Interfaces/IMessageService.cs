using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Interfaces
{
    public interface IMessageService
    {
        void SendMessage(int senderId, int receiverId, string message);

        IEnumerable<Message> GetDialogueWith(int currentId, int otherId);
        
    }
}
