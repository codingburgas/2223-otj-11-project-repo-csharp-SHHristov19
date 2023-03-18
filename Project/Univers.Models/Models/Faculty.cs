using System;
using System.Collections.Generic;

namespace Univers.Models.Models;

public class Faculty
{
    public string? Id { get; set; } = null!;

    public string? UniversityId { get; set; } = null!;

    public string? DeanId { get; set; } = null!;

    public string? ViceDeanId { get; set; } = null!;

    public string? Name { get; set; } = null!;

    public string? Code { get; set; } = null!;

    public Staff? Dean { get; set; } = null!;

    public University? University { get; set; } = null!;

    public Staff? ViceDean { get; set; } = null!;
}
