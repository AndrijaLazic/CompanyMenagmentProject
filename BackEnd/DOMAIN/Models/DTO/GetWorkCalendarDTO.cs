using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.DTO
{
    public class GetWorkCalendarDTO
    {
        public string dateString {  get; set; }
        public int offset { get; set; } = 0;
        public int numOfRows { get; set; } = int.MaxValue;
    }
}
