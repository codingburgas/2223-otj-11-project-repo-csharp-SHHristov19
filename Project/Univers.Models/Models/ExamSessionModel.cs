using System;
using System.Collections.Generic;

namespace Univers.Models.Models;

public class ExamSessionModel
{
    public string? Id { get; set; } = null!;

    public DateTime? DateOfStart { get; set; } = null!;

    public DateTime? DateOfEnd { get; set; } = null!;

    public string? Type { get; set; } = null!;
}
