using System;
using System.Collections.Generic;

namespace DataAccess.Model;

public partial class TblStudentMaster
{
    public int StudentMasterIdPk { get; set; }

    public string? Prn { get; set; }

    public string StudentName { get; set; } = null!;

    public string StudentAddress { get; set; } = null!;

    public string ParentContact1 { get; set; } = null!;

    public string? ParentContact2 { get; set; }

    public string? StudentEmailId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<TblStudentDetail> TblStudentDetails { get; set; } = new List<TblStudentDetail>();
}
