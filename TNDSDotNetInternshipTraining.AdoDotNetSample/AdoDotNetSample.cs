using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNDSDotNetInternshipTraining.AdoDotNetSample
{
    public class AdoDotNetSample
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
            Console.WriteLine("This is the connection string: " + builder.ConnectionString);
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();

            string query = @"SELECT [StudentId],[StudentNo],[StudentName],[FatherName],[Address],[DateOfBirth],[IsDelete],[CreatedDateTime],[CreatedBy],[ModifiedDateTime],[ModifiedBy] FROM [dbo].[Tbl_Student] WHERE [IsDelete] = 0";

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No records found.");
                return;
            }

            List<Student> lst = new List<Student>();
            foreach (DataRow row in dt.Rows)
            {
                Student item = new Student()
                {
                    StudentId = Convert.ToInt32(row["StudentId"]),
                    StudentNo = row["StudentNo"]?.ToString() ?? string.Empty,
                    StudentName = row["StudentName"]?.ToString() ?? string.Empty,
                    FatherName = row["FatherName"]?.ToString() ?? string.Empty,
                    Address = row["Address"]?.ToString() ?? string.Empty,
                    DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]),
                    IsDelete = Convert.ToBoolean(row["IsDelete"]),
                    CreatedDateTime = Convert.ToDateTime(row["CreatedDateTime"]),
                    CreatedBy = row["CreatedBy"]?.ToString() ?? string.Empty,
                    ModifiedDateTime = row["ModifiedDateTime"] == DBNull.Value ? null : Convert.ToDateTime(row["ModifiedDateTime"]),
                    ModifiedBy = row["ModifiedBy"] == DBNull.Value ? null : row["ModifiedBy"].ToString()
                };

                lst.Add(item);
                Console.WriteLine($"Name: {item.StudentName}, DOB: {item.DateOfBirth.ToString("yyyy-MM-dd")}");
            }
        }

        public void Edit(int id) 
        {
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();

            string query = @"SELECT [StudentId],[StudentNo],[StudentName],[FatherName],[Address],[DateOfBirth],[IsDelete],[CreatedDateTime],[CreatedBy],[ModifiedDateTime],[ModifiedBy] 
                             FROM [dbo].[Tbl_Student] WHERE StudentId = @StudentId AND [IsDelete] = 0";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@StudentId", id);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No record found.");
                return;
            }

            DataRow row = dt.Rows[0];
            Student item = new Student()
            {
                StudentId = Convert.ToInt32(row["StudentId"]),
                StudentNo = row["StudentNo"]?.ToString() ?? string.Empty,
                StudentName = row["StudentName"]?.ToString() ?? string.Empty,
                FatherName = row["FatherName"]?.ToString() ?? string.Empty,
                Address = row["Address"]?.ToString() ?? string.Empty,
                DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]),
                IsDelete = Convert.ToBoolean(row["IsDelete"]),
                CreatedDateTime = Convert.ToDateTime(row["CreatedDateTime"]),
                CreatedBy = row["CreatedBy"]?.ToString() ?? string.Empty,
                ModifiedDateTime = row["ModifiedDateTime"] == DBNull.Value ? null : Convert.ToDateTime(row["ModifiedDateTime"]),
                ModifiedBy = row["ModifiedBy"] == DBNull.Value ? null : row["ModifiedBy"].ToString()
            };

            Console.WriteLine($"Found Student: {item.StudentName}, DOB: {item.DateOfBirth.ToString("yyyy-MM-dd")}");
        }

        
        public void Create(Student student)
        {
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Student] 
                            ([StudentNo], [StudentName], [FatherName], [Address], [DateOfBirth], [IsDelete], [CreatedDateTime], [CreatedBy]) 
                            VALUES 
                            (@StudentNo, @StudentName, @FatherName, @Address, @DateOfBirth, @IsDelete, @CreatedDateTime, @CreatedBy)";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@StudentNo", student.StudentNo);
            cmd.Parameters.AddWithValue("@StudentName", student.StudentName);
            cmd.Parameters.AddWithValue("@FatherName", student.FatherName);
            cmd.Parameters.AddWithValue("@Address", student.Address);
            cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
            cmd.Parameters.AddWithValue("@IsDelete", false); // အသစ်ဆောက်တာမို့ အမြဲ false ပါ
            cmd.Parameters.AddWithValue("@CreatedDateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", student.CreatedBy ?? "System");

            int result = cmd.ExecuteNonQuery();
            connection.Close();

            if (result > 0)
                Console.WriteLine("Student created successfully!");
            else
                Console.WriteLine("Failed to create student.");
        }

        
        public void Update(int id, Student student)
        {
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Student] 
                             SET [StudentNo] = @StudentNo, 
                                 [StudentName] = @StudentName, 
                                 [FatherName] = @FatherName, 
                                 [Address] = @Address, 
                                 [DateOfBirth] = @DateOfBirth,
                                 [ModifiedDateTime] = @ModifiedDateTime,
                                 [ModifiedBy] = @ModifiedBy
                             WHERE [StudentId] = @StudentId";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@StudentId", id);
            cmd.Parameters.AddWithValue("@StudentNo", student.StudentNo);
            cmd.Parameters.AddWithValue("@StudentName", student.StudentName);
            cmd.Parameters.AddWithValue("@FatherName", student.FatherName);
            cmd.Parameters.AddWithValue("@Address", student.Address);
            cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
            cmd.Parameters.AddWithValue("@ModifiedDateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@ModifiedBy", student.ModifiedBy ?? "System_Admin");

            int result = cmd.ExecuteNonQuery();
            connection.Close();

            if (result > 0)
                Console.WriteLine("Student updated successfully!");
            else
                Console.WriteLine("No record found to update.");
        }

        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Student] 
                             SET [IsDelete] = 1, 
                                 [ModifiedDateTime] = @ModifiedDateTime, 
                                 [ModifiedBy] = @ModifiedBy 
                             WHERE [StudentId] = @StudentId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@StudentId", id);
            cmd.Parameters.AddWithValue("@ModifiedDateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@ModifiedBy", "System_Admin");

            int result = cmd.ExecuteNonQuery();
            connection.Close();

            if (result > 0)
                Console.WriteLine("Student deleted (Soft Delete) successfully!");
            else
                Console.WriteLine("No record found to delete.");
        }
    }
}