﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Interfaces
{
    public interface IMessageService
    {
        Message SendMessage(int senderId, int receiverId, string message);

        IEnumerable<Message> GetDialogueWith(int currentId, int otherId);

        IEnumerable<Message> GetDialoguesPreview(int forPersonId);

        IEnumerable<int> GetTalkersIds(int forPersonId);

        IEnumerable<Message> GetLatestMessages(DateTime fromDate, int fromPersonId, int toPersonId);
    }
}
