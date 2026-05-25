using System;
using System.Collections.Generic;

namespace TNDSDotNetInternshipTraining.Database.AppDbContextModels;

public partial class TblStudent
{
    public int StudentId { get; set; }

    public string StudentNo { get; set; } = null!;

    public string? StudentName { get; set; }

    public string? FatherName { get; set; }

    public string? Address { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public bool IsDelete { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public int? ModifiedBy { get; set; }
}
