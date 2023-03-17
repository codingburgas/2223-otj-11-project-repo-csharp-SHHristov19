using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Univers.DAL.Entities;

public partial class User
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(30)]
    public string Username { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? PasswordSalt { get; set; }

    [StringLength(30)]
    public string FirstName { get; set; } = null!;

    [StringLength(30)]
    public string MiddleName { get; set; } = null!;

    [StringLength(30)]
    public string LastName { get; set; } = null!;

    public DateTime DateOfRegistration { get; set; }

    [StringLength(16)]
    [Unicode(false)]
    public string PhoneNumber { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    [StringLength(10)]
    public string Gender { get; set; } = null!;

    [Unicode(false)]
    public string? Image { get; set; }

    [Required]
    public bool? IsActive { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Staff> Staff { get; } = new List<Staff>();

    [InverseProperty("User")]
    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
