using System;
using System.Collections.Generic;

namespace Univers.Models.Models;

public class StaffModel
{
    public string? Id { get; set; } = null!;

    public string? UserId { get; set; } = null!;

    public string? Role { get; set; } = null!;

    public UserModel? User { get; set; } = null!;
}
