using System;
using System.Collections.Generic;

namespace DOMAIN.Models.Database;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public byte WorkerType { get; set; }

    public virtual WorkerType WorkerTypeNavigation { get; set; } = null!;
}
