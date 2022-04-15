﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Oqtane.ChatHubs.Models
{
    public class ChatHubRoomChatHubBlacklistUser : ChatHubBaseModel
    {

        public int ChatHubRoomId { get; set; }
        public int ChatHubBlacklistUserId { get; set; }


        [NotMapped] public virtual ChatHubRoom Room { get; set; }
        [NotMapped] public virtual ChatHubBlacklistUser BlacklistUser { get; set; }

    }
}
