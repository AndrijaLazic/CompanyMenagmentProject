using System;
using System.Collections.Generic;

namespace DOMAIN.Models.Database;

public partial class WorkerType
{
    public byte Id { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
