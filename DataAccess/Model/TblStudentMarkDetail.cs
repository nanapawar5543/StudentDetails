using System;
using System.Collections.Generic;

namespace DataAccess.Model;

public partial class TblStudentMarkDetail
{
    public int StudentMarkDetailsIdPk { get; set; }

    public int? StudentDetailsIdFk { get; set; }

    public int? SubjectClassMappingIdFk { get; set; }

    public int ObtainedMarks { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual TblStudentDetail? StudentDetailsIdFkNavigation { get; set; }

    public virtual TblSubjectClassMapping? SubjectClassMappingIdFkNavigation { get; set; }
}
