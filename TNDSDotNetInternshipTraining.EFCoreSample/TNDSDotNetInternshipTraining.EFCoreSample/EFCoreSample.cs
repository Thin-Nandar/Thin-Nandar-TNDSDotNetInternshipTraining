using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNDSDotNetInternshipTraining.EFCoreSample
{
    public class EFCoreSample
    {
        private readonly AppDbContext _db;
        public EFCoreSample()
        {
            _db = new AppDbContext();
        }


        public void Read()
        {

            List<Student> lst = _db.Students.ToList();
            foreach (Student s in lst)
            {
                Console.WriteLine(s.FatherName);
            }

        }

        public void Edit()
        {
            var student = _db.Students.FirstOrDefault(s => s.StudentId == 1);
            if (student != null)
            {
                Console.WriteLine("No of record found");
                return;

            }
            Console.WriteLine(JsonConvert.SerializeObject(student));
            Console.WriteLine(JsonConvert.SerializeObject(student, Formatting.Indented));
        }


        public void Update()
        {

            var student = _db.Students.FirstOrDefault(s => s.StudentId == 1);

            if (student == null)
            {
                Console.WriteLine("No record found to update.");
                return;
            }


            student.FatherName = "U Ba";



            int result = _db.SaveChanges();

            if (result > 0)
            {
                Console.WriteLine("Update Successful!");
            }
        }


        public void Delete()
        {

            var student = _db.Students.FirstOrDefault(s => s.StudentId == 1);

            if (student == null)
            {
                Console.WriteLine("No record found to delete.");
                return;
            }


            _db.Students.Remove(student);


            int result = _db.SaveChanges();

            if (result > 0)
            {
                Console.WriteLine("Delete Successful!");
            }
        }
    }
}





























    }
    }
}
