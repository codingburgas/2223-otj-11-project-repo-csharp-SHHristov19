using System;
using System.Collections.Generic;

namespace Univers.Models.Models;

public class Staff
{
    public string? Id { get; set; } = null!;

    public string? UserId { get; set; } = null!;

    public string? Role { get; set; } = null!;

    public User? User { get; set; } = null!;
}
