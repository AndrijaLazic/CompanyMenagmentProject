using System;
using System.Collections.Generic;

namespace DOMAIN.Models.Database;

public partial class ShiftType
{
    public byte ShiftNumber { get; set; }

    public string StartTime { get; set; } = null!;

    public string EndTime { get; set; } = null!;

    public virtual ICollection<WorkCalendar> WorkCalendars { get; set; } = new List<WorkCalendar>();
}
