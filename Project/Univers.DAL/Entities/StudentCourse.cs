using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Univers.DAL.Entities;

[PrimaryKey("StudentId", "SemesterId")]
[Table("StudentCourse")]
public partial class StudentCourse
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string StudentId { get; set; } = null!;

    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string SemesterId { get; set; } = null!;

    public int? Course { get; set; }

    [ForeignKey("SemesterId")]
    [InverseProperty("StudentCourses")]
    public virtual Semester Semester { get; set; } = null!;

    [ForeignKey("StudentId")]
    [InverseProperty("StudentCourses")]
    public virtual Student Student { get; set; } = null!;
}
