using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Abstraction.Interfaces.Services
{
    public interface IStudentServices
    {
        Task<List<PRNModel>> GetPRNDetails(string prnno);
        Task<StudentDetails> GetStudentDetails(string prnno);
        Task<StudentDetails> LoadClassExamDetails();
        Task<StudentSubjectMarkDetails> GetStudentSubjectMarkDetails(int ClassID, int ExamTypeID, string PRN);
        Task<int> GetSubjectTotalMarks(int SubjectID);
        Task<int> SaveStudentDetails(StudentDetails studentdetails);
    }
}
