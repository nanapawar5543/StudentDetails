using System;
using System.Collections.Generic;

namespace DataAccess.Model;

public partial class TblStudentDetail
{
    public int StudentDetailsIdPk { get; set; }

    public int? StudentMasterIdFk { get; set; }

    public int? ClassMasterIdFk { get; set; }

    public int? ExamMasterIdFk { get; set; }

    public decimal? SemPct { get; set; }

    public int Rollno { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual TblClassMaster? ClassMasterIdFkNavigation { get; set; }

    public virtual TblExamMaster? ExamMasterIdFkNavigation { get; set; }

    public virtual TblStudentMaster? StudentMasterIdFkNavigation { get; set; }

    public virtual ICollection<TblStudentMarkDetail> TblStudentMarkDetails { get; set; } = new List<TblStudentMarkDetail>();
}
