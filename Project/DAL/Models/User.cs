using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class User
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(30)]
    public string Username { get; set; } = null!;

    [StringLength(30)]
    public string Password { get; set; } = null!;

    [StringLength(30)]
    [Unicode(true)]
    public string FirstName { get; set; } = null!;

    [StringLength(30)]
    [Unicode(true)]
    public string MiddleName { get; set; } = null!;

    [StringLength(30)]
    [Unicode(true)]
    public string LastName { get; set; } = null!;

    public DateTime DateOfRegistration { get; set; }

    [StringLength(16)]
    public string PhoneNumber { get; set; } = null!;

    [StringLength(50)]
    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Image { get; set; }

    public bool? IsActive { get; set; } = true;

    [InverseProperty("User")]
    public virtual ICollection<Staff> Staff { get; } = new List<Staff>();

    [InverseProperty("User")]
    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
