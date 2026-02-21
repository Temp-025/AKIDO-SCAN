using System;
using System.Collections.Generic;

namespace EnterpriseGatewayPortal.Core.Domain.Models;

public partial class Filestorage
{
    public int Id { get; set; }

    public string Fileid { get; set; } = null!;

    public byte[]? File { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
}
