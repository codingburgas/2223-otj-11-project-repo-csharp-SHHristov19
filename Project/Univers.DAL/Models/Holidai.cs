﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

[Keyless]
public partial class Holidai
{
    [StringLength(100)]
    public string? Name { get; set; }

    [Column(TypeName = "date")]
    public DateTime? DateOfStart { get; set; }

    [Column(TypeName = "date")]
    public DateTime? DateOfEnd { get; set; }
}