using System;
using System.Collections.Generic;

namespace DataAccess.Model;

public partial class TblClassMaster
{
    public int ClassMasterIdPk { get; set; }

    public string ClassName { get; set; } = null!;

    public bool IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<TblStudentDetail> TblStudentDetails { get; set; } = new List<TblStudentDetail>();

    public virtual ICollection<TblSubjectClassMapping> TblSubjectClassMappings { get; set; } = new List<TblSubjectClassMapping>();
}
