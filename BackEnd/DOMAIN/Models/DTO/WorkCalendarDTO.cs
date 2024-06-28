using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.DTO
{
    public class WorkCalendarDTO
    {
        /// <summary>
        /// Needs to be in format: 2024-07-27
        /// </summary>
        [Required(ErrorMessage = "DateRequired")]
        public DateOnly Date { get; init; }

        [Required(ErrorMessage = "ShiftRequired")]
        public byte Shift { get; init; }

        [Required(ErrorMessage = "UserIdRequired")]
        public int UserId { get; init; }

    }
}
