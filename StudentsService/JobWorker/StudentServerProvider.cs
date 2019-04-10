using Contracts;
using StudentsService_Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobWorker
{
    public class StudentServerProvider : IStudent
    {
        StudentDataRepository studentRepo = new StudentDataRepository();

        public void AddStudent(string indexNo, string name, string lastName)
        {
            Student s = new Student(indexNo);
            s.Name = name;
            s.LastName = lastName;

            studentRepo.AddStudent(s);
            Trace.TraceInformation("Student dodat.");
        }

        public List<string> RetrieveAllIndexes()
        {
            var temp = studentRepo.RetrieveAllStudents().ToList().Select(s => s.RowKey);
            return temp.ToList();
        }
    }
}
