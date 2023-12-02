using System;
using System.Collections.Generic;

namespace DataAccess.Model;

public partial class TblSubjectMaster
{
    public int SubjectMasterIdPk { get; set; }

    public string SubjectName { get; set; } = null!;

    public bool IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<TblSubjectClassMapping> TblSubjectClassMappings { get; set; } = new List<TblSubjectClassMapping>();
}
