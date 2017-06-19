using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Core.Infrastructure;
using SocialNetwork.Dal.Interfaces;
using SocialNetwork.Dal.ORM;
using SocialNetwork.Core.Interfaces;
using System.Linq.Expressions;

namespace SocialNetwork.Core.Services
{
    public class MessageService : IMessageService
    {
        #region Fields
        private readonly IUnitOfWork uow;
        private readonly IRepository<Dal.ORM.Message> messageRepository;
        #endregion
        #region Constructor
        public MessageService(IUnitOfWork uow, IRepository<Dal.ORM.Message> repository)
        {
            this.uow = uow;
            this.messageRepository = repository;
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Gets all the messages sended to or received from the other user
        /// </summary>
        /// <param name="currentId">The id of the user sended request</param>
        /// <param name="otherId">The id of the other user</param>
        /// <returns></returns>
        public IEnumerable<Message> GetDialogueWith(int currentId, int otherId)
        {
            var sendedMessages = messageRepository.GetByPredicate(SearchExpressionBuilder.ByProperty<SocialNetwork.Dal.ORM.Message, int>(nameof(Dal.ORM.Message.SenderId), currentId)).Where(m => m.ReceiverId == otherId);
            var receivedMessages = messageRepository.GetByPredicate(SearchExpressionBuilder.ByProperty<SocialNetwork.Dal.ORM.Message, int>(nameof(Dal.ORM.Message.ReceiverId), currentId)).Where(m => m.SenderId == otherId);
            return sendedMessages.Union(receivedMessages).OrderBy(m => m.Date).Select(m => m.ToBllMessage());
        }

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="senderId">The sender id</param>
        /// <param name="receiverId">The receiver id</param>
        /// <param name="message">The message to send</param>
        /// <returns></returns>
        public Message SendMessage(int senderId, int receiverId, string message)
        {
            var newMessage = new Message { FromId = senderId, ToId = receiverId, MessageString = message, Time = DateTime.Now };
            messageRepository.Create(newMessage.ToDalMessage());
            uow.Commit();
            return newMessage;
        }

        /// <summary>
        /// Gets the people that have messages with specified person
        /// </summary>
        /// <param name="forPersonId">The person's id</param>
        /// <returns></returns>
        public IEnumerable<int> GetTalkersIds(int forPersonId)
        {
            var sendedMessages = messageRepository.GetByPredicate(SearchExpressionBuilder.ByProperty<Dal.ORM.Message, int>(nameof(Dal.ORM.Message.SenderId), forPersonId)).Select(m => m.ToBllMessage());
            var receivedMessages = messageRepository.GetByPredicate(SearchExpressionBuilder.ByProperty<Dal.ORM.Message, int>(nameof(Dal.ORM.Message.ReceiverId), forPersonId)).Select(m => m.ToBllMessage());
            return sendedMessages.Select(m => m.ToId).Union(receivedMessages.Select(m => m.FromId)).Distinct();
        }

        /// <summary>
        /// Gets all the messages from one person to another sended since the specified date
        /// </summary>
        /// <param name="fromDate">The date to search from</param>
        /// <param name="fromPersonId">The sender of the messages</param>
        /// <param name="toPersonId">The receiver of the messages</param>
        /// <returns></returns>
        public IEnumerable<Message> GetLatestMessages(DateTime fromDate, int fromPersonId, int toPersonId)
        {
            var parameter = Expression.Parameter(typeof(Dal.ORM.Message));
            var date = Expression.Property(parameter, "Date");
            var from = Expression.Constant(fromDate);
            var latest = Expression.GreaterThan(date, from);
            var fromUser = Expression.Property(parameter, "SenderId");
            var fromUserNeeded = Expression.Constant(fromPersonId);
            var isFromUser = Expression.Equal(fromUser, fromUserNeeded);
            var toUser = Expression.Property(parameter, "ReceiverId");
            var toUserNeeded = Expression.Constant(toPersonId);
            var isToUser = Expression.Equal(toUser, toUserNeeded);
            var isMatch = Expression.AndAlso(latest, Expression.AndAlso(isFromUser, isToUser));
            var searchExpression = Expression.Lambda<Func<Dal.ORM.Message, bool>>(isMatch, parameter);
            return messageRepository.GetByPredicate(searchExpression).Select(m => m.ToBllMessage());
        }
        #endregion
    }
}
