using System;
using System.Collections.Generic;

namespace DOMAIN.Models.Database;

public partial class WorkCalendar
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public byte Shift { get; set; }

    public int UserId { get; set; }

    public virtual ShiftType ShiftNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
