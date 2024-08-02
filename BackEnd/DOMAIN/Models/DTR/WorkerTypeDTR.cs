using DOMAIN.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.DTR
{
    public class WorkerTypeDTR
    {
        public byte Id { get; set; }

        public string TypeName { get; set; } = null!;
    }
}
