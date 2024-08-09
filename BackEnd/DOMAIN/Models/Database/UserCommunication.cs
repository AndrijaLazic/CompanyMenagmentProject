using System;
using System.Collections.Generic;

namespace DOMAIN.Models.Database;

public partial class UserCommunication
{
    public int Id { get; set; }

    public int User1 { get; set; }

    public int User2 { get; set; }

    public int User1Unread { get; set; }

    public int User2Unread { get; set; }

    public virtual ICollection<CommunicationMessage> CommunicationMessages { get; set; } = new List<CommunicationMessage>();

    public virtual User User1Navigation { get; set; } = null!;

    public virtual User User2Navigation { get; set; } = null!;
}
