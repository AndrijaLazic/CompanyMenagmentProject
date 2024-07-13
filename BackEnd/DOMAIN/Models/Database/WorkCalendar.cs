using System;
using System.Collections.Generic;

namespace DOMAIN.Models.Database;

public partial class WorkCalendar
{
    public DateOnly Date { get; set; }

    public byte Shift { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
