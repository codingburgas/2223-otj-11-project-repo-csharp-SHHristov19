using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Univers.DAL.Entities;

[PrimaryKey("InstructorId", "ComponentId", "SubjectId")]
public partial class SubjectComponent
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string InstructorId { get; set; } = null!;

    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string ComponentId { get; set; } = null!;

    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string SubjectId { get; set; } = null!;

    [ForeignKey("ComponentId")]
    [InverseProperty("SubjectComponents")]
    public virtual Component Component { get; set; } = null!;

    [ForeignKey("InstructorId")]
    [InverseProperty("SubjectComponents")]
    public virtual Staff Instructor { get; set; } = null!;

    [ForeignKey("SubjectId")]
    [InverseProperty("SubjectComponents")]
    public virtual Subject Subject { get; set; } = null!;
}
