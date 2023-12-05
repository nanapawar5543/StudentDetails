using Abstraction.Interfaces.Services;
using Common.Models;
using DataAccess.Model;

namespace Services
{
    public class StudentServices : IStudentServices
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
            catch (Exception ex)
            {

            }
            return studentdetails;
        }

        public async Task<StudentDetails> LoadClassExamDetails()
        {
            StudentDetails studentdetails = new StudentDetails();
            if (_dbstudentDetailsContext != null)
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
                studentdetails.ListofExamTypes = new List<System.Web.Mvc.SelectListItem>();
                studentdetails.ListofClassNames = new List<System.Web.Mvc.SelectListItem>();
            }

            return studentdetails;
        }

        public async Task<StudentSubjectMarkDetails> GetStudentSubjectMarkDetails(int ClassID, int ExamTypeID, string PRN)
        {
            StudentSubjectMarkDetails studentSubjectMarkDetails = new StudentSubjectMarkDetails();
            studentSubjectMarkDetails.Rollno = ( from objstudentdetails in _dbstudentDetailsContext.TblStudentDetails
                                                 join objstudentMaster in _dbstudentDetailsContext.TblStudentMasters
                                                 on objstudentdetails.StudentMasterIdFk equals objstudentMaster.StudentMasterIdPk
                                                 where objstudentdetails.ClassMasterIdFk==ClassID && objstudentdetails.ExamMasterIdFk==ExamTypeID
                                                 && objstudentMaster.Prn==PRN
                                                 select objstudentdetails.Rollno).FirstOrDefault();
                

            studentSubjectMarkDetails.ListSubjectMasters = (from objsubjmapping in _dbstudentDetailsContext.TblSubjectClassMappings
                                                            join objsubject in _dbstudentDetailsContext.TblSubjectMasters
                                                            on objsubjmapping.SubjectMasterIdFk equals objsubject.SubjectMasterIdPk
                                                            join objclass in _dbstudentDetailsContext.TblClassMasters
                                                            on objsubjmapping.ClassMasterIdFk equals objclass.ClassMasterIdPk
                                                            where objclass.ClassMasterIdPk == ClassID
                                                            select new SubjectMaster()
                                                            {
                                                                SubjectID = objsubjmapping.SubjectClassMappingIdPk,
                                                                SubjectName = objsubject.SubjectName
                                                            }).ToList();

            studentSubjectMarkDetails.ListStudentMarks = (from objstudentmarks in _dbstudentDetailsContext.TblStudentMarkDetails
                                                          join objstudentsubmapping in _dbstudentDetailsContext.TblSubjectClassMappings
                                                          on objstudentmarks.SubjectClassMappingIdFk equals objstudentsubmapping.SubjectClassMappingIdPk
                                                          join objsubjectmaster in _dbstudentDetailsContext.TblSubjectMasters
                                                          on objstudentsubmapping.SubjectMasterIdFk equals objsubjectmaster.SubjectMasterIdPk
                                                          join objstudentdetails in _dbstudentDetailsContext.TblStudentDetails
                                                          on objstudentmarks.StudentDetailsIdFk equals objstudentdetails.StudentDetailsIdPk
                                                          join objstudentmaster in _dbstudentDetailsContext.TblStudentMasters
                                                          on objstudentdetails.StudentMasterIdFk equals objstudentmaster.StudentMasterIdPk
                                                          where objstudentmaster.Prn == PRN && objstudentdetails.ClassMasterIdFk == ClassID
                                                          && objstudentdetails.ExamMasterIdFk == ExamTypeID
                                                          select new StudentMarks()
                                                          {
                                                              subjectID = objstudentsubmapping.SubjectClassMappingIdPk,
                                                              SubjectName = objsubjectmaster.SubjectName,
                                                              TotalMarks = objstudentsubmapping.SubjectMarks,
                                                              ObtainedMarks = objstudentmarks.ObtainedMarks
                                                          }).ToList();

            return studentSubjectMarkDetails;
        }
        public async Task<int> GetSubjectTotalMarks(int SubjectID)
        {
            int totalMarks = (int)_dbstudentDetailsContext.TblSubjectClassMappings.Where(x => x.SubjectClassMappingIdPk == SubjectID)
                .Select(y => y.SubjectMarks).FirstOrDefault();
            return totalMarks;
        }
        public async Task<int> SaveStudentDetails(StudentDetails studentdetails)
        {
            try
            {
                bool isExistPRN = _dbstudentDetailsContext.TblStudentMasters.Where(a => a.Prn == studentdetails.PRN).Any();
                if (isExistPRN)
                {
                    var existingStudentMaster = _dbstudentDetailsContext.TblStudentMasters
                        .Where(a => a.Prn == studentdetails.PRN).FirstOrDefault();
                    if (existingStudentMaster != null)
                    {
                        existingStudentMaster.StudentName = studentdetails.studentName;
                        existingStudentMaster.StudentAddress = studentdetails.studentAddress;
                        existingStudentMaster.ParentContact1 = studentdetails.ParentContact1;
                        existingStudentMaster.ParentContact2 = studentdetails.ParentContact2;
                        existingStudentMaster.StudentEmailId = studentdetails.studentEmailID;
                        _dbstudentDetailsContext.SaveChanges();
                    }

                    int studentDetailsPK = _dbstudentDetailsContext.TblStudentDetails
                        .Where(a => a.StudentMasterIdFk == existingStudentMaster.StudentMasterIdPk
                        && a.ClassMasterIdFk == studentdetails.ClassMasterID
                        && a.ExamMasterIdFk == studentdetails.ExamMasterID)
                        .Select(b => b.StudentDetailsIdPk).FirstOrDefault();

                    if (studentDetailsPK > 0)
                    {
                        var existingStudentDetails = _dbstudentDetailsContext.TblStudentDetails
                        .Where(a => a.StudentMasterIdFk == existingStudentMaster.StudentMasterIdPk).FirstOrDefault();
                        if (existingStudentDetails != null)
                        {
                            existingStudentDetails.Rollno = studentdetails.Rollno;
                            _dbstudentDetailsContext.SaveChanges();
                        }
                    }
                    else
                    {
                        TblStudentDetail tblStudentDetail = new TblStudentDetail()
                        {
                            ClassMasterIdFk = studentdetails.ClassMasterID,
                            StudentMasterIdFk = existingStudentMaster.StudentMasterIdPk,
                            ExamMasterIdFk = studentdetails.ExamMasterID,
                            Rollno = studentdetails.Rollno
                        };
                        _dbstudentDetailsContext.TblStudentDetails.Add(tblStudentDetail);
                        _dbstudentDetailsContext.SaveChanges();
                    }

                    studentDetailsPK = _dbstudentDetailsContext.TblStudentDetails
                        .Where(a => a.StudentMasterIdFk == existingStudentMaster.StudentMasterIdPk
                        && a.ClassMasterIdFk == studentdetails.ClassMasterID
                        && a.ExamMasterIdFk == studentdetails.ExamMasterID)
                        .Select(b => b.StudentDetailsIdPk).FirstOrDefault();

                    if (studentdetails.ListofStudentMarks.Count > 1  && studentDetailsPK > 0)
                    {
                        foreach (var item in studentdetails.ListofStudentMarks)
                        {
                            var existingStudentMarks = _dbstudentDetailsContext.TblStudentMarkDetails
                                .Where(a => a.StudentDetailsIdFk == studentDetailsPK && a.SubjectClassMappingIdFk == item.subjectID).FirstOrDefault();
                            if (existingStudentMarks != null)
                            {
                                existingStudentMarks.ObtainedMarks = item.ObtainedMarks;
                                _dbstudentDetailsContext.SaveChanges();
                            }
                            else
                            {
                                TblStudentMarkDetail tblmarksdetails = new TblStudentMarkDetail()
                                {
                                    StudentDetailsIdFk = studentDetailsPK,
                                    SubjectClassMappingIdFk = item.subjectID,
                                    ObtainedMarks = item.ObtainedMarks
                                };
                                _dbstudentDetailsContext.TblStudentMarkDetails.Add(tblmarksdetails);
                                _dbstudentDetailsContext.SaveChanges();
                            }
                        }
                    }
                }
                else
                {
                    TblStudentMaster tblStudent = new TblStudentMaster()
                    {
                        Prn = studentdetails.PRN,
                        StudentName = studentdetails.studentName,
                        StudentAddress = studentdetails.studentAddress,
                        ParentContact1 = studentdetails.ParentContact1,
                        ParentContact2 = studentdetails.ParentContact2,
                        StudentEmailId = studentdetails.studentEmailID,
                    };
                    _dbstudentDetailsContext.TblStudentMasters.Add(tblStudent);
                    _dbstudentDetailsContext.SaveChanges();

                    int studentMasterPK = _dbstudentDetailsContext.TblStudentMasters
                        .Where(a => a.Prn == studentdetails.PRN).Select(b => b.StudentMasterIdPk).FirstOrDefault();

                    if (studentMasterPK > 0)
                    {
                        TblStudentDetail tblStudentDetail = new TblStudentDetail()
                        {
                            ClassMasterIdFk = studentdetails.ClassMasterID,
                            StudentMasterIdFk = studentMasterPK,
                            ExamMasterIdFk = studentdetails.ExamMasterID,
                            Rollno = studentdetails.Rollno
                        };
                        _dbstudentDetailsContext.TblStudentDetails.Add(tblStudentDetail);
                        _dbstudentDetailsContext.SaveChanges();
                    }


                    int studentDetailsPK = _dbstudentDetailsContext.TblStudentDetails
                        .Where(a => a.StudentMasterIdFk == studentMasterPK
                        && a.ClassMasterIdFk == studentdetails.ClassMasterID
                        && a.ExamMasterIdFk == studentdetails.ExamMasterID)
                        .Select(b => b.StudentDetailsIdPk).FirstOrDefault();

                    if (studentdetails.ListofClassNames != null && studentDetailsPK > 0)
                    {
                        foreach (var item in studentdetails.ListofStudentMarks)
                        {
                            TblStudentMarkDetail tblmarksdetails = new TblStudentMarkDetail()
                            {
                                StudentDetailsIdFk = studentDetailsPK,
                                SubjectClassMappingIdFk = item.subjectID,
                                ObtainedMarks = item.ObtainedMarks
                            };
                            _dbstudentDetailsContext.TblStudentMarkDetails.Add(tblmarksdetails);
                            _dbstudentDetailsContext.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

            return 1;
        }
    }
}
