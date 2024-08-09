using System;
using System.Collections.Generic;

namespace DOMAIN.Models.Database;

public partial class CommunicationMessage
{
    public int Id { get; set; }

    public int CommunicationId { get; set; }

    public int SenderId { get; set; }

    public string Message { get; set; } = null!;

    public virtual UserCommunication Communication { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
