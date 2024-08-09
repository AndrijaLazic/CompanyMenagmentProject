using DOMAIN.Models.Database;
using DOMAIN.Models.DTO;
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

        public CommunicationMessage[] GetAllMessages(int communicationId)
        {
            return _databaseContext.CommunicationMessages.Where(x=> x.CommunicationId == communicationId).ToArray();
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

        public UserCommunication[] GetUserCommunications(int userId)
        {
            return _databaseContext.UserCommunications.Where(x=> x.User1 == userId ||  x.User2 == userId).ToArray();
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
    }
}
