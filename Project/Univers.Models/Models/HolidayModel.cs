using System;
using System.Collections.Generic;

namespace Univers.Models.Models;

public class HolidayModel
{
    public string? Name { get; set; } = null!;

    public DateTime? DateOfStart { get; set; } = null!;

    public DateTime? DateOfEnd { get; set; } = null!;
}
