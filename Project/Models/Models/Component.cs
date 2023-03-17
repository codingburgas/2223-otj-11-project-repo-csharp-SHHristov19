using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Component
{
    public string Id { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Activity { get; set; }

    public DateTime DateOfStart { get; set; }

    public TimeSpan Duration { get; set; }

    public string Location { get; set; }

    public string Note { get; set; }
}
