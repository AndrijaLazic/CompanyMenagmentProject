using DOMAIN.Models.Database;
using DOMAIN.Models.DTO;
using DOMAIN.Models.DTR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CommunicationDB
    {
        private CompanyMenagmentProjectContext _databaseContext;

        public CommunicationDB(CompanyMenagmentProjectContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void AddNewCommunication(int user1Id, int user2Id)
        {
            int pom;
            if (user1Id > user2Id)
            {
                pom = user2Id;
                user2Id = user1Id;
                user1Id = pom;
            }
            _databaseContext.UserCommunications.Add(new UserCommunication()
            {
                User1 = user1Id,
                User2 = user2Id
            });
            _databaseContext.SaveChanges();
        }

        public CommunicationMessageDTR[] GetAllMessages(int communicationId)
        {
            CommunicationMessage[] communicationMessages = _databaseContext.CommunicationMessages.Where(x => x.CommunicationId == communicationId).ToArray();
            CommunicationMessageDTR[] array = new CommunicationMessageDTR[communicationMessages.Length];
            for (int i = 0; i < communicationMessages.Length; i++)
            {
                array[i] = CommunicationMessageDTR.ToDTR(communicationMessages[i]);
            }
            return array;
        }

        public UserCommunication? GetCommunication(int communicationId)
        {
            return _databaseContext.UserCommunications.Where(x=>x.Id == communicationId).FirstOrDefault();
        }

        public UserCommunication? GetCommunication(int user1Id,int user2Id)
        {
            int pom;
            if(user1Id > user2Id)
            {
                pom = user2Id;
                user2Id = user1Id;
                user1Id = pom;
            }
            return _databaseContext.UserCommunications.Where(x => x.User1 == user1Id && x.User2 == user2Id).FirstOrDefault();
        }

        public UserCommunicationDTR[] GetUserCommunications(int userId)
        {

            UserCommunication[] userCommunications = _databaseContext.UserCommunications.Where(x => x.User1 == userId || x.User2 == userId).ToArray();
            UserCommunicationDTR[] array = new UserCommunicationDTR[userCommunications.Length];
            for (int i = 0; i < userCommunications.Length; i++)
            {
                array[i] = UserCommunicationDTR.ToDTR(userCommunications[i]);
            }
            return array;
        }

        public void AddMessage(ChatMessageDTO messageDTO,int communicationID,int senderID)
        {
            _databaseContext.CommunicationMessages.Add(new CommunicationMessage()
            {
                CommunicationId = communicationID,
                Message = messageDTO.Message,
                SenderId = senderID,
            });
            _databaseContext.SaveChanges();
        }

        public void IncreaseMessagesUnread(int communicationId,int receiver)
        {
            var comm = _databaseContext.UserCommunications.FirstOrDefault(x => x.Id == communicationId);
            if (comm.User1 == receiver)
            {
                comm.User1Unread++;
            }
            else
            {
                comm.User2Unread++;
            }
            _databaseContext.SaveChanges();
        }

        public void DecresaseMessagesUnread(int communicationId, int receiver)
        {
            var comm = _databaseContext.UserCommunications.FirstOrDefault(x => x.Id == communicationId);
            if (comm.User1 == receiver)
            {
                comm.User1Unread = 0;
            }
            else
            {
                comm.User2Unread = 0;
            }
            _databaseContext.SaveChanges();
        }
    }
}
