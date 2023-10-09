using System;
using System.Collections.Generic;

namespace Univers.Models.Models;

public class ComponentModel
{
    public string? Id { get; set; } = null!;

    public string? Type { get; set; } = null!;

    public string? Activity { get; set; } = null!;

    public DateTime? DateOfStart { get; set; } = null!;

    public TimeSpan? Duration { get; set; } = null!;

    public string? Location { get; set; } = null!;

    public string? Note { get; set; } = null!;
}
