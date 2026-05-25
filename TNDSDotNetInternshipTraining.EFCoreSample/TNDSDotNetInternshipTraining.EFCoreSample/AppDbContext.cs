using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNDSDotNetInternshipTraining.EFCoreSample
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=TNDSDotNetInternshipTraining;User Id=sa;Password=sasa@123;TrustServerCertificate=True;");


            }
        }
        public DbSet<Student> Students { get; set; }


    }

    [Table("Tbl_Student")]
    public class Student
    {
        [Key]

        public int StudentId { get; set; }
        public string StudentNo { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? ModifiedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
