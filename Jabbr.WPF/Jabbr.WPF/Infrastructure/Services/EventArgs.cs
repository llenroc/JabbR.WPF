﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jabbr.WPF.Messages;

namespace Jabbr.WPF.Infrastructure.Services
{
    public class RoomEventArgs : EventArgs
    {
        public string Room { get; set; }

        public RoomEventArgs()
        {
        }

        public RoomEventArgs(string room)
        {
            Room = room;
        }
    }

    public class MessageProcessedEventArgs : RoomEventArgs
    {
        public ChatMessageViewModel MessageViewModel { get; set; }

        public MessageProcessedEventArgs(ChatMessageViewModel message, string room)
        {
            Room = room;
            MessageViewModel = message;
        }
    }
}