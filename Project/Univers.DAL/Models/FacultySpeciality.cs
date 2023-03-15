using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

[Keyless]
[Table("FacultySpeciality")]
public partial class FacultySpeciality
{
    [StringLength(50)]
    [Unicode(false)]
    public string FacultyId { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string SpecialityId { get; set; } = null!;

    [ForeignKey("FacultyId")]
    public virtual Faculty Faculty { get; set; } = null!;

    [ForeignKey("SpecialityId")]
    public virtual Speciality Speciality { get; set; } = null!;
}
