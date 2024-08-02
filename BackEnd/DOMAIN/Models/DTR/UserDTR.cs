using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.DTR
{
    public class UserDTR
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public byte WorkerType { get; set; }
    }
}
