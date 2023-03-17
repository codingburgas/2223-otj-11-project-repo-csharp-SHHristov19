using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Speciality
{
    public string Id { get; set; } = null!;

    public string TutorId { get; set; }

    public string Name { get; set; } = null!;

    public string Degree { get; set; }

    public int Semesters { get; set; }

    public string Code { get; set; }

    public virtual Staff Tutor { get; set; }
}
