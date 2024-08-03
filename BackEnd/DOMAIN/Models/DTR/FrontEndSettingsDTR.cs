using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.DTR
{
    public class FrontEndSettingsDTR
    {
        public List<UserDTR> users {  get; set; }
        public List<ShiftTypeDTR> shiftTypes { get; set; }
        public List<WorkerTypeDTR> workerTypes { get; set; }
    }
}
