using System;
using System.Collections.Generic;

namespace Univers.Models.Models;

public class UniversityModel
{
    public string? Id { get; set; } = null!;

    public string? RectorId { get; set; } = null!;

    public string? Name { get; set; } = null!;

    public string? Address { get; set; } = null!;

    public int? Capacity { get; set; } = null!;

    public StaffModel? Rector { get; set; } = null!;

    public string RectorName { get; set; } = null!;
}
