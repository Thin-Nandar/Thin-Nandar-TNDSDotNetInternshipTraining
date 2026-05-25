namespace TNDSDotNetInternshipTraining.WebApi.Models
{
    public class StudentCreateRequestModel
    {
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

    public class StudentCreateResponseModel
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; } = null!;
    }

    public class StudentPatchRequestModel
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

    public class StudentPatchResponseModel
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; } = null!;

    }

    public class StudentUpdateRequestModel
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

    public class StudentUpdateResponseModel
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; } = null!;
        public StudentModel Student { get; set; } = null!;

    }

    public class StudentModel
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

    public class StudentDeleteResponseModel
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; } = null!;
    }
}
