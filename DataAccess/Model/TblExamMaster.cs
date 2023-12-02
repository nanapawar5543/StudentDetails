using System;
using System.Collections.Generic;

namespace DataAccess.Model;

public partial class TblExamMaster
{
    public int ExamMasterIdPk { get; set; }

    public string ExamName { get; set; } = null!;

    public bool IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<TblStudentDetail> TblStudentDetails { get; set; } = new List<TblStudentDetail>();
}
