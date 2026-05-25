using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TNDSDotNetInternshipTraining.Database.AppDbContextModels;
using TNDSDotNetInternshipTraining.WebApi.Models;
using System.IO.Pipes;

namespace TNDSDotNetInternshipTraining.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();

        [HttpGet]
        public IActionResult GetStudents()
        {
            List<TblStudent> students = _db.TblStudents.Where(s => s.IsDelete == false).ToList();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            TblStudent student = _db.TblStudents.Where(s => s.StudentId == id && s.IsDelete == false).FirstOrDefault();
            if (student is null)
            {
                return NotFound("Student not found");
            }
            return Ok(student);
        }

        [HttpPost]
        public IActionResult CreateStudent(StudentCreateRequestModel request)
        {
            TblStudent student = new TblStudent()
            {
                StudentNo = request.StudentNo,
                StudentName = request.StudentName,
                FatherName = request.FatherName,
                Address = request.Address,
                DateOfBirth = request.DateOfBirth,
                IsDelete = false,
                CreatedDateTime = DateTime.Now,
                CreatedBy = request.CreatedBy
            };
            _db.TblStudents.Add(student);
            int result = _db.SaveChanges();
            return StatusCode(201, new StudentCreateResponseModel
            {
                isSuccess = result > 0,
                Message = result > 0 ? "Student created successfully" : "Failed to create student"
            });

        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, StudentUpdateRequestModel request)
        {
            TblStudent stu = _db.TblStudents.Where(s => s.StudentId == id && s.IsDelete == false).FirstOrDefault();
            if (stu is null)
            {
                return NotFound("Student not found");
            }
            stu.StudentNo = request.StudentNo;
            stu.StudentName = request.StudentName;
            stu.FatherName = request.FatherName;
            stu.Address = request.Address;
            stu.DateOfBirth = request.DateOfBirth;
            stu.IsDelete = request.IsDelete;
            stu.ModifiedDateTime = DateTime.Now;
            stu.ModifiedBy = request.ModifiedBy;
            int result = _db.SaveChanges();
            return StatusCode(200, new StudentUpdateResponseModel
            {
                isSuccess = result > 0,
                Message = result > 0 ? "Student updated successfully" : "Failed to update student",
                Student = new StudentModel
                {
                    StudentId = stu.StudentId,
                    StudentNo = stu.StudentNo,
                    StudentName = stu.StudentName,
                    FatherName = stu.FatherName,
                    Address = stu.Address,
                    DateOfBirth = stu.DateOfBirth,
                    IsDelete = stu.IsDelete,
                    ModifiedBy = stu.ModifiedBy
                }

            });
        }

        [HttpPatch("{id}")]
        public IActionResult PatchStudent(int id, StudentPatchRequestModel request)
        {
            TblStudent stu = _db.TblStudents.Where(s => s.StudentId == id && s.IsDelete == false).FirstOrDefault();

            int count = 0;
            if (stu is null)
            {
                count++;
                return NotFound("Student not found");
            }

            if(!string.IsNullOrEmpty(request.StudentNo))
            {
                count++;
                stu.StudentNo = request.StudentNo;
            }
            if(!string.IsNullOrEmpty(request.StudentName))
           {
                count++;
                stu.StudentName = request.StudentName;
           }
            if(!string.IsNullOrEmpty(request.FatherName))
            {
                count++;
                stu.FatherName = request.FatherName;
            }
            if (!string.IsNullOrEmpty(request.Address))
            {
                count++;
                stu.Address = request.Address;
            }
            if(!string.IsNullOrEmpty(request.DateOfBirth.ToString()))
            {
                count++;
                stu.DateOfBirth = request.DateOfBirth;
            }
            if(!string.IsNullOrEmpty(request.IsDelete.ToString()))
            {
                count++;
                stu.IsDelete = request.IsDelete;
            }
            if(!string.IsNullOrEmpty(request.ModifiedBy.ToString()))
            {
                count++;
                stu.ModifiedBy = request.ModifiedBy;
            }

            if(count == 0)
            {
                return NotFound(new StudentUpdateResponseModel
                {
                    isSuccess = false,
                    Message = "No data found to update"
                });
            }

            var result = _db.SaveChanges();
            return StatusCode(200, new StudentUpdateResponseModel
            {
                isSuccess = result > 0,
                Message = result > 0 ? "Student updated successfully" : "Failed to update student",
                Student = new StudentModel
                {
                    StudentId = stu.StudentId,
                    StudentNo = stu.StudentNo,
                    StudentName = stu.StudentName,
                    FatherName = stu.FatherName,
                    Address = stu.Address,
                    DateOfBirth = stu.DateOfBirth,
                    IsDelete = stu.IsDelete,
                    ModifiedBy = stu.ModifiedBy
                }
            });

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            TblStudent stu = _db.TblStudents.Where(s => s.StudentId == id && s.IsDelete == false).FirstOrDefault();
            if (stu is null)
            {
                return NotFound("Student not found");
            }
            stu.IsDelete = true;
            stu.ModifiedDateTime = DateTime.Now;
            int result = _db.SaveChanges();
            return StatusCode(200, new StudentDeleteResponseModel
            {
                isSuccess = result > 0,
                Message = result > 0 ? "Student deleted successfully" : "Failed to delete student"
            });

        }
        }
}
