using System;
using System.Collections.Generic;

namespace Univers.Models.Models;

public class FacultyModel
{
    public string? Id { get; set; } = null!;

    public string? UniversityId { get; set; } = null!;

    public string? DeanId { get; set; } = null!;

    public string? ViceDeanId { get; set; } = null!;

    public string? Name { get; set; } = null!;

    public string? Code { get; set; } = null!;

    public StaffModel? Dean { get; set; } = null!;

    public UniversityModel? University { get; set; } = null!;

    public StaffModel? ViceDean { get; set; } = null!;
}
