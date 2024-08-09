using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.DTO
{
    public class ChatMessageDTO
    {
        public string Message {  get; set; }
        public int receiverId { get; set; }
    }
}
