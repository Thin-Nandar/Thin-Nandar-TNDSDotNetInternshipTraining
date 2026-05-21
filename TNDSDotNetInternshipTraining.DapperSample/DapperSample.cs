using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient; 
using System.Dynamic;

namespace TNDSDotNetInternshipTraining.DapperSample
{
    public class DapperSample
    {
        private readonly SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
        {
            DataSource = "localhost",
            InitialCatalog = "TNDSDotNetInternshipTraining",
            UserID = "sa",
            Password = "sasa@123",
            IntegratedSecurity = true,
            TrustServerCertificate = true
        };

        public void Read()
        {

            string query = @"SELECT [StudentId]
      ,[StudentNo]
      ,[StudentName]
      ,[FatherName]
      ,[Address]
      ,[DateOfBirth]
      ,[IsDelete]
      ,[CreatedDateTime]
      ,[CreatedBy]
      ,[ModifiedDateTime]
      ,[ModifiedBy]
  FROM [dbo].[Tbl_Student] Where IsDelete = 0 ";
            using IDbConnection db = new SqlConnection(builder.ConnectionString);
            db.Open();
            var lst = db.Query<Student>(query).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.StudentNo);
                Console.WriteLine(item.StudentName);
            }
        }
        public void Edit()
        {
            string query = $@"SELECT [StudentId]
      ,[StudentNo]
      ,[StudentName]
      ,[FatherName]
      ,[Address]
      ,[DateOfBirth]
      ,[IsDelete]
      ,[CreatedDateTime]
      ,[CreatedBy]
      ,[ModifiedDateTime]
      ,[ModifiedBy]
  FROM [dbo].[Tbl_Student] Where StudentId=@StudentId and IsDelete = 0";

            using IDbConnection db = new SqlConnection(builder.ConnectionString);
            db.Open();
            var item = db.Query<Student>(query, new Student { StudentId = 1 }).FirstOrDefault();
            if (item is null)
            {
                Console.WriteLine("No record found");
            }
            else
            {
                Console.WriteLine(item.StudentNo);
                Console.WriteLine(item.StudentName);
            }
        }
        public void Create()
        {
            string query = @"INSERT INTO Tbl_Student
            (
                StudentNo,
                StudentName,
                FatherName,
                Address,
                DateOfBirth,
                IsDelete,
                CreatedDateTime,
                CreatedBy
            )
            VALUES
            (
                @StudentNo,
                @StudentName,
                @FatherName,
                @Address,
                @DateOfBirth,
                0,
                @CreatedDateTime,
                @CreatedBy
            )";

            Student item = new Student()
            {
                StudentNo = "S-001",
                StudentName = "Mg Mg",
                FatherName = "U Ba",
                Address = "Yangon",
                DateOfBirth = new DateTime(2000, 1, 1),
                CreatedDateTime = DateTime.Now,
                CreatedBy = "admin"
            };

            using IDbConnection db = new SqlConnection(builder.ConnectionString);
            db.Open();

            int result = db.Execute(query, item);

            Console.WriteLine(result > 0
                ? "Saving Successful"
                : "Saving Failed");

        }
        public void Update()
        {
            string query = @"UPDATE Tbl_Student
            SET
                StudentNo = @StudentNo,
                StudentName = @StudentName,
                FatherName = @FatherName,
                Address = @Address,
                DateOfBirth = @DateOfBirth,
                ModifiedDateTime = @ModifiedDateTime,
                ModifiedBy = @ModifiedBy
            WHERE StudentId = @StudentId";

            Student item = new Student()
            {
                StudentId = 1,
                StudentNo = "S-001",
                StudentName = "Aung Aung",
                FatherName = "U Tun",
                Address = "Mandalay",
                DateOfBirth = new DateTime(2001, 2, 2),
                ModifiedDateTime = DateTime.Now,
                ModifiedBy = "admin"
            };

            using IDbConnection db = new SqlConnection(builder.ConnectionString);
            db.Open();

            int result = db.Execute(query, item);

            Console.WriteLine(result > 0
                ? "Updating Successful"
                : "Updating Failed");

        }
        public void Delete()
        {
            string query = @"UPDATE Tbl_Student
            SET
                IsDelete = 1,
                ModifiedDateTime = @ModifiedDateTime,
                ModifiedBy = @ModifiedBy
            WHERE StudentId = @StudentId";

            using IDbConnection db = new SqlConnection(builder.ConnectionString);
            db.Open();

            int result = db.Execute(query, new
            {
                StudentId = 1,
                ModifiedDateTime = DateTime.Now,
                ModifiedBy = 1
            });

            Console.WriteLine(result > 0
                ? "Deleting Successful"
                : "Deleting Failed");

        }
    }

}