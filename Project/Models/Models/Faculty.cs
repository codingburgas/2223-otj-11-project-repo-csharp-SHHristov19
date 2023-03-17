using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Faculty
{
    public string Id { get; set; } = null!;

    public string UniversityId { get; set; } = null!;

    public string DeanId { get; set; } = null!;

    public string ViceDeanId { get; set; }

    public string Name { get; set; }

    public string Code { get; set; }

    public virtual Staff Dean { get; set; } = null!;

    public virtual University University { get; set; } = null!;

    public virtual Staff ViceDean { get; set; }
}
