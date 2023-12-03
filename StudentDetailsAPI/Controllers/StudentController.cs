using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using Common.Models;
using Abstraction.Interfaces.Services;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace StudentDetailsAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class StudentController : ControllerBase
    {
        IStudentServices _studentServices;
        public StudentController(IStudentServices studentServices) {
            _studentServices = studentServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetPRNDetails(string prnno)
        {
            List<PRNModel> studentPRNDetailsList = new List<PRNModel>();
            studentPRNDetailsList=await _studentServices.GetPRNDetails(prnno);
            return Ok(studentPRNDetailsList);
        }
        [HttpGet]
        public async Task<IActionResult> GetStudentDetails(string prnno)
        {
            StudentDetails studentDetailsList = new StudentDetails();
            studentDetailsList =await _studentServices.GetStudentDetails(prnno);
            return Ok(studentDetailsList);
        }
        [HttpGet]
        public async Task<IActionResult> LoadClassExamDetails()
        {
            StudentDetails studentDetailsList = new StudentDetails();
            studentDetailsList =await _studentServices.LoadClassExamDetails();
            return Ok(studentDetailsList);
        }
        [HttpGet]
        public async Task<IActionResult> GetSubjectDetails(int ClassID)
        {
            List<SubjectMaster> subjects=new List<SubjectMaster>();
            int intClassID=Convert.ToInt32(ClassID);
            subjects = await _studentServices.GetSubjectMasterDetails(ClassID);
            return Ok(subjects);
        }
        [HttpGet]
        public async Task<IActionResult> GetSubjectTotalMarks(int SubjectID)
        {
            int totalMarks = await _studentServices.GetSubjectTotalMarks(SubjectID);
            return Ok(totalMarks);
        }
    }
}
