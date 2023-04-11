using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Univers.DAL.Entities;

public partial class Student
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? SpecialityId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string UserId { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string? Identity { get; set; }

    [StringLength(60)]
    public string? Citizenship { get; set; } 

    public DateTime? DateOfStarting { get; set; }

    public DateTime? DateOfGraduate { get; set; }

    [StringLength(50)]
    public string? FormOfEducation { get; set; }

    public DateTime? DateOfBirth { get; set; }

    [StringLength(50)]
    public string? CountryOfBirth { get; set; }

    [StringLength(50)]
    public string? MunicipalityOfBirth { get; set; }

    [StringLength(50)]
    public string? AreaOfBirth { get; set; }

    [StringLength(50)]
    public string? CityOfBirth { get; set; }

    [StringLength(50)]
    public string? FacultyNumber { get; set; }

    [ForeignKey("SpecialityId")]
    [InverseProperty("Students")]
    public virtual Speciality Speciality { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Students")]
    public virtual User User { get; set; } = null!;
}
