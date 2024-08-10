using DOMAIN.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.DTR
{
    public class UserCommunicationDTR
    {
        public int Id { get; set; }

        public int User1 { get; set; }

        public int User2 { get; set; }

        public int User1Unread { get; set; }

        public int User2Unread { get; set; }

        public List<CommunicationMessageDTR> communicationMessages { get; set; }

        public static UserCommunicationDTR ToDTR(UserCommunication userCommunication)
        {
            UserCommunicationDTR userCommunication1 = new UserCommunicationDTR()
            {
                Id = userCommunication.Id,
                User1 = userCommunication.User1,
                User2 = userCommunication.User2,
                User1Unread = userCommunication.User1Unread,
                User2Unread = userCommunication.User2Unread,
                communicationMessages = new List<CommunicationMessageDTR>()
            };
            CommunicationMessage[] list = userCommunication.CommunicationMessages.ToArray();
            for (int i = 0; i < list.Length; i++)
            {
                userCommunication1.communicationMessages.Add(CommunicationMessageDTR.ToDTR(list[i]));
            }
            return userCommunication1;
        }
    }
}
