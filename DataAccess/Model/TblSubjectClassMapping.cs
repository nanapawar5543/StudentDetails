using System;
using System.Collections.Generic;

namespace DataAccess.Model;

public partial class TblSubjectClassMapping
{
    public int SubjectClassMappingIdPk { get; set; }

    public int? SubjectMasterIdFk { get; set; }

    public int? ClassMasterIdFk { get; set; }

    public int SubjectMarks { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual TblClassMaster? ClassMasterIdFkNavigation { get; set; }

    public virtual TblSubjectMaster? SubjectMasterIdFkNavigation { get; set; }

    public virtual ICollection<TblStudentMarkDetail> TblStudentMarkDetails { get; set; } = new List<TblStudentMarkDetail>();
}
