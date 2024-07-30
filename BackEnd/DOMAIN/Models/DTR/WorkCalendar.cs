using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.DTR
{
    public class WorkCalendarAllUsersDTR
    {
        public DateTime Date { get; set; }
        public byte Shift { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
     
        public byte WorkerType { get; set; }
    }
}
