using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Interfaces
{
    public interface IMessageService
    {
        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="senderId">The sender id</param>
        /// <param name="receiverId">The receiver id</param>
        /// <param name="message">The message to send</param>
        /// <returns></returns>
        Message SendMessage(int senderId, int receiverId, string message);

        /// <summary>
        /// Gets all the messages sended to or received from the other user
        /// </summary>
        /// <param name="currentId">The id of the user sended request</param>
        /// <param name="otherId">The id of the other user</param>
        /// <returns></returns>
        IEnumerable<Message> GetDialogueWith(int currentId, int otherId);

        IEnumerable<int> GetTalkersIds(int forPersonId);

        /// <summary>
        /// Gets all the messages from one person to another sended since the specified date
        /// </summary>
        /// <param name="fromDate">The date to search from</param>
        /// <param name="fromPersonId">The sender of the messages</param>
        /// <param name="toPersonId">The receiver of the messages</param>
        /// <returns></returns>
        IEnumerable<Message> GetLatestMessages(DateTime fromDate, int fromPersonId, int toPersonId);
    }
}
