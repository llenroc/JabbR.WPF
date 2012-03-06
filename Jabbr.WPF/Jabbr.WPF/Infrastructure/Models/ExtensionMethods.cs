﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JabbrModels = JabbR.Client.Models;

namespace Jabbr.WPF.Infrastructure.Models
{
    internal static class ExtensionMethods
    {
        public static UserStatus Translate(this JabbrModels.UserStatus userStatus)
        {
            UserStatus toReturn = UserStatus.Offline;

            switch (userStatus)
            {
                case JabbR.Client.Models.UserStatus.Active:
                    toReturn = UserStatus.Active;
                    break;
                case JabbR.Client.Models.UserStatus.Inactive:
                    toReturn = UserStatus.Inactive;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("userStatus");
            }

            return toReturn;
        }
    }
}
