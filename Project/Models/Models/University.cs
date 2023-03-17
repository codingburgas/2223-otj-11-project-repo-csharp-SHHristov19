using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class University
{
    public string Id { get; set; } = null!;

    public string RectorId { get; set; } = null!;

    public string Name { get; set; }

    public string Address { get; set; }

    public int Capacity { get; set; }

    public virtual Staff Rector { get; set; } = null!;
}
