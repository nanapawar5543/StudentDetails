using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Models;
using System.Threading.Tasks;

namespace Abstraction.Interfaces.Services
{
    public interface IStudentServices
    {
        Task<List<PRNModel>> GetPRNDetails(string prnno);
        Task<StudentDetails> GetStudentDetails(string prnno);
        Task<StudentDetails> LoadClassExamDetails();
    }
}
