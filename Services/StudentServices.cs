using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using DataAccess.Model;
using Abstraction.Interfaces.Services;
using System.Web.WebPages.Html;
using System.Web.Mvc;

namespace Services
{
    public class StudentServices:IStudentServices
    {
        DbstudentDetailsContext _dbstudentDetailsContext;
        public StudentServices()
        {
                _dbstudentDetailsContext = new DbstudentDetailsContext();
        }
        public async Task<List<PRNModel>> GetPRNDetails(string prnno)
        {
            var prndetails = (from obj in _dbstudentDetailsContext.TblStudentMasters
                          where obj.Prn.Contains(prnno)
                          select new PRNModel()
                          {
                              PRN = obj.Prn,
                              StudentName = obj.Prn + " - " + obj.StudentName,
                          }).ToList();
            return prndetails;
        }
        public async Task<StudentDetails> GetStudentDetails(string prnno)
        {
            StudentDetails studentdetails = new StudentDetails();
            try
            {
                studentdetails = (from obj in _dbstudentDetailsContext.TblStudentMasters
                                      join objstudentdetails in _dbstudentDetailsContext.TblStudentDetails
                                      on obj.StudentMasterIdPk equals objstudentdetails.StudentMasterIdFk into studentDetailsGroup
                                      from objstudentdetails in studentDetailsGroup.DefaultIfEmpty()
                                      join objclassmaster in _dbstudentDetailsContext.TblClassMasters
                                  on objstudentdetails.ClassMasterIdFk equals objclassmaster.ClassMasterIdPk into ClassMastersGroup
                                      from objclassmaster in ClassMastersGroup.DefaultIfEmpty()
                                      join objexamtype in _dbstudentDetailsContext.TblExamMasters
                                  on objstudentdetails.ExamMasterIdFk equals objexamtype.ExamMasterIdPk into ExamMastersGroup
                                      from objexamtype in ExamMastersGroup.DefaultIfEmpty()
                                      where obj.Prn == prnno
                                      select new StudentDetails()
                                      {
                                          studentName = obj.StudentName,
                                          studentAddress = obj.StudentAddress,
                                          ParentContact1 = obj.ParentContact1,
                                          ParentContact2 = obj.ParentContact2,
                                          studentEmailID = obj.StudentEmailId,
                                          Rollno = objstudentdetails.Rollno,
                                          ClassMasterID = objclassmaster.ClassMasterIdPk,
                                          ClassName = objclassmaster.ClassName,
                                          ExamMasterID = objexamtype.ExamMasterIdPk,
                                          ExamName = objexamtype.ExamName
                                      }).FirstOrDefault();
            }
            catch(Exception ex)
            {

            }
            return studentdetails;
        }

        public async Task<StudentDetails> LoadClassExamDetails()
        {
            StudentDetails studentdetails = new StudentDetails();
            if(_dbstudentDetailsContext != null)
            {
                studentdetails.ListofExamTypes = _dbstudentDetailsContext.TblExamMasters
                .Select(obj => new System.Web.Mvc.SelectListItem
                {
                    Text = obj.ExamName,
                    Value = obj.ExamMasterIdPk.ToString()
                }).ToList();
                studentdetails.ListofClassNames = _dbstudentDetailsContext.TblClassMasters
                .Select(obj => new System.Web.Mvc.SelectListItem
                {
                    Text = obj.ClassName,
                    Value = obj.ClassMasterIdPk.ToString()
                }).ToList();
            }
            else
            {
                studentdetails.ListofExamTypes = new List<System.Web.Mvc.SelectListItem> ();
                studentdetails.ListofClassNames = new List<System.Web.Mvc.SelectListItem>();
            }
            
            return studentdetails;
        }

        public async Task<List<SubjectMaster>> GetSubjectMasterDetails(int ClassID)
        {
            var subjects = (from objsubjmapping in _dbstudentDetailsContext.TblSubjectClassMappings
                            join objsubject in _dbstudentDetailsContext.TblSubjectMasters
                            on objsubjmapping.SubjectMasterIdFk equals objsubject.SubjectMasterIdPk
                            join objclass in _dbstudentDetailsContext.TblClassMasters
                            on objsubjmapping.ClassMasterIdFk equals objclass.ClassMasterIdPk
                            where objclass.ClassMasterIdPk== ClassID
                            select new SubjectMaster()
                            {
                                SubjectID=objsubjmapping.SubjectClassMappingIdPk,
                                SubjectName=objsubject.SubjectName
                            }).ToList();
            return subjects;
        }
        public async Task<int> GetSubjectTotalMarks(int SubjectID)
        {
            int totalMarks=_dbstudentDetailsContext.TblSubjectClassMappings.Where(x=>x.SubjectClassMappingIdPk==SubjectID)
                .Select(y=>y.SubjectMarks).FirstOrDefault();
            return totalMarks;
        }
    }
}
