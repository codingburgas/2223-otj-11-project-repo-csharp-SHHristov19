using System;
using System.Collections.Generic;

namespace UniversModels.Models;

public class ExamSession
{
    public string? Id { get; set; } = null!;

    public DateTime? DateOfStart { get; set; } = null!;

    public DateTime? DateOfEnd { get; set; } = null!;

    public string? Type { get; set; } = null!;
}
