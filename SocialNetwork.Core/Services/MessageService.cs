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
        private readonly IUnitOfWork uow;
        private readonly IRepository<Dal.ORM.Message> messageRepository;
        public MessageService(IUnitOfWork uow, IRepository<Dal.ORM.Message> repository)
        {
            this.uow = uow;
            this.messageRepository = repository;
        }
        public IEnumerable<Message> GetDialogueWith(int currentId, int otherId)
        {
            var sendedMessages = messageRepository.GetByPredicate(SearchExpressionBuilder.ByProperty<SocialNetwork.Dal.ORM.Message, int>(nameof(Dal.ORM.Message.SenderId), currentId)).Where(m => m.ReceiverId == otherId);
            var receivedMessages = messageRepository.GetByPredicate(SearchExpressionBuilder.ByProperty<SocialNetwork.Dal.ORM.Message, int>(nameof(Dal.ORM.Message.ReceiverId), currentId)).Where(m => m.SenderId == otherId);
            return sendedMessages.Union(receivedMessages).OrderBy(m => m.Date).Select(m => m.ToBllMessage());
        }

        public void SendMessage(int senderId, int receiverId, string message)
        {
            messageRepository.Create(new Message { FromId = senderId, ToId = receiverId, MessageString = message, Time = DateTime.Now }.ToDalMessage());
        }

        public IEnumerable<Message> GetDialoguesPreview(int forPersonId)
        {
            var sendedMessages = messageRepository.GetByPredicate(SearchExpressionBuilder.ByProperty<SocialNetwork.Dal.ORM.Message, int>(nameof(Dal.ORM.Message.SenderId), forPersonId)).Select(m => m.ToBllMessage());
            var receivedMessages = messageRepository.GetByPredicate(SearchExpressionBuilder.ByProperty<SocialNetwork.Dal.ORM.Message, int>(nameof(Dal.ORM.Message.ReceiverId), forPersonId)).Select(m => m.ToBllMessage());
            sendedMessages = sendedMessages.OrderBy(m => m.ToId).ThenBy(m => m.Time).Reverse();
            List<Message> previewSendedMessages = new List<Message>();
            int lastTalkerId = -1;
            foreach (var message in sendedMessages)
            {
                if (message.ToId != lastTalkerId)
                {
                    lastTalkerId = message.ToId;
                    previewSendedMessages.Add(message);
                }
            }
            lastTalkerId = -1;
            List<Message> previewReceivedMessages = new List<Message>();
            foreach (var message in receivedMessages)
            {
                if (message.FromId != lastTalkerId)
                {
                    lastTalkerId = message.FromId;
                    previewReceivedMessages.Add(message);
                }
            }
            var previewMessages = previewSendedMessages.Join(previewReceivedMessages, m => m.ToId, m => m.FromId, (m1, m2) => m1.Time > m2.Time ? m1 : m2).Union(sendedMessages.Where(m => !receivedMessages.Select(mod => mod.FromId).Contains(m.ToId))).Union(receivedMessages.Where(m => !sendedMessages.Select(mod => mod.ToId).Contains(m.FromId)));
            return previewMessages;
        }
    }
}
