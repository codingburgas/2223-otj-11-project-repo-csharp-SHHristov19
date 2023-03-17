using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class ExamSession
{
    public string Id { get; set; } = null!;

    public DateTime DateOfStart { get; set; }

    public DateTime DateOfEnd { get; set; }

    public string Type { get; set; }
}
