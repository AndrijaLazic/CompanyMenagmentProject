using DOMAIN.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.DTR
{
    public class CommunicationMessageDTR
    {
        public int Id { get; set; }

        public int CommunicationId { get; set; }

        public int SenderId { get; set; }

        public string Message { get; set; } = null!;
        public static CommunicationMessageDTR ToDTR(CommunicationMessage message)
        {
            return new CommunicationMessageDTR()
            {
                Id = message.Id,
                CommunicationId = message.CommunicationId,
                SenderId = message.SenderId,
                Message = message.Message,
            };
        }
    }
}
