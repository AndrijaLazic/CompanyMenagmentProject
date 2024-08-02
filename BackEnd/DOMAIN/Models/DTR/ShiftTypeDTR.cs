using DOMAIN.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.DTR
{
    public class ShiftTypeDTR
    {
        public byte ShiftNumber { get; set; }

        public string StartTime { get; set; } = null!;

        public string EndTime { get; set; } = null!;

    }
}
