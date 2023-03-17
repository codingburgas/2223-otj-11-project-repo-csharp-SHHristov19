using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Staff
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
