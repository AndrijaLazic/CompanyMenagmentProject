using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.DTR
{
    public class FrontEndSettingsDTR
    {
        public List<UserDTR> userDTRs {  get; set; }
        public List<ShiftTypeDTR> shiftDTRs { get; set; }
        public List<WorkerTypeDTR> workerTypesDTRs { get; set; }
    }
}
